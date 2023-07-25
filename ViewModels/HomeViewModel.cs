using System;
using ReactiveUI;

using CookinGest.src.DB;
using System.Reactive;

using CookinGest.Views.Home.Tabs;

using System.Windows.Input;

namespace CookinGest.ViewModels
{
    public class HomeViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        public Client client;

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public Client Client {
            get => client;       
        }

        public RoutingState Router { get; } = new RoutingState();

        public Tabs _tab;
        // The command that navigates a user to first view model.
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }


        // ICommand

        private ICommand _handleSwitchTab;

        public ICommand HandleSwitchTab
        {
            get => _handleSwitchTab;
            set => this.RaiseAndSetIfChanged(ref _handleSwitchTab, value);
        }


        public HomeViewModel(IScreen screen, Client c)
        {
            HostScreen = screen;
            client = c;

            SwitchTab(Tabs.Home);


            HandleSwitchTab = ReactiveCommand.Create<Tabs>(SwitchTab);


        }

        private void SwitchTab(Tabs tab) {
            if (tab == _tab) return;

            switch(tab) {
                case Tabs.Profil:
                    Router.Navigate.Execute(new ProfilTabViewModel(this, c: Client));
                    break;

                case Tabs.Home:
                    Router.Navigate.Execute(new OrderTabViewModel(this, c: Client));
                    break;

                case Tabs.Creator:
                    Router.Navigate.Execute(new CreatorTabViewModel(this, c: Client));
                    break;

                case Tabs.History:
                    Router.Navigate.Execute(new HistoryTabViewModel(this, c: Client));
                    break;


                case Tabs.AdminDashboard:
                    Router.Navigate.Execute(new AdminDashboardTabViewModel(this));
                    break;

                default:
                    return;
            }

            _tab = tab;
        }
    }


    public enum Tabs {
        Profil,
        Home,
        Creator,
        History,

        AdminDashboard,
    }
}

