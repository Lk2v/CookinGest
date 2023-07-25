using System;
using CookinGest.ViewModels;
using CookinGest.Views;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
    public class AdminBoardsLocator : ReactiveUI.IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            StatsBoardViewModel context => new StatsBoard { DataContext = context },

            ClientsBoardViewModel context => new ClientsBoard { DataContext = context },
            ProductsBoardViewModel context => new ProductsBoard { DataContext = context },
            RecipesBoardViewModel context => new RecipesBoard { DataContext = context },
            SuppliersBoardViewModel context => new SuppliersBoard { DataContext = context },


            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

