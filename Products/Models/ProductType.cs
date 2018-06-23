using System;
using System.Collections.Generic;

namespace Products.Models
{
    public class ProductType
    {
        public string Name { get; set; }
        public string Measure { get; set; }

        public static List<ProductType> GetAllTypes()
        {
            return new List<ProductType>()
            {
                new ProductType() {Name="Торты", Measure="кг"},
                new ProductType() {Name="Чизкейки", Measure="кг"},
                new ProductType() {Name="Печенье", Measure="набор"},
                new ProductType() {Name="Эклеры", Measure="набор"},
                new ProductType() {Name="Кейк-попсы", Measure="набор"},
                new ProductType() {Name="Капкейки", Measure="набор"}
            };
        }
    }
}