﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using FluentFTP;
using System.Windows;

namespace Products.Models
{
    static class Site
    {
        public static void UploadProducts(string login, string password)
        {
            string getFullName(string subPath)
            {
                return $"site/wwwroot/{subPath}";
            }
            List<Product> products = Database.ReadAllProducts();
            string JSONFileName = "products.js",
                   imgDirFullName = getFullName("img/products");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Product>));
            FileStream stream = File.Create(JSONFileName);
            serializer.WriteObject(stream, products);
            stream.Close();
            FtpClient client = new FtpClient("waws-prod-sn1-011.ftp.azurewebsites.windows.net", login, password);
            client.DataConnectionType = FtpDataConnectionType.AutoActive;
            client.Connect();
            client.DeleteDirectory(imgDirFullName);
            client.CreateDirectory(imgDirFullName);
            client.UploadFile(JSONFileName, getFullName($"js/{JSONFileName}"));
            File.Delete(JSONFileName);
            foreach (var product in products)
                client.UploadFile($"img/{product.ImgFileName}", $"{imgDirFullName}/{product.ImgFileName}");
            client.Disconnect();
        }
    }
}