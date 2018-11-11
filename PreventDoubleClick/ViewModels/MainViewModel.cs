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
        public string SelectedItem { get; private set; }

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
            using (new CommandExecutionPreventer(DisplayHelloDialogCommand, ref _canDisplayHelloDialog))
            {
                Debug.WriteLine("DisplayHelloDialogCommand fired.");
                await MainPage.Current.HelloDialog.ShowAsync();
            }
        }

        private bool _canDisplaySelectedItem = true;
        public DelegateCommand<string> DisplaySelectedItemCommand { get; private set; }
        private async Task DisplaySelectedItemAsync(string selectedItem)
        {
            using (new CommandExecutionPreventer(DisplaySelectedItemCommand, ref _canDisplaySelectedItem))
            {
                Debug.WriteLine("DisplaySelectedItemCommand fired.");
                SelectedItem = selectedItem;
                await MainPage.Current.SelectedItemDialog.ShowAsync();
            }
        }
    }

    public class CommandExecutionPreventer : IDisposable
    {
        private readonly DelegateCommandBase _delegateCommand;
        private bool _canExecute;

        public CommandExecutionPreventer(DelegateCommandBase delegateCommand, ref bool canExecute)
        {
            _delegateCommand    = delegateCommand;
            _canExecute         = canExecute;

            _canExecute = false;
            _delegateCommand?.RaiseCanExecuteChanged();
        }

        public void Dispose()
        {
            _canExecute = true;
            _delegateCommand?.RaiseCanExecuteChanged();
        }
    }
}
