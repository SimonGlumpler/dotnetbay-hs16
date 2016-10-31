using System;
using System.Linq;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.Data.Provider.FileStorage;
using DotNetBay.Interfaces;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IMainRepository MainRepository { get; private set; }
        public IAuctionRunner AuctionRunner { get; private set; }

        public App()
        {
            MainRepository = new FileSystemMainRepository("WPF_DotNetBay_Data.json");
            MainRepository.SaveChanges();

            PopulateWithTestData();

            AuctionRunner = new AuctionRunner(MainRepository);
            AuctionRunner.Start();
        }

        private void PopulateWithTestData()
        {
            var memberService = new SimpleMemberService(MainRepository);
            var service = new AuctionService(MainRepository, memberService);

            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();
                service.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }
    }
}
