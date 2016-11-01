using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class AuctionViewModel : ViewModelBase
    {
        private const double Tolerance = 0.002;

        private Auction auction;
        private double currentPrice;
        private double bidCount;
        private string currentWinner;
        private DateTime? closedDateLocal;
        private string finalWinner;
        private string status;
        private bool isRunning;

        public AuctionViewModel(Auction auction)
        {
            this.auction = auction;
            Apply();
        }

        public string Title
        {
            get { return auction.Title; }
        }

        public byte[] Image
        {
            get { return auction.Image; }
        }

        public double StartPrice
        {
            get { return auction.StartPrice; }
        }

        public DateTime StartDateTimeLocal
        {
            get { return auction.StartDateTimeUtc.ToLocalTime(); }
        }

        public DateTime EndDateTimeLocal
        {
            get { return auction.EndDateTimeUtc.ToLocalTime(); }
        }

        public string Seller
        {
            get { return auction.Seller.DisplayName; }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public double CurrentPrice
        {
            get { return currentPrice; }
            set
            {
                if (Math.Abs(currentPrice - value) > Tolerance)
                {
                    currentPrice = value;
                    NotifyPropertyChanged("CurrentPrice");
                }
            }
        }

        public double BidCount
        {
            get { return bidCount; }
            set
            {
                if (Math.Abs(bidCount - value) > Tolerance)
                {
                    bidCount = value;
                    NotifyPropertyChanged("BidCount");
                }
            }
        }

        public string CurrentWinner
        {
            get { return currentWinner; }
            set
            {
                currentWinner = value;
                NotifyPropertyChanged("CurrentWinner");
            }
        }

        public DateTime? ClosedDateLocal
        {
            get { return closedDateLocal?.ToLocalTime(); }
            set { closedDateLocal = value; }
        }

        public string FinalWinner
        {
            get { return finalWinner; }
            set
            {
                finalWinner = value;
                NotifyPropertyChanged("FinalWinner");
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    NotifyPropertyChanged("IsRunning");
                }
            }
        }

        public Auction Auction
        {
            get { return auction; }
            set
            {
                auction = value;
                NotifyPropertyChanged("Auction");
            }
        }

        public void Update(Auction newAuction)
        {
            auction = newAuction;
        }

        private void Apply()
        {
            Status = auction.IsClosed ? "Closed" : "Valid";
            CurrentPrice = auction.CurrentPrice;
            BidCount = auction.Bids.Count;
            IsRunning = auction.IsRunning;

            if (auction.ActiveBid != null)
            {
                CurrentWinner = auction.ActiveBid.Bidder.DisplayName;
            }

            if (auction.CloseDateTimeUtc > DateTime.MinValue)
            {
                ClosedDateLocal = auction.CloseDateTimeUtc.ToLocalTime();
            }

            if (auction.Winner != null)
            {
                FinalWinner = auction.Winner.DisplayName;
            }
        }

        public void PlaceBidExcecute()
        {
            var bidView = new BidView(auction);
            bidView.ShowDialog();
        }

        public bool CanPlaceBidExecute()
        {
            return true;
        }

        public ICommand PlaceBid
        {
            get { return new RelayCommand(PlaceBidExcecute, CanPlaceBidExecute); }
        }
    }
}
