using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards;

public partial class ProductsBoard : ReactiveUserControl<ProductsBoardViewModel>
{
    public ProductsBoard()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}