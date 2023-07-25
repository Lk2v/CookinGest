using System;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs
{
    public class HomeTabsLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ProfilTabViewModel context => new ProfilTab { DataContext = context },
            OrderTabViewModel context => new OrderTab { DataContext = context },
            CreatorTabViewModel context => new CreatorTab { DataContext = context },
            HistoryTabViewModel context => new HistoryTab { DataContext = context },

            AdminDashboardTabViewModel context => new AdminDashboardTab { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

