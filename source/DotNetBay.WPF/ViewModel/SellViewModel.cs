using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using Microsoft.Win32;

namespace DotNetBay.WPF.ViewModel
{
    class SellViewModel : ViewModelBase
    {
        private readonly IAuctionService auctionService;
        private readonly Auction newAuction;
        private string filePathToImage;

        public string FilePathToImage
        {
            get { return filePathToImage; }
            set
            {
                filePathToImage = value;
                NotifyPropertyChanged("FilePathToImage");
            }
        }

        public Auction NewAuction
        {
            get { return newAuction; }
        }

        public SellViewModel(IMemberService memberService, IAuctionService auctionService)
        {
            FilePathToImage = "<select image with extension .jpg>";

            var app = Application.Current as App;

            if (app != null)
            {
                this.auctionService = auctionService;
                newAuction = new Auction
                {
                    Seller = memberService.GetCurrentMember(),
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
                };
            }
        }

        private void SelectImageExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);

                // only allow jpg
                if (fileInfo.Extension.EndsWith(".jpg"))
                {
                    FilePathToImage = openFileDialog.FileName;
                    NewAuction.Image = File.ReadAllBytes(FilePathToImage);
                    if (FilePathToImage != null)
                    {
                        NotifyPropertyChanged("FilePathToImage");
                    }
                }
            }
        }

        private bool CanSelectImageExecute()
        {
            return true;
        }

        public ICommand SelectImage
        {
            get { return new RelayCommand(SelectImageExecute, CanSelectImageExecute); }
        }

        private void AddAuctionExecute(Window window)
        {
            auctionService.Save(NewAuction);
            window?.Close();
        }

        private bool CanAddAuctionExecute(Window window)
        {
            return true;
        }

        public ICommand AddAuction
        {
            get { return new RelayCommand<Window>(AddAuctionExecute, CanAddAuctionExecute); }
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
