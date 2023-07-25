using System;
using System.Collections.Generic;
using System.Reactive;
using CookinGest.src.DB;
using ReactiveUI;
using CookinGest.src.DataTemplate;
using System.Linq;
using System.Collections.ObjectModel;
using CookinGest.Windows;
using System.Threading.Tasks;

using CookinGest.Windows.ShowRecipesByProduct;
using CookinGest.Windows.AddProduct;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
	public class ProductsBoardViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        ObservableCollection<ProductData> _data;
        public ObservableCollection<ProductData> Data {
            get => _data;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }

        public ReactiveCommand<ProductData, Unit> ShowRecipesProduct { get; }
        public ReactiveCommand<int, Unit> RestockProduct { get; }
        public ReactiveCommand<Unit, Unit> AddProduct { get; }

        public ProductsBoardViewModel(IScreen screen)
        {
            HostScreen = screen;

            AddProduct = ReactiveCommand.CreateFromTask(AddProductDialog);

            ShowRecipesProduct = ReactiveCommand.CreateFromTask<ProductData>(RecipesProductDialog);
            RestockProduct = ReactiveCommand.Create<int>(RestockProductReq);

            

            Data = Load();
        }

        async Task RecipesProductDialog(ProductData product)
        {
            if (App.MainWindow != null)
            {
                var dialog = new ShowRecipesByProductWindow(product);
                await dialog.ShowDialog<ProductData>(App.MainWindow);
            }
        }

        void RestockProductReq(int id)
        {
            Console.WriteLine("Restock " + id);
            ProductData newPro =  Service.ApprovisionnerIngredient(id);

            for(int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Id == id)
                {
                    Data[i] = newPro;
                    break;
                }
            }
        }

        async Task AddProductDialog()
        {
            if (App.MainWindow != null)
            {
                var dialog = new AddProductWindow();
                ProductData res = await dialog.ShowDialog<ProductData>(App.MainWindow);

                if (res == null) return; // la fenetre d'ajout de recette a été fermée

                Data.Add(Service.AjouterIngredient(res));
            }
        }

        ObservableCollection<ProductData> Load()
        {
            return new ObservableCollection<ProductData>(Service.ListeIngredientsAvecNbRecettes());
        }

        
    }
}

