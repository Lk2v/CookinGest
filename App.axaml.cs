using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CookinGest.src.DB;
using CookinGest.ViewModels;
using Mysqlx;

namespace CookinGest;

public partial class App : Application
{
    public static MainWindow? MainWindow { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        bool initOk;
        // Init API ici
        do
        {
            initOk = Service.Initialiser();
            if (!initOk)
            {
                AfficherErreur("Erreur initialisation BDD");
                Thread.Sleep(2000);
            }
            
        } while (!initOk);
        

        Console.WriteLine("Service en ligne");
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            App.MainWindow = desktop.MainWindow as MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    void AfficherErreur(string err)
    {
        Console.WriteLine("ERREUR : " + err);
    }

}