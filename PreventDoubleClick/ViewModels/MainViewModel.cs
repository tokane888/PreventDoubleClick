using PreventDoubleClick.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PreventDoubleClick.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public List<string> Numbers = new List<string> { "Zero", "One", "Two" };

        public MainViewModel()
        {
            DisplayHelloDialogCommand = new DelegateCommand(
                async () => await DisplayHelloDialogAsync(),
                () => _canDisplayDialog);
            DisplaySelectedItemCommand = new DelegateCommand<string>(
                async s => await DisplaySelectedItemAsync(s),
                _ => _canDisplayDialog);
        }

        private bool _canDisplayDialog = true;

        public DelegateCommand DisplayHelloDialogCommand { get; private set; }
        private async Task DisplayHelloDialogAsync()
        {
            try
            {
                ToggleButtonIsEnabled();

                Debug.WriteLine("DisplayHelloDialogCommand fired.");
                // Do some work.
                await Task.Delay(1000);

                await new MessageDialog("Hello").ShowAsync();
            }
            finally
            {
                ToggleButtonIsEnabled();
            }
        }

        public DelegateCommand<string> DisplaySelectedItemCommand { get; private set; }
        private async Task DisplaySelectedItemAsync(string selectedItem)
        {
            try
            {
                ToggleButtonIsEnabled();

                Debug.WriteLine("DisplaySelectedItemCommand fired.");
                // Do some work.
                await Task.Delay(1000);

                await new MessageDialog($"{selectedItem} is selected.").ShowAsync();
            }
            finally
            {
                ToggleButtonIsEnabled();
            }
        }

        private void ToggleButtonIsEnabled()
        {
            _canDisplayDialog = !_canDisplayDialog;
            DisplayHelloDialogCommand.RaiseCanExecuteChanged();
            DisplaySelectedItemCommand.RaiseCanExecuteChanged();
        }
    }
}
