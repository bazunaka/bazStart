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
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.ServiceProcess;

namespace bazStart
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\downloads/");
        string unzip_dir = "C:/nginx-1.24.0";

        public MainWindow()
        {
            InitializeComponent();
            FileInfo fileInf = new FileInfo(files[0]);
            var bc = new BrushConverter();
            nginx_name.Content = fileInf.Name;
            if (fileInf.Exists)
            {
                check_distr.Content = "Дистрибутив найден";
                check_distr.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
            }
            else
            {
                check_distr.Content = "Дистрибутив не найден";
                check_distr.Foreground = (Brush)bc.ConvertFrom("#F57170");
            }
            if (Directory.Exists(unzip_dir))
            {
                check_unzip.Content = "Архив распакован";
                check_unzip.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
            }
            else
            {
                check_unzip.Content = "Архив не распакован";
                check_unzip.Foreground = (Brush)bc.ConvertFrom("#F57170");
            }
            var process = Process.GetProcessesByName("nginx");
            if (process.Length == 0)
            {
                check_start.Content = "Процесс не запущен";
                check_start.Foreground = (Brush)bc.ConvertFrom("#F57170");
                stop_nginx.IsEnabled = false;
                start_nginx.IsEnabled = true;
            }
            else
            {
                check_start.Content = "Процесс запущен";
                check_start.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
                stop_nginx.IsEnabled = true;
                start_nginx.IsEnabled = false;
            }
        }    

        private void unzip_nginx_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fileInf = new FileInfo(files[0]);
            var bc = new BrushConverter();            
            string zip_string = files[0];
            ZipFile.ExtractToDirectory(zip_string, "C:\\");
            System.Threading.Thread.Sleep(1000);
            check_unzip.Content = "Архив распакован";
            check_unzip.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
        }

        private void start_nginx_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo si = new ProcessStartInfo("D:\\GitHub\\bazStart\\bazStart\\bin\\x64\\Debug\\start_nginx.bat");
            si.Verb = "runas";          
            try
            {
                Process.Start(si);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            System.Threading.Thread.Sleep(1000);
            var bc = new BrushConverter();
            var process = Process.GetProcessesByName("nginx");
            if (process.Length == 0)
            {
                check_start.Content = "Процесс не запущен";
                check_start.Foreground = (Brush)bc.ConvertFrom("#F57170");
            }
            else
            {
                check_start.Content = "Процесс запущен";
                check_start.Foreground = (Brush)bc.ConvertFrom("#10DDC2");
                start_nginx.IsEnabled = false;
                stop_nginx.IsEnabled = true;
            }
        }

        private void stop_nginx_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            try
            {
                foreach (var item in Process.GetProcessesByName("nginx"))
                {
                    item.Kill();
                    System.Threading.Thread.Sleep(1000);
                    start_nginx.IsEnabled = true;
                    stop_nginx.IsEnabled = false;
                    check_start.Content = "Процесс не запущен";
                    check_start.Foreground = (Brush)bc.ConvertFrom("#F57170");
                }
            }
            catch (InvalidOperationException ex) 
            {
                MessageBox.Show(ex.Message);
            }   
        }
    }
}
