using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window, INotifyPropertyChanged
    {
        private readonly AuctionService auctionService;
        private readonly Auction newAuction;

        public Auction NewAuction
        {
            get { return newAuction; }
        }

        public string FilePathToImage { get; set; }

        public SellView()
        {
            InitializeComponent();
            DataContext = this;
            FilePathToImage = "<select image with extension .jpg>";
            var app = Application.Current as App;

            if (app != null)
            {
                var simpleMemberService = new SimpleMemberService(app.MainRepository);
                auctionService = new AuctionService(app.MainRepository, simpleMemberService);
                newAuction = new Auction
                {
                    Seller = simpleMemberService.GetCurrentMember(),
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
                };
            }
        }

        public void SelectImageButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);
                
                // only allow jpg
                if (fileInfo.Extension.EndsWith(".jpg"))
                {
                    FilePathToImage = openFileDialog.FileName;
                    newAuction.Image = File.ReadAllBytes(FilePathToImage);
                    if (FilePathToImage != null)
                    {
                        NotifyPropertyChanged("FilePathToImage");
                    }
                }
            }
        }

        public void AddAuctionClick(object sender, RoutedEventArgs e)
        {
            auctionService.Save(newAuction);
            Close();
        }

        public void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
