using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CookinGest.ViewModels;
using CookinGest.Views;
using CookinGest.Windows;
using CookinGest.Windows.PublishRecipe;
using ReactiveUI;

namespace CookinGest;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => {
            
        });
        AvaloniaXamlLoader.Load(this);


    }

    private async Task DoShowDialogAsync()
    {
        var dialog = new PublishRecipeWindow();
        //dialog.DataContext = interaction.Input;
        Console.WriteLine("HELELE");

        await dialog.ShowDialog(this);
        //interaction.SetOutput(result);
    }
}