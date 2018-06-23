using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Win32;
using Products.Models;

namespace Products.Views
{
    public partial class ProductCreation : Window, INotifyPropertyChanged
    {
        public ProductCreation(Product product = null)
        {
            Types = ProductType.GetAllTypes();
            if (product == null)
            {
                if (Types.Count > 0)
                    Type = Types[0].Name;
                ProductName = Description = Price = ImgFileName = string.Empty;
            }
            else
            {
                id = product.Id;
                ProductName = product.Name;
                Type = product.Type;
                Description = product.Description;
                Price = product.Price.ToString();
                ImgFileName = product.ImgFileName;
            }
            OpenImage = new Command(openImage);
            SaveProduct = new Command(saveProduct);
            DataContext = this;
            InitializeComponent();
        }

        #region Свойства для привязки данных
        public List<ProductType> Types { get; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImgFileName { get; set; }
        public Command OpenImage { get; }
        public Command SaveProduct { get; }

        private int id;
        #endregion

        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Методы команд нажатия кнопок
        private void openImage(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Файлы изображений (*.bmp, *.gif, *.jpg, *.png)|*.bmp;*.gif;*.jpg;*.png|Все файлы (*.*)|*.*"
            };
            if (fileDialog.ShowDialog().Value)
            {
                Directory.CreateDirectory("img");
                int? max = null;
                string[] names = Directory.GetFiles("img");
                foreach (var name in names)
                    if (int.TryParse(Path.GetFileNameWithoutExtension(name), out int result) && (max == null || result > max))
                        max = result;
                ImgFileName = $"{(max == null ? 1 : max + 1)}{Path.GetExtension(fileDialog.FileName)}";
                File.Copy(fileDialog.FileName, $"img\\{ImgFileName}");
                onPropertyChanged("ImgFileName");
            }
            fileDialog = null;
        }

        private void saveProduct(object parameter)
        {
            if (string.IsNullOrEmpty(ImgFileName))
            {
                MessageBox.Show("Укажите путь к изображению изделия");
                return;
            }
            if (string.IsNullOrEmpty(ProductName))
            {
                MessageBox.Show("Введите название изделия");
                return;
            }
            if (Type == null)
            {
                MessageBox.Show("Выберите тип изделия");
                return;
            }
            if (!decimal.TryParse(Price, out decimal result))
            {
                MessageBox.Show("Введите корректную цену единицы изделия");
                return;
            }
            Database.InsertOrUpdateProduct(
                new Product()
                {
                    Id = id,
                    Name = ProductName,
                    Type = Type,
                    Description = Description,
                    Price = decimal.Round(result, 2),
                    ImgFileName = ImgFileName
                });
            Close();
        }
        #endregion
    }
}