using PreventDoubleClick.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PreventDoubleClick.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            DisplayHelloDialogCommand = new DelegateCommand(
                async () => await DisplayHelloDialogAsync(),
                () => _canDisplayHelloDialog);
        }

        private bool _canDisplayHelloDialog = true;
        public DelegateCommand DisplayHelloDialogCommand { get; private set; }
        private async Task DisplayHelloDialogAsync()
        {
            try
            {
                Debug.WriteLine("Command fired.");
                _canDisplayHelloDialog = false;
                DisplayHelloDialogCommand.RaiseCanExecuteChanged();
                await MainPage.Current.HelloDialog.ShowAsync();
            }
            finally
            {
                _canDisplayHelloDialog = true;
                DisplayHelloDialogCommand.RaiseCanExecuteChanged();
            }
        }
    }


}
