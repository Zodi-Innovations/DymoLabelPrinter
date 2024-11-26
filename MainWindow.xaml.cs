﻿using DymoSDK.Implementations;
using DymoSDK.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }
        private FileSystemWatcher fileWatcher;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;

        
            AutoPrintLabel();

            StartFileWatcher();
        }

        private async void AutoPrintLabel()
        {
            // Laad het bestand en de printers opnieuw
            await LoadAndCheckFileAndPrinters();

            // Controlee
            if (ViewModel.Printers.Any())  
            {
                ViewModel.SelectedPrinter = ViewModel.Printers.First();

                if (ViewModel.dymoSDKLabel != null)
                {
                    string defaultLabelFile = @"C:\Users\ismai\Downloads\demo.dymo";  
                    ViewModel.dymoSDKLabel.LoadLabelFromFilePath(defaultLabelFile);

                    ViewModel.PrintLabelAction();

                   
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void StartFileWatcher()
        {
            string labelFilePath = @"C:\Users\ismai\Downloads\demo.dymo";  
            string directory = Path.GetDirectoryName(labelFilePath);  

            // Configureert FileSystemWatcher 
            fileWatcher = new FileSystemWatcher(directory)
            {
                Filter = Path.GetFileName(labelFilePath), 
                NotifyFilter = NotifyFilters.LastWrite 
            };

            fileWatcher.Changed += FileWatcher_Changed;

            fileWatcher.EnableRaisingEvents = true;
        }

        private async void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(500);  

            await LoadAndCheckFileAndPrinters();

            AutoPrintLabel();
        }

        private async Task LoadAndCheckFileAndPrinters()
        {
            string defaultLabelFile = @"C:\Users\ismai\Downloads\demo.dymo";  
            ViewModel.dymoSDKLabel.LoadLabelFromFilePath(defaultLabelFile);

            await ViewModel.LoadPrinters();

            if (!ViewModel.Printers.Any())
            {
                StatusText.Text = "Geen printers gevonden.";
            }
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            fileWatcher?.Dispose();
        }
    }
}