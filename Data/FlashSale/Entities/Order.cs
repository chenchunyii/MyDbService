using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MyDbService.Data.FlashSale.Enums;

namespace MyDbService.Data.FlashSale.Entities
{
    [Table("orders")]
    [Index(nameof(UserId), nameof(ProductId), IsUnique = true)]
    [Index(nameof(ProductId), IsUnique = false)]
    public class Order
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("product_id")]
        public long ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal UnitPrice { get; set; }
        [Column("status")]
        public OrderStatus Status { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}