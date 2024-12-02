using DymoSDK.Implementations;
using DymoSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
            }
        }

        public IEnumerable<IPrinter> Printers { get; set; }
        public IPrinter SelectedPrinter { get; set; }
        public DymoSDK.Interfaces.IDymoLabel dymoSDKLabel { get; set; }

        public MainViewModel()
        {
            try
            {
                DymoSDK.App.Init();
                dymoSDKLabel = DymoLabel.Instance;
                StatusMessage = "Dymo SDK succesvol geïnitialiseerd.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Fout bij het initialiseren van de Dymo SDK: {ex.Message}";
            }
        }

        public async Task LoadPrinters()
        {
            int maxRetries = 1;
            int retryCount = 0;
            bool printersLoaded = false;

            while (retryCount < maxRetries && !printersLoaded)
            {
                try
                {
                    await Task.Delay(1000);  

                    Printers = await DymoPrinter.Instance.GetPrinters();

                    if (Printers == null)
                    {
                        StatusMessage = "De printerlijst is null.";
                    }
                    else
                    {
                        StatusMessage = $"Aantal printers gevonden: {Printers.Count()}";
                    }

                    if (Printers != null && Printers.Any())
                    {
                        printersLoaded = true;
                        StatusMessage = $"{Printers.Count()} printer(s) gevonden.";
                    }
                    else
                    {
                        retryCount++;
                        StatusMessage = $"Geen printers gevonden, poging {retryCount}/{maxRetries}...";

                        await Task.Delay(2000);
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Fout bij het ophalen van printers: {ex.Message}";
                    retryCount++;
                    await Task.Delay(2000);
                }
            }

            if (!printersLoaded)
            {
                StatusMessage = "Kan geen printers vinden na meerdere pogingen.";
            }
        }

        public void LoadLabelFromFilePath(string filePath)
        {
            dymoSDKLabel.LoadLabelFromFilePath(filePath);
        }

        public void PrintLabelAction()
        {
            int copies = 0;
            if (SelectedPrinter != null)
            {
                DymoPrinter.Instance.PrintLabel(dymoSDKLabel, SelectedPrinter.Name, copies);
            }
        }
    }
}
