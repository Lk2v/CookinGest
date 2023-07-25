using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using CookinGest.src.DataTemplate;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
	public class SuppliersBoardViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);


        public List<SupplierData> Data { get; set; }

        public SuppliersBoardViewModel(IScreen screen)
        {
            HostScreen = screen;
            Data = Load();
        }

        List<SupplierData> Load()
        {
            return Service.ListeFournisseurs();
        }
    }
}

