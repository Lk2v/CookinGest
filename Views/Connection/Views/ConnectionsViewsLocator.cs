using System;
using ReactiveUI;

namespace CookinGest.Views.Connection.Views
{
    public class ConnectionViewsLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            RecentViewModel context => new RecentView { DataContext = context },
            LoginViewModel context => new LoginView { DataContext = context },
            RegisterViewModel context => new RegisterView { DataContext = context },
            
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

