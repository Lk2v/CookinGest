using System;
using System.Reactive;
using CookinGest.src;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using CookinGest.src.StoreConnections;
using CookinGest.Views;
using ReactiveUI;

namespace CookinGest.Views.Connection.Views
{
    public class RegisterViewModel : ReactiveObject, IRoutableViewModel
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        private string _firstname;
        private string _lastname;
        private string _telephone;
        private string _email;
        private string _password;
        private string _adresse;

        private bool _isLoginEnabled;
        private bool _isBusy;

        //

        public string FirstName
        {
            get => _firstname;
            set => this.RaiseAndSetIfChanged(ref _firstname, value);
        }

        public string LastName
        {
            get => _lastname;
            set => this.RaiseAndSetIfChanged(ref _lastname, value);
        }

        public string Email
        {
            get => _email;
            set  => this.RaiseAndSetIfChanged(ref _email, value.ToLower());
        }

        public string Telephone
        {
            get => _telephone;
            set => this.RaiseAndSetIfChanged(ref _telephone, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Adresse
        {
            get => _adresse;
            set => this.RaiseAndSetIfChanged(ref _adresse, value);
        }

        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            set => this.RaiseAndSetIfChanged(ref _isLoginEnabled, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

        public ReactiveCommand<Unit, Unit> SwitchLoginView { get; }

        public RegisterViewModel(IScreen screen)
        {
            HostScreen = screen;

            RegisterCommand = ReactiveCommand.Create(Register);
            SwitchLoginView = ReactiveCommand.Create(GoLoginView);
        }

        void Register()
        {
            decimal tel = Convert.ToDecimal(Telephone.Substring(6).Replace(" ", string.Empty));

            bool ok;
            ClientData acc;

            (ok, acc) = Service.CreeClient(Email, Password, FirstName, LastName, tel, Adresse);
            if(ok)
            {
                StoreConnections.EnregistrerConnection(acc);
                MessageBus.Current.SendMessage(new MessageBusType(MessageType.LoginSucess));
            }
        }

        void GoLoginView()
        {
            MessageBus.Current.SendMessage(new MessageBusType(MessageType.SwitchLoginView));
        }
        
    }
}

