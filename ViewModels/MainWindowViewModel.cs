using System;
using ReactiveUI;
using System.Reactive;

using CookinGest.src;
using CookinGest.src.DB;
using System.Threading.Tasks;

using CookinGest.Windows.PublishRecipe;
using CookinGest.Windows;

namespace CookinGest.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        // The command that navigates a user to first view model.
        //public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public Interaction<PublishRecipeViewModel, Unit> ShowDialog { get; }

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<PublishRecipeViewModel, Unit>();

            MessageBus.Current.Listen<MessageBusType>()
            .Subscribe(MessageSubscribe);
            Router.Navigate.Execute(new ConnectionViewModel(this));

            //Router.Navigate.Execute(new HomeViewModel(this, c: null));

            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            //

        }

        public void MessageSubscribe(MessageBusType message) {
            switch(message.ViewType) {
                case MessageType.LoginSucess:
                    Client? c = Service.ObtenirClient();
                    if (c != null) {
                        Router.Navigate.Execute(new HomeViewModel(this, c: c!));
                    }
                    break;

                case MessageType.LogOut:
                    Router.Navigate.Execute(new ConnectionViewModel(this));
                    break;
            }
        }
    }

}

