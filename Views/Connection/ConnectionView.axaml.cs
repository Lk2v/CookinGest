using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CookinGest.ViewModels;


namespace CookinGest.Views;

public partial class ConnectionView : ReactiveUserControl<ConnectionViewModel>
{
    public ConnectionView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}