using System;
using SQLite;

namespace Products.Models
{
    [Table("product_types")]
    public class ProductType
    {
        [PrimaryKey, Column("name")]
        public string Name { get; set; }

        [Column("measure")]
        public string Measure { get; set; }
    }
}