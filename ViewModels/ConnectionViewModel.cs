using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using CookinGest.src;
using CookinGest.src.DB;
using CookinGest.src.StoreConnections;
using CookinGest.Views.Connection.Views;
using ReactiveUI;


namespace CookinGest.ViewModels
{
    public class ConnectionViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        //
        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }


        public ReactiveCommand<Unit, Unit> SwitchRegisterView { get; }

        Views _stateView;

        Views StateView
        {
            get => _stateView;
            set
            {
                _stateView = value;
                BottomBtnIndicator(value);
            }
        }

        string _bottomBtnText = "";
        public string BottomButtonText
        {
            get => _bottomBtnText;
            set => this.RaiseAndSetIfChanged(ref _bottomBtnText, value);
        }

        MessageBus ConnectionBusState;

        public ConnectionViewModel(IScreen screen)
        {
            HostScreen = screen;

            SwitchRegisterView = ReactiveCommand.Create(Switch);

            ConnectionBusState = new MessageBus();

            ConnectionBusState.Listen<ConnectionStateMessage>().Subscribe(StateMessage);


            GoLogin();

            StoreConnections.ConnectionsRecentes();
        }

        void StateMessage(ConnectionStateMessage d)
        {
            switch(d.Type)
            {
                case ConnectionStateEnum.Login:
                    GoLoginView();
                    break;

                case ConnectionStateEnum.FastLogin:
                    GoLoginView(d.Data);
                    break;

                case ConnectionStateEnum.LoginList:
                    GoConnectionsList();
                    break;
            }
        }

        void Switch()
        {
            switch(StateView)
            {
                case Views.Register:
                    GoLogin();
                    break;

                default:
                    GoRegisterView();
                    break;
            }
        }

        void GoLogin()
        {
            List<AccountData> accList = StoreConnections.ConnectionsRecentes();
            if(accList.Count == 0)
            {
                GoLoginView();
            } else
            {
                GoConnectionsList(accList);
            }
        }

        void GoConnectionsList(List<AccountData>? acc = null)
        {
            if(acc == null)
            {
                acc = StoreConnections.ConnectionsRecentes();
            }

            Router.Navigate.Execute(new RecentViewModel(this, ConnectionBusState, acc!));
            StateView = Views.ConnectionsList;
        }

        void BottomBtnIndicator(Views v)
        {
            BottomButtonText = (v == Views.Register) ? "J'ai déjà un compte" : "Je n'ai pas de compte"; 
        }

        void GoLoginView(AccountData? acc = null)
        {
            bool goBack = StoreConnections.NombreConnectionsRecentes() > 0;
            Router.Navigate.Execute(new LoginViewModel(this, ConnectionBusState, acc, goBack));
            StateView = Views.Login;
        }

        void GoRegisterView()
        {
            Router.Navigate.Execute(new RegisterViewModel(this));
            StateView = Views.Register;
        }
    }

    enum Views
    {
        ConnectionsList,
        Login,
        Register,
    }
}

