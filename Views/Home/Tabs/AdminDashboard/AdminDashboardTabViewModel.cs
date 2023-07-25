using System;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using ReactiveUI;

using CookinGest.Views.Home.Tabs.AdminDashboard.Boards;

namespace CookinGest.Views.Home.Tabs
{
	public class AdminDashboardTabViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<Boards, Unit> HandleSwitchBoard { get; }

        public AdminDashboardTabViewModel(IScreen screen)
        {
            HostScreen = screen;

            SwitchBoard(Boards.Tops);
            HandleSwitchBoard = ReactiveCommand.Create<Boards>(SwitchBoard);
        }

        void SwitchBoard(Boards b)
        {
            switch(b)
            {
                case Boards.Tops:
                    Router.Navigate.Execute(new StatsBoardViewModel(this));
                    break;

                case Boards.Clients:
                    Router.Navigate.Execute(new ClientsBoardViewModel(this));
                    break;


                case Boards.Products:
                    Router.Navigate.Execute(new ProductsBoardViewModel(this));
                    break;

                case Boards.Recipes:
                    Router.Navigate.Execute(new RecipesBoardViewModel(this));
                    break;

                case Boards.Suppliers:
                    Router.Navigate.Execute(new SuppliersBoardViewModel(this));
                    break;
            }
        }
    }

    public enum Boards
    {
        Tops,
        Clients,

        Products,
        Recipes,
        Suppliers
    }
}

