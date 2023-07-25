using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using ReactiveUI;
using CookinGest.src.DataTemplate;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
    public class StatsBoardViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);


        public List<TopData<ClientData>> TopClients { get; set; }

        public List<TopData<ClientData>> TopCreator { get; set; }

        public List<RecipeData> TopRecipe { get; set; }

        public StatsBoardViewModel(IScreen screen)
        {
            HostScreen = screen;

            Load();
        }

        public void Load()
        {
            TopClients = Service.TopClientSemaine();
            TopCreator = Service.TopCreateurSemaine();
            TopRecipe = Service.TopRecetteSemaine();
        }
    }

    
}

