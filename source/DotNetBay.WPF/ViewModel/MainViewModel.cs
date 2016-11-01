using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AuctionViewModel> auctions;
        private readonly IAuctionService auctionService;

        public ObservableCollection<AuctionViewModel> Auctions
        {
            get { return auctions; }
            private set
            {
                auctions = value;
                NotifyPropertyChanged("Auctions");
            }
        }

        public MainViewModel(IAuctioneer auctioneer, IAuctionService auctionService)
        {
            this.auctionService = auctionService;
            auctions = new ObservableCollection<AuctionViewModel>();

            // register for events
            auctioneer.AuctionEnded += (sender, args) => { ApplyChanges(args.Auction); };
            auctioneer.AuctionStarted += (sender, args) => { ApplyChanges(args.Auction); };
            auctioneer.BidAccepted += (sender, args) => { ApplyChanges(args.Auction); };
            auctioneer.BidDeclined += (sender, args) => { ApplyChanges(args.Auction); };

            // populate auctions list
            var allAuctions = auctionService.GetAll();
            foreach (var auction in allAuctions)
            {
                var auctionVm = new AuctionViewModel(auction);
                Auctions.Add(auctionVm);
            }
        }

        private void ShowSellViewExecute()
        {
            var sellView = new SellView();
            sellView.ShowDialog();
            var allAuctions = auctionService.GetAll();
            var newAuctions = allAuctions.Where(a => auctions.All(vm => vm.Auction != a));

            foreach (var auction in newAuctions)
            {
                var auctionVm = new AuctionViewModel(auction);
                auctions.Add(auctionVm);
            }
        }

        private bool CanShowSellViewExecute()
        {
            return true;
        }

        public ICommand ShowSellView
        {
            get { return new RelayCommand(ShowSellViewExecute, CanShowSellViewExecute); }
        }

        private void ApplyChanges(Auction auction)
        {
            var auctionVm = auctions.FirstOrDefault(vm => vm.Auction == auction);
            auctionVm?.Update(auction);
        }
    }
}
