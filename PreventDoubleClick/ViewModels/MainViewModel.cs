using PreventDoubleClick.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PreventDoubleClick.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public List<string> Numbers = new List<string> { "Zero", "One", "Two" };

        public MainViewModel()
        {
            DisplayHelloDialogCommand = new DelegateCommand(
                async () => await DisplayHelloDialogAsync(),
                () => _canDisplayHelloDialog);
            DisplaySelectedItemCommand = new DelegateCommand<string>(
                async s => await DisplaySelectedItemAsync(s),
                _ => _canDisplaySelectedItem);
        }

        private bool _canDisplayHelloDialog = true;
        public DelegateCommand DisplayHelloDialogCommand { get; private set; }
        private async Task DisplayHelloDialogAsync()
        {
            try
            {
                _canDisplayHelloDialog = false;
                DisplayHelloDialogCommand.RaiseCanExecuteChanged();

                Debug.WriteLine("DisplayHelloDialogCommand fired.");
                // Do some work.
                await Task.Delay(1000);

                // Display Content dialog.
                await MainPage.Current.HelloDialog.ShowAsync();
            }
            finally
            {
                _canDisplayHelloDialog = true;
                DisplayHelloDialogCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _canDisplaySelectedItem = true;
        public DelegateCommand<string> DisplaySelectedItemCommand { get; private set; }
        private async Task DisplaySelectedItemAsync(string selectedItem)
        {
            try
            {
                _canDisplaySelectedItem = false;
                DisplaySelectedItemCommand.RaiseCanExecuteChanged();

                Debug.WriteLine("DisplaySelectedItemCommand fired.");
                // Do some work.
                await Task.Delay(1000);

                // Display Content dialog.
                await MainPage.Current.SelectedItemDialog.ShowAsync();
            }
            finally
            {
                _canDisplaySelectedItem = true;
                DisplaySelectedItemCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
