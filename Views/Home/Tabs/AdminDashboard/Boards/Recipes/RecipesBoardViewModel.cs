using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media.TextFormatting.Unicode;
using CookinGest.src.DB;
using ReactiveUI;
using CookinGest.src.DataTemplate;
using CookinGest.Windows.ConfirmDialog;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CookinGest.Views.Home.Tabs.AdminDashboard.Boards
{
	public class RecipesBoardViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public ObservableCollection<RecipeData> Data { get; set; }

        public ReactiveCommand<RecipeData, Unit> DeleteRecipeReq { get; }

        public RecipesBoardViewModel(IScreen screen)
        {
            HostScreen = screen;

            DeleteRecipeReq = ReactiveCommand.CreateFromTask<RecipeData>(DeleteRecipe);

            Data = Load();
        }

        ObservableCollection<RecipeData> Load()
        {
            return new ObservableCollection<RecipeData>(Service.ListeRecettes());
        }

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

                    Data.Remove(recipe);
                }
            }
        }
    }
}

