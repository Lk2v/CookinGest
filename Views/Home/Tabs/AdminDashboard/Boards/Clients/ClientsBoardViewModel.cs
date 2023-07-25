using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using ReactiveUI;
using CookinGest.src.DataTemplate;
using System.Collections.ObjectModel;
using CookinGest.Windows.BankAccount;
using System.Threading.Tasks;
using CookinGest.Windows.ConfirmDialog;

using CookinGest.Windows.ShowCreatorStats;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
    public class ClientsBoardViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public ObservableCollection<ClientData> Data { get; set; }

        public int CreatorAmount
        {
            get {
                int s = 0;
                foreach (ClientData c in Data)
                {
                    if (c.EstCreateur)
                    {
                        s += 1;
                    }
                }
                return s;
            }
        }
        public ReactiveCommand<int, Unit> PromoteAdmin { get; }
        public ReactiveCommand<int, Unit> DeleteClient { get; }
        public ReactiveCommand<ClientData, Unit> CreatorStats { get; }
        public ClientsBoardViewModel(IScreen screen)
        {
            HostScreen = screen;

            PromoteAdmin = ReactiveCommand.Create<int>(PromoteAdminReq);
            DeleteClient = ReactiveCommand.CreateFromTask<int>(DeleteClientReq);
            CreatorStats = ReactiveCommand.CreateFromTask<ClientData>(CreatorStatsDialog);
            Data = Load();
        }

        void PromoteAdminReq(int idClient)
        {
            ClientData nc = Service.PromotionAdmin(idClient);
            if (nc != null)
            {
                for(int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Id == idClient)
                    {
                        Data[i] = nc;
                        break;
                    }
                }
            }
        }

        async Task DeleteClientReq(int idClient)
        {
            if (App.MainWindow != null)
            {
                var dialog = new ConfirmDialogWindow("Supprimer le client ?", "");
                DialogResult<bool> res = await dialog.ShowDialog<DialogResult<bool>>(App.MainWindow);

                if (res == null || res.Value == false) return; // la fenetre d'ajout de recette a été fermée

                if (res.Value)
                {
                    // Suppresion valider par l'utilisateur
                    Service.SupprimerClient(idClient);

                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (Data[i].Id == idClient)
                        {
                            Data.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        async Task CreatorStatsDialog(ClientData dt)
        {
            if (App.MainWindow != null)
            {
                var dialog = new ShowCreatorStatsWindow((int) dt.CreateurId!);
                await dialog.ShowDialog<ProductData>(App.MainWindow);
            }
        }

        ObservableCollection<ClientData> Load()
        {
            return new ObservableCollection<ClientData>(Service.ListeClients());
        }
    }
}

