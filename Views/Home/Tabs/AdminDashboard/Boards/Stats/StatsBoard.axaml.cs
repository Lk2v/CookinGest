using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CookinGest.Views.Home.Tabs.AdminDashboard.Boards;
using ReactiveUI;


namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards;
                                                    
public partial class StatsBoard : ReactiveUserControl<StatsBoardViewModel>
{
    public StatsBoard()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}