using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
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

            app.AuctionRunner.Auctioneer.AuctionEnded += AuctioneerAuctionEnded;
            app.AuctionRunner.Auctioneer.AuctionStarted += AuctioneerAuctionStarted;
            app.AuctionRunner.Auctioneer.BidAccepted += AuctioneerBidAccepted;
            app.AuctionRunner.Auctioneer.BidDeclined += AuctioneerBidDeclined;

            DataContext = this;
        }

        private void AuctioneerAuctionEnded(object sender, AuctionEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerAuctionStarted(object sender, AuctionEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerBidAccepted(object sender, ProcessedBidEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerBidDeclined(object sender, ProcessedBidEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void SellViewButton_Click(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog();
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void BidViewButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAuction = (Auction)this.AuctionsDataGrid.SelectedItem;
            var bidView = new BidView(currentAuction);
            bidView.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class StatusBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool) value == true)
                {
                    return "Closed";
                }
                else
                {
                    return "Valid";
                }
            }

            throw new ArgumentException("value should be bool");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
