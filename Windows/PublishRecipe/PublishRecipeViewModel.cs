using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.PublishRecipe
{
    public class PublishRecipeViewModel : ReactiveObject
    {
        List<ProductData> _list;
        public List<ProductData> List {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        string _title;
        public string RecipeTitle
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        string _description;
        public string ShortDescription
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public ReactiveCommand<Unit, RecipeData> SubmitRecipe { get;  }

        public PublishRecipeViewModel()
        {
            SubmitRecipe = ReactiveCommand.Create<RecipeData>(SendRecipe);
        }

        RecipeData SendRecipe()
        {
            List<ProductData> products = new List<ProductData>();

            foreach(ProductData p in List)
            {
                if(p.CheckBoxSelect)
                {
                    products.Add(p);
                }
            }

            return new RecipeData
            {
                Nom=RecipeTitle,
                Description=ShortDescription,
                IngredientsListe=products,
            };
        }

        internal void OnWindowShown()
        {
            LoadDataAsync();
        }

        internal void LoadDataAsync()
        {
            // Ici, nous exécutons notre requête BDD pour charger les données
            // et les stockons dans notre propriété
            List = Service.ListeIngredients();
        }
    }
}

