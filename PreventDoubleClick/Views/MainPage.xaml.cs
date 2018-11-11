using PreventDoubleClick.ViewModels;
using Windows.UI.Xaml.Controls;

namespace PreventDoubleClick.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;
        public static MainPage Current { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
