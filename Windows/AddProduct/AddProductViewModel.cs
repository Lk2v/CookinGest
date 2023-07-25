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

namespace CookinGest.Windows.AddProduct
{
    public class AddProductViewModel : ReactiveObject
    {

        string _title;
        public string ProductTitle
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

        int _currentStock = 0;
        public int CurrentStock
        {
            get => _currentStock;
            set => this.RaiseAndSetIfChanged(ref _currentStock, value);
        }

        int _maxStock = 200;
        public int MaxStock
        {
            get => _maxStock;
            set => this.RaiseAndSetIfChanged(ref _maxStock, value);
        }

        int _price = 1;
        public int ProductPrice
        {
            get => _price;
            set => this.RaiseAndSetIfChanged(ref _price, value);
        }

        List<IngredientCategorie> _listCat;
        public List<IngredientCategorie> ListCat {
            get => _listCat;
            set => this.RaiseAndSetIfChanged(ref _listCat, value);
        }

        List<SupplierData> _listFourn;
        public List<SupplierData> ListFourn
        {
            get => _listFourn;
            set => this.RaiseAndSetIfChanged(ref _listFourn, value);
        }

        IngredientCategorie _selectedCat;
        public IngredientCategorie SelectedCat
        {
            get => _selectedCat;
            set => this.RaiseAndSetIfChanged(ref _selectedCat, value);
        }

        SupplierData _selectedFourn;
        public SupplierData SelectedFourn
        {
            get => _selectedFourn;
            set => this.RaiseAndSetIfChanged(ref _selectedFourn, value);
        }
        public ReactiveCommand<Unit,ProductData> Submit { get; }

        public AddProductViewModel()
        {
            Submit = ReactiveCommand.Create<ProductData>(SubmitReq);
        }

        ProductData SubmitReq()
        {
            return new ProductData
            {
                Titre = ProductTitle,
                Description = ShortDescription,

                CategorieId = SelectedCat.Id,
                StockMin = 0, // par defaut
                Stock = CurrentStock,
                StockMax = MaxStock,

                FournisseurId = SelectedFourn.Id,
                Prix = ProductPrice,
            };
        }

        internal void OnWindowShown()
        {
            ListCat = Service.ListeCategoriesIngredient();
            SelectedCat = ListCat[0];

            ListFourn = Service.ListeFournisseurs();
            SelectedFourn = ListFourn[0];
        }
    }
}

