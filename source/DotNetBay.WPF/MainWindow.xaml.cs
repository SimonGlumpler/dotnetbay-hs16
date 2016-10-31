using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Auction> auctions;
        private AuctionService auctionService;

        public ObservableCollection<Auction> Auctions
        {
            get { return auctions; }
            private set
            {
                this.auctions = value;
                NotifyPropertyChanged("Auctions");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            var app = Application.Current as App;
            auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            auctions = new ObservableCollection<Auction>(auctionService.GetAll());
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog();
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
