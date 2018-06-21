using System;
using System.Runtime.Serialization;
using SQLite;

namespace Products.Models
{
    [Table("products"), DataContract]
    public class Product
    {
        [PrimaryKey, Column("id"), DataMember]
        public int Id { get; set; }

        [Column("name"), DataMember]
        public string Name { get; set; }

        [Column("type"), DataMember]
        public string Type { get; set; }

        [Column("description"), DataMember]
        public string Description { get; set; }

        [Column("price"), DataMember]
        public decimal Price { get; set; }

        [Column("img_file_name"), DataMember]
        public string ImgFileName { get; set; }
    }
}