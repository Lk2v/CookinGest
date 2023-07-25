using System;
using System.Reactive;
using System.Reactive.Linq;
using CookinGest.src;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using CookinGest.src.StoreConnections;
using ReactiveUI;


namespace CookinGest.Views.Connection.Views
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        //
        private string _email;
        private string _password;
        private bool _isLoginEnabled;
        private bool _isBusy;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
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

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        AccountData? _loginProfil;
        public AccountData? LoginProfil
        {
            get => _loginProfil;
            set => this.RaiseAndSetIfChanged(ref _loginProfil, value);
        }

        public bool EmailEnabled
        {
            get => LoginProfil == null;
        }

        public ReactiveCommand<Unit, Unit> Back { get; }

        IMessageBus MessagesBus;

        bool _canGoBack = false;
        public bool CanGoBack
        {
            get => _canGoBack;
            set => this.RaiseAndSetIfChanged(ref _canGoBack, value);
        }

        public LoginViewModel(IScreen screen, IMessageBus bus, AccountData? account, bool canGoBack = true)
        {
            HostScreen = screen;


            MessagesBus = bus;

            LoginProfil = account;
            if(account != null)
            {
                Email = account.Mail;
            }

            Back = ReactiveCommand.Create(BackToList);

            CanGoBack = canGoBack;

            /// TEST
           
            // On crée un Observable qui représente l'état de validité du formulaire
            var isFormValid = this.WhenAnyValue(
                x => x.Email,
                x => x.Password,
                (email, password) =>
                    !string.IsNullOrWhiteSpace(email) &&
                    !string.IsNullOrWhiteSpace(password)
            );

           
            // On observe l'état de validité du formulaire et on met à jour la propriété IsLoginEnabled
            isFormValid
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(valid => IsLoginEnabled = valid);

            // On observe l'état de la propriété IsBusy et on met à jour la propriété IsLoginEnabled
            this.WhenAnyValue(x => x.IsBusy)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy => IsLoginEnabled = !busy);

            // On crée une commande pour la validation du formulaire et l'authentification de l'utilisateur
            LoginCommand = ReactiveCommand.Create(Login);
        }

        void BackToList()
        {
            MessagesBus.SendMessage(
                new ConnectionStateMessage(ConnectionStateEnum.LoginList)
            );
        }
        void Login()
        {
            IsBusy = true;
            Console.WriteLine(Email);
            Console.WriteLine(Password);

            try
            {
                bool ok;
                ClientData acc;

                (ok, acc) = Service.AuthentifierClient(Email, Password);
                if (ok)
                {
                    StoreConnections.EnregistrerConnection(acc);
                    MessageBus.Current.SendMessage(new MessageBusType(MessageType.LoginSucess));
                }
                else
                {
                    // On efface les champs email et mot de passe
                    if(LoginProfil == null)
                    {
                        Email = "";
                    }
                   
                    Password = "";
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

