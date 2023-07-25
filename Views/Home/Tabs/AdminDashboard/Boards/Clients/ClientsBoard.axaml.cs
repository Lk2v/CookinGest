using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards;

public partial class ClientsBoard : ReactiveUserControl<ClientsBoardViewModel>
{
    public ClientsBoard()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}