using System;
using System.ComponentModel;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        private readonly AuctionService auctionService;
        private readonly Auction selectedAuction;

        public Auction SelectedAuction
        {
            get { return selectedAuction; }
        }

        public double YourBid { get; set; }

        public BidView(Auction selectedAuction)
        {
            InitializeComponent();
            DataContext = this;
            this.selectedAuction = selectedAuction;
            var app = Application.Current as App;
            auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            YourBid = Math.Max(SelectedAuction.CurrentPrice, SelectedAuction.StartPrice);
        }

        public void PlaceBidAuction_Click(object sender, RoutedEventArgs e)
        {
            auctionService.PlaceBid(SelectedAuction, YourBid);
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
