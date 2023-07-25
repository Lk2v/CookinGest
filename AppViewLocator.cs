using System;
using CookinGest.ViewModels;
using CookinGest.Views;
using ReactiveUI;

namespace CookinGest
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            HomeViewModel context => new HomeView { DataContext = context },

            ConnectionViewModel context => new ConnectionView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

