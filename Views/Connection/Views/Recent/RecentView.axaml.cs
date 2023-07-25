using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CookinGest.ViewModels;


namespace CookinGest.Views.Connection.Views;

public partial class RecentView : ReactiveUserControl<RecentViewModel>
{
    public RecentView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}