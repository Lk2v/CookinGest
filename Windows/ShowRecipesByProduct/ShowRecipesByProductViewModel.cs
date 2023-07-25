using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.ShowRecipesByProduct
{
    public class ShowRecipesByProductViewModel : ReactiveObject
    {
        List<RecipeData> _list;

        public List<RecipeData> List {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        ProductData _product;
        public ProductData Product
        {
            get => _product;
            set => this.RaiseAndSetIfChanged(ref _product, value);
        }

        public ShowRecipesByProductViewModel(ProductData p)
        {
            Product = p;
        }

        List<RecipeData> Load()
        {
            return new List<RecipeData>(Service.ListeRecetteParIngredient(Product.Id));
        }

        internal void OnWindowShown()
        {
            List = Load();
        }
    }
}

