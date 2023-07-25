using System;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs
{
	public class ProfilTabViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public ReactiveCommand<Unit, Unit> LogOut { get; }

        Client _client;

        public Client Client {
            get => _client;
           
        }

        public ProfilTabViewModel(IScreen screen, Client c)
        {
            HostScreen = screen;

            LogOut = ReactiveCommand.Create(LogOutReq);

            _client = c;
        }

        void LogOutReq()
        {
            MessageBus.Current.SendMessage(new MessageBusType(MessageType.LogOut));
        }
    }
}

