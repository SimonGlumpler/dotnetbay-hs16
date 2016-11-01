using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public BidView(Auction selectedAuction)
        {
            InitializeComponent();
            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);
            DataContext = new BidViewModel(auctionService, selectedAuction);
        }
    }
}
