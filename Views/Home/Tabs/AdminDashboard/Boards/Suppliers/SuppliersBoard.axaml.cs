using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards;

public partial class SuppliersBoard : ReactiveUserControl<SuppliersBoardViewModel>
{
    public SuppliersBoard()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}