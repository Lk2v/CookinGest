using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs;

public partial class HistoryTab : ReactiveUserControl<HistoryTabViewModel>
{
    public HistoryTab()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}