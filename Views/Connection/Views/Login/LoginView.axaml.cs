using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CookinGest.ViewModels;


namespace CookinGest.Views.Connection.Views;

public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}