using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs;

public partial class ProfilTab : ReactiveUserControl<ProfilTabViewModel>
{
    public ProfilTab()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}