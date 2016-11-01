using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF.ViewModel
{
    class BidViewModel : ViewModelBase
    {
        private readonly IAuctionService auctionService;
        private readonly Auction selectedAuction;
        
        public Auction SelectedAuction
        {
            get { return selectedAuction; }
        }

        public string YourBid { get; set; }

        public BidViewModel(IAuctionService auctionService, Auction selectedAuction)
        {
            this.selectedAuction = selectedAuction;
            this.auctionService = auctionService;
            YourBid = Math.Max(SelectedAuction.CurrentPrice, SelectedAuction.StartPrice).ToString();
        }

        private void PlaceBidExecute(Window window)
        {
            auctionService.PlaceBid(SelectedAuction, double.Parse(YourBid));
            window?.Close();
        }

        private bool CanPlaceBidExecute(Window window)
        {
            return true;
        }

        public ICommand PlaceBid
        {
            get { return new RelayCommand<Window>(PlaceBidExecute, CanPlaceBidExecute); }
        }

        private void CloseWindowExecute(Window window)
        {
            window?.Close();
        }

        private bool CanCloseWindowExecute(Window window)
        {
            return true;
        }

        public ICommand CloseWindow
        {
            get { return new RelayCommand<Window>(CloseWindowExecute, CanCloseWindowExecute); }
        }
    }
}
