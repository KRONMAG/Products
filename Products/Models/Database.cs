using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Products.Models
{
    public static class Database
    {
        private static SQLiteConnection db;

        static Database()
        {
            db = new SQLiteConnection("products.db");
            db.CreateTable<Product>();
        }

        public static List<Product> ReadAllProducts()
        {
            return db.Table<Product>().ToList();
        }

        public static void InsertOrUpdateProduct(Product product)
        {
            if (product.Id == 0)
                product.Id = (db.Table<Product>().Count() > 0 ? db.Table<Product>().Max(x => x.Id) : 0) + 1;
            db.InsertOrReplace(product);
        }

        public static void DeleteProduct(Product product)
        {
            db.Delete<Product>(product.Id);
        }
    }
}