using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using ReactiveUI;

using CookinGest.Windows;
using System.Reactive.Linq;
using CookinGest.Windows.PublishRecipe;
using CookinGest.Windows.ConvertCreatorPoints;
using Avalonia;
using CookinGest.src.DataTemplate;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CookinGest.Views.Home.Tabs
{
	public class HistoryTabViewModel : ReactiveObject, IRoutableViewModel
    {
        Client _client;

        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public Client Client
        {
            get => _client;

        }

        public List<OrderHistory> HistoryList { get; set; }

        public HistoryTabViewModel(IScreen screen, Client c)
        {
            HostScreen = screen;
            _client = c;

            LoadHistory();
        }


        void LoadHistory()
        {
            HistoryList = Client.ListeCommandesClient();
        }

    }
}

