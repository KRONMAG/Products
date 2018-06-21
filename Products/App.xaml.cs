using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Products.Models;

namespace Products
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.ProcessExit += onProcessExit;
        }

        private void onProcessExit(object sender, EventArgs e)
        {
            List<Product> products = Database.ReadAllProducts();
            string[] names = Directory.GetFiles("img");
            foreach (var name in names)
                try
                {
                    if ((from product in products where product.ImgFileName == Path.GetFileName(name) select product).Count() == 0)
                        File.Delete(name);
                }
                catch
                {

                }
        }
    }
}