using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using Products.Models;

namespace Products.Views
{
    public partial class ProductsDisplay : Window, INotifyPropertyChanged
    {
        public ProductsDisplay()
        {
            Types = ProductType.GetAllTypes();
            if (Types.Count > 0) selectedType = Types[0];
            products = Database.ReadAllProducts();
            CreateProduct = new Command(createProduct);
            EditProduct = new Command(editProduct);
            DeleteProduct = new Command(deleteProduct);
            SendProducts = new Command(sendProducts);
            DataContext = this;
            InitializeComponent();
        }

        #region Свойства для привязки данных
        public List<ProductType> Types { get; }
        public ProductType SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                onPropertyChanged("SelectedType");
                onPropertyChanged("DisplayedProducts");
            }
        }
        public List<Product> DisplayedProducts
        {
            get
            {
                return (from product in products where product.Type == selectedType.Name select product).ToList();
            }
        }
        public Product SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                onPropertyChanged("SelectedProductMenuVisibility");
            }
        }
        public Visibility SelectedProductMenuVisibility
        {
            get
            {
                return selectedProduct == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public Command EditProduct { get; set; }
        public Command DeleteProduct { get; set; }
        public Command CreateProduct { get; set; }
        public Command SendProducts { get; set; }

        private ProductType selectedType;
        private Product selectedProduct;
        private List<Product> products;
        #endregion

        #region Реализация интерфейса InotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Методы команд нажатия кнопок меню
        private void createProduct(object parameter)
        {
            Window creationWindow = new ProductCreation();
            creationWindow.Closed += updateShownProducts;
            creationWindow.ShowDialog();
        }

        private void editProduct(object parameter)
        {
            Window editionWindow = new ProductCreation(selectedProduct);
            editionWindow.Closed += updateShownProducts;
            editionWindow.ShowDialog();
        }

        private void deleteProduct(object parameter)
        {
            Database.DeleteProduct(selectedProduct);
            updateShownProducts(null, null);
        }

        private void updateShownProducts(object sender, EventArgs e)
        {
            products = Database.ReadAllProducts();
            onPropertyChanged("DisplayedProducts");
        }

        private void sendProducts(object parameter)
        {
            (new ProductsUploading()).ShowDialog();
        }
        #endregion
    }
}