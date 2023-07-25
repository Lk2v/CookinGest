using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using CookinGest.ViewModels;
using ReactiveUI;

using CookinGest.Views.Home.Tabs;
using CookinGest.src;
using System.Reactive;
using System;

namespace CookinGest.Views;

public partial class HomeView : ReactiveUserControl<HomeViewModel>
{

    private Button _orderNav;
    private Button _creatorNav;

    public HomeView()
    {
        //InitializeComponent();
        this.WhenActivated(disposables => {


        });
        AvaloniaXamlLoader.Load(this);


    }
}