using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.ShowCreatorRecipeOrderHistory
{
    public class ShowCreatorRecipeOrderHistoryViewModel : ReactiveObject
    {
        List<OrderHistory> _list;

        public List<OrderHistory> List {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        Client _creatorClient;
        public Client CreatorClient
        {
            get => _creatorClient;
            set => this.RaiseAndSetIfChanged(ref _creatorClient, value);
        }

        public ShowCreatorRecipeOrderHistoryViewModel(Client c)
        {
            CreatorClient = c;
        }

        List<OrderHistory> Load()
        {
            return CreatorClient.ObtenirListeCommandesRecettesCreateur();
        }

        internal void OnWindowShown()
        {
            List = Load();
        }
    }
}

