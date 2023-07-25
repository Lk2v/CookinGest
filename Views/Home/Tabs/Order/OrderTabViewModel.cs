using System;
using ReactiveUI;
using CookinGest.src.DB;
using System.Reactive;

using CookinGest.src.DataTemplate;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using CookinGest.Windows;
using System.Threading.Tasks;
using CookinGest.Windows.OrderRecipe;
using CookinGest.Windows.BankAccount;
using System.Linq;
using Google.Protobuf.WellKnownTypes;

namespace CookinGest.Views.Home.Tabs
{
    public class OrderTabViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        Client _client;
        int _soldeBancaire = 0;

        public Client Client {
            get => _client;
        }

        List<RecipeData> _listData;

        public List<RecipeData> ListData {
            get => _listData;
            set => this.RaiseAndSetIfChanged(ref _listData, value);
        }

        public int SoldeBancaire {
            get => _soldeBancaire;
            set => this.RaiseAndSetIfChanged(ref _soldeBancaire, value);
        }

        public ReactiveCommand<SearchFilter, Unit> SetSearchFilter { get; }


        public ReactiveCommand<int, Unit> OrderRecipe { get; }

        public ReactiveCommand<Unit, Unit> OpenBankAccount { get;  }

        public OrderTabViewModel(IScreen screen, Client c)
        {
            HostScreen = screen;
            _client = c;
            SoldeBancaire = c.SoldeBanque;

            SetSearchFilter = ReactiveCommand.Create<SearchFilter>(SetFilter);

            OrderRecipe = ReactiveCommand.CreateFromTask<int>(OrderRecipeDialog);

            OpenBankAccount = ReactiveCommand.CreateFromTask(OpenBankAccountWindow);
            SetFilter(SearchFilter.MostPopular);

            
        }

        private async Task OpenBankAccountWindow()
        {
            if (App.MainWindow != null)
            {
                var dialog = new BankAccountWindow(Client.SoldeBanque);
                DialogResult<bool> res = await dialog.ShowDialog<DialogResult<bool>>(App.MainWindow);

                if (res == null || res.Value) return; // la fenetre d'ajout de recette a été fermée

                if (Client.RechargerCompteBancaire())
                {
                    SoldeBancaire = Client.SoldeBanque;
                }
            }
        }

        async Task OrderRecipeDialog(int recetteId)
        {
            
            if (App.MainWindow != null)
            {
                RecipeData recipe = ListData.Find(x => (x.Id == recetteId));

                if (recipe == null) return;

                var dialog = new OrderRecipeWindow(recipe);
                CommandDetails res = await dialog.ShowDialog<CommandDetails>(App.MainWindow);

                if (res == null) return; // la fenetre d'ajout de recette a été fermée

                Client.Commander(recetteId, res.MethodPayement == PaymentMethod.SoldeCreateur);
                SoldeBancaire = Client.SoldeBanque;
            }
        }

        void SetFilter(SearchFilter f)
        {
            ListData = Service.ListeRecettes(f);
        }

    }

    
}

