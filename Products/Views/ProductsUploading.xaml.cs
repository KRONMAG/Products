using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Net.Sockets;
using Products.Models;
using FluentFTP;

namespace Products.Views
{
    public partial class ProductsUploading : Window
    {
        public string Login { get; set; }
        public Command UploadProducts { get;  }

        public ProductsUploading()
        {
            Login = string.Empty;
            UploadProducts = new Command(uploadProducts);
            DataContext = this;
            InitializeComponent();
        }

        private void uploadProducts(object parameter)
        {
            PasswordBox box = (PasswordBox)parameter;
            if (string.IsNullOrEmpty(Login))
                MessageBox.Show("Введите логин");
            else if (string.IsNullOrEmpty(box.Password))
                MessageBox.Show("Введите пароль");
            else
            {
                try
                {
                    Site.UploadProducts(Login, box.Password);
                    MessageBox.Show("Данные успешно отправлены на сервер");
                    Close();
                }
                catch (FileNotFoundException e)
                {
                    MessageBox.Show($"Не удалось найти файл {e.FileName}");
                }
                catch (SocketException e)
                {
                    MessageBox.Show("Не удалось подключиться к серверу");
                }
                catch (FtpCommandException e)
                {
                    MessageBox.Show("Учетные данные введены неверно");
                }
                catch (FtpException e)
                {
                    MessageBox.Show("Не все данные были загружены, повторите отправку");
                }
            }
        }
    }
}