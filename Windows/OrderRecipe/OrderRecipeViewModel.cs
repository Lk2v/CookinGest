using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.OrderRecipe
{
    public class OrderRecipeViewModel : ReactiveObject
    {
        
        public ReactiveCommand<Unit, CommandDetails> SubmitOrder { get; }

        RecipeData _recipe;

        public RecipeData Recipe
        {
            get => _recipe;
        }

        public string SubTitle
        {
            get => $"« {Recipe.Nom} » sera livré à votre adresse";
        }

        string _price;
        public string Price
        {
            get => _price;
            set => this.RaiseAndSetIfChanged(ref _price, value);
        }

        public OrderRecipeViewModel(RecipeData r)
        {
            _recipe = r;
            SubmitOrder = ReactiveCommand.Create<CommandDetails>(OrderRecipe);
            

            SelectedPaymentMethod = 0;
        }

        int _selectedPaymentMethod = 0;

        public int SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set {
                this.RaiseAndSetIfChanged(ref _selectedPaymentMethod, value);


                if (value == 1)
                {
                    Price = (Recipe.Prix * 100) + " pts";
                }
                else
                {
                    Price = Recipe.Prix + " €";
                }
            }
        }

        CommandDetails OrderRecipe()
        {
            return new CommandDetails
            {
                Qte = 1,
                MethodPayement = (PaymentMethod) SelectedPaymentMethod,
            };
    }

        internal void OnWindowShown()
        {
            
        }
    }
}

