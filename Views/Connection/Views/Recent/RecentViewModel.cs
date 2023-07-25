using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using CookinGest.src;
using CookinGest.src.DB;
using CookinGest.src.StoreConnections;
using ReactiveUI;


namespace CookinGest.Views.Connection.Views
{
    public class RecentViewModel : ReactiveObject, IRoutableViewModel
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);


        public ReactiveCommand<AccountData, Unit> SelectAccount { get; }
        public ReactiveCommand<Unit, Unit> ToggleDeleteMode { get; }

        public ReactiveCommand<AccountData, Unit> DeleteFromListAccount { get; }

        ObservableCollection<AccountData> _listAccounts;
        public ObservableCollection<AccountData> ListAccounts
        {
            get => _listAccounts;
            set => this.RaiseAndSetIfChanged(ref _listAccounts, value);
        }

        IMessageBus MessagesBus;

        public ReactiveCommand<Unit, Unit> Login { get; }

        bool _deleteMode = false;
        public bool DeleteMode
        {
            get => _deleteMode;
            set => this.RaiseAndSetIfChanged(ref _deleteMode, value);
        }

        public RecentViewModel(IScreen screen, IMessageBus bus, List<AccountData> accList)
        {
            HostScreen = screen;
            MessagesBus = bus;

            SelectAccount = ReactiveCommand.Create<AccountData>(SelectAccountReq);
            ToggleDeleteMode = ReactiveCommand.Create(ToggleDeleteModeReq);

            DeleteFromListAccount = ReactiveCommand.Create<AccountData>(DeleteFromListAccountReq);
            Login = ReactiveCommand.Create(LoginReq);

            ListAccounts = new ObservableCollection<AccountData>(accList);
        }

        void LoginReq()
        {
            MessagesBus.SendMessage(
                new ConnectionStateMessage(ConnectionStateEnum.Login)
            );
        }

        void SelectAccountReq(AccountData accDt)
        {
            MessagesBus.SendMessage(
                new ConnectionStateMessage(ConnectionStateEnum.FastLogin, accDt)
            );
        }

        void ToggleDeleteModeReq()
        {
            DeleteMode = !DeleteMode;
        }

        void DeleteFromListAccountReq(AccountData accDt)
        {
            if(StoreConnections.SupprimerConnection(accDt))
            {
                ListAccounts.Remove(accDt);

                if(ListAccounts.Count == 0)
                {
                    LoginReq();
                }
            }
        }
    }
}

