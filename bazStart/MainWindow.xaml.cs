using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace bazStart
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\downloads/");
        string unzip_dir = Directory.GetCurrentDirectory();
        
        public MainWindow()
        {
            InitializeComponent();           
            FileInfo fileInf = new FileInfo(files[0]);
            unzip_dir = unzip_dir + "/nginx-1.24.0";
            var bc = new BrushConverter();
            nginx_name.Content = fileInf.Name;
            if (fileInf.Exists)
            {
                check_distr.Content = "Дистрибутив найден";               
                check_distr.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
            } else
            {
                check_distr.Content = "Дистрибутив не найден";
                check_distr.Foreground = (Brush)bc.ConvertFrom("#F57170");
            }
            if (Directory.Exists(unzip_dir))
            {
                check_unzip.Content = "Архив распакован";
                check_unzip.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
            } else
            {
                check_unzip.Content = "Архив не распакован";
                check_unzip.Foreground = (Brush)bc.ConvertFrom("#F57170");
            }
        }

        private void unzip_nginx_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fileInf = new FileInfo(files[0]);
            var bc = new BrushConverter();            
            string zip_string = files[0];
            ZipFile.ExtractToDirectory(zip_string, Directory.GetCurrentDirectory());
        }
    }
}
