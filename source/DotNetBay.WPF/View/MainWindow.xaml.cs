using System.Windows;
using DotNetBay.Core;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var app = Application.Current as App;
            var auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            DataContext = new MainViewModel(app.AuctionRunner.Auctioneer, auctionService);
        }
    }
}
