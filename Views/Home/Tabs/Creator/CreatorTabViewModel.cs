

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using CookinGest.Windows;
using CookinGest.Windows.ConfirmDialog;
using CookinGest.Windows.ConvertCreatorPoints;
using CookinGest.Windows.PublishRecipe;
using CookinGest.Windows.ShowCreatorRecipeOrderHistory;
using CookinGest.Windows.ShowRecipesByProduct;
using ReactiveUI;

namespace CookinGest.Views.Home.Tabs
{
	public class CreatorTabViewModel: ReactiveObject, IRoutableViewModel
    {
        Client _client;
        bool _isCreator;

        int _solde;
        int _numberOrdersRecipes;

        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public ReactiveCommand<Unit, Unit> EnableCreatorProfil { get; }

        public ReactiveCommand<Unit, Unit> OpenSoldeOptions { get; }
        public ReactiveCommand<Unit, Unit> ShowOrdersRecipesCreatorReq { get; }
        public ReactiveCommand<Unit, Unit> PublishRecipe { get; }

        public Interaction<PublishRecipeViewModel, Unit> ShowDialog { get; }

        public Client Client
        {
            get => _client;

        }

        public bool IsCreator
        {
            get => _isCreator;
            set => this.RaiseAndSetIfChanged(ref _isCreator, value);
        }

        public int SoldePoint
        {
            get => _solde;
            set => this.RaiseAndSetIfChanged(ref _solde, value);
        }

        public int NumberOrdersRecipes
        {
            get => _numberOrdersRecipes;
            set => this.RaiseAndSetIfChanged(ref _numberOrdersRecipes, value);
        }

        //NombreCommandesRecetteCreateur
        ObservableCollection<RecipeData> _creatorRecipesList;

        public ObservableCollection<RecipeData> CreatorRecipesList {
            get => _creatorRecipesList;
            set => this.RaiseAndSetIfChanged(ref _creatorRecipesList, value);
        }

        public ReactiveCommand<RecipeData, Unit> DeleteRecipeReq { get; }

        public CreatorTabViewModel(IScreen screen, Client c)
        {
            HostScreen = screen;
            _client = c;

            IsCreator = Client.EstCreateur;
            SoldePoint = Client.SoldePoint;
            EnableCreatorProfil = ReactiveCommand.Create(ReqEnableCreatorProfil);

            OpenSoldeOptions = ReactiveCommand.CreateFromTask(SoldeOptionAsync);
            ShowOrdersRecipesCreatorReq = ReactiveCommand.CreateFromTask(ShowOrdersRecipesCreator);
            PublishRecipe = ReactiveCommand.CreateFromTask(PublishRecipeDialogAsync);

            DeleteRecipeReq = ReactiveCommand.CreateFromTask<RecipeData>(DeleteRecipe);

            Load();
        }

        void ReqEnableCreatorProfil()
        {
            if(Client.CreeProfilCreateur())
            {
                IsCreator = Client.EstCreateur;
                LoadCreatorStats();
            }
        }

        async Task PublishRecipeDialogAsync()
        {
            if(App.MainWindow != null)
            {
                var dialog = new PublishRecipeWindow();
                RecipeData res = await dialog.ShowDialog<RecipeData>(App.MainWindow);

                if (res == null) return; // la fenetre d'ajout de recette a été fermée

                List<int> produitsID = new List<int>();
                foreach(ProductData prd in res.IngredientsListe)
                {
                    produitsID.Add(prd.Id);
                }

                if(Client.CreeRecette(res.Nom, res.Description, produitsID)) // si l'ajout est reussit
                {
                    Console.WriteLine("Reactualisation...");
                    // On reactualise la liste des recettes
                    LoadCreatorRecipes();
                }
            }
        }

        void Load()
        {
            LoadCreatorStats();
            LoadCreatorRecipes();
        }

        void LoadCreatorStats()
        {
            (NumberOrdersRecipes, SoldePoint) = Client.StatsCreateur();
        }

        void LoadCreatorRecipes()
        {
            CreatorRecipesList = new ObservableCollection<RecipeData>(Client.ObtenirListeRecetteCreateur());
        }

        async Task SoldeOptionAsync()
        {
            if (App.MainWindow != null)
            {
                var dialog = new ConvertCreatorPointsWindow(1000); // a changer en SoldePoint
                DialogResult<int> conversionAmount = await dialog.ShowDialog<DialogResult<int>>(App.MainWindow);

                if (conversionAmount == null) return; // la fenetre d'ajout de recette a été fermée

                if(Client.ConvertirSoldeCreateur(conversionAmount.Value))
                {
                    SoldePoint = Client.SoldePoint;
                }
                
            }
        }

        async Task ShowOrdersRecipesCreator()
        {
            if (App.MainWindow != null)
            {
                var dialog = new ShowCreatorRecipeOrderHistoryWindow(Client);
                await dialog.ShowDialog<ProductData>(App.MainWindow);
            }
        }


        // Actions

        async Task DeleteRecipe(RecipeData recipe)
        {
            if (App.MainWindow != null)
            {
                var dialog = new ConfirmDialogWindow("Supprimer la recette ?", "");
                DialogResult<bool> res = await dialog.ShowDialog<DialogResult<bool>>(App.MainWindow);

                if (res == null || res.Value == false) return; // la fenetre d'ajout de recette a été fermée

                if (res.Value)
                {
                    // Suppresion valider par l'utilisateur
                    Service.SupprimerRecette(recipe.Id);

                    CreatorRecipesList.Remove(recipe);
                    LoadCreatorStats();
                }
            }
        }

    }
}

