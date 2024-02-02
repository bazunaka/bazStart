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
        
        public MainWindow()
        {
            InitializeComponent();           
            FileInfo fileInf = new FileInfo(files[0]);
            nginx_name.Content = fileInf.Name;
        }

        private void unzip_nginx_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fileInf = new FileInfo(files[0]);
            string zip_string = files[0];
            ZipFile.ExtractToDirectory(zip_string, Directory.GetCurrentDirectory());
        }
    }
}
