# MyDbService

ASP.NET Core (.NET 10) + EF Core 10，使用 MySQL（`MySql.EntityFrameworkCore`）。

## 前置需求

安裝 EF Core 命令列工具（只需安裝一次）：

```bash
dotnet tool install --global dotnet-ef
```

已安裝過的話，可以用 `dotnet tool update --global dotnet-ef` 更新。

## 新增 Entity / DbContext 後要做的事

每個資料庫各有自己的 DbContext 和 Migrations 資料夾：

| 資料庫 | DbContext | 連線字串名稱 | Migrations 資料夾 |
|---|---|---|---|
| member | `MemberDbContext` | `Member` | `Data/Member/Migrations` |

### 1. 註冊 DbContext（新增 DbContext 時才需要）

在 `appsettings.json` 的 `ConnectionStrings` 加上連線字串：

```json
"Member": "Server=127.0.0.1;Port=3306;Database=member;User=chun;Password=...;"
```

然後在 `Program.cs` 註冊：

```csharp
builder.Services.AddDbContext<MemberDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Member")!));
```

### 2. 建立 Migration

```bash
dotnet ef migrations add <MigrationName> --context MemberDbContext --output-dir Data/Member/Migrations
```

- `<MigrationName>`：描述這次的變更，例如 `InitialCreate`、`AddPhoneToUser`
- `--context`：專案裡有多個 DbContext，所以一定要指定
- `--output-dir`：讓每個 DbContext 的 migration 放在各自的資料夾

產生之後，打開 `Data/Member/Migrations/<時間戳>_<MigrationName>.cs`，確認表名、欄位和索引是否正確。

### 3. 套用到資料庫

```bash
dotnet ef database update --context MemberDbContext
```

如果資料庫還不存在，EF 會先建立它（MySQL 帳號需要有 CREATE 權限）。

### 4. 確認結果（選用）

```bash
mysql -u chun -p -e "SHOW TABLES FROM member; SHOW CREATE TABLE member.users\G"
```

## 常用指令

```bash
# 列出所有 migration 和它們是否已套用
dotnet ef migrations list --context MemberDbContext

# 撤掉最新一個「還沒套用」的 migration
dotnet ef migrations remove --context MemberDbContext

# 把資料庫退回到指定的 migration（用 0 會退回到全部都沒套用）
dotnet ef database update <MigrationName> --context MemberDbContext

# 產生 SQL 腳本，但不直接執行（例如要交給 DBA）
dotnet ef migrations script --context MemberDbContext --idempotent -o member.sql
```

## 注意事項

- 修改 Entity 後一定要重新跑「建立 Migration → 套用到資料庫」，只改 C# 程式碼不會影響資料庫。
- 已經套用、或已經推上 git 的 migration 不要直接修改或刪除，要用新的 migration 來修正。
- 預設值這類 Attribute 做不到的設定（例如 `DEFAULT CURRENT_TIMESTAMP`），要在 DbContext 的 `OnModelCreating` 裡用 Fluent API 設定。
