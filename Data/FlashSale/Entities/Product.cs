using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDbService.Data.FlashSale.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Column("price")]
        public decimal Price { get; set; }
        [Column("stock")]
        public int Stock { get; set; }
        [Column("sale_start_at")]
        public DateTime SaleStartAt { get; set; }
        [Column("sale_end_at")]
        public DateTime SaleEndAt { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}