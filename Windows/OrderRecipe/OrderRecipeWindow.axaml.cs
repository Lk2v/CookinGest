using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;
using CookinGest.src.DataTemplate;

namespace CookinGest.Windows.OrderRecipe
{
    public partial class OrderRecipeWindow : ReactiveWindow<OrderRecipeViewModel>
    {
        public OrderRecipeWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.SubmitOrder.Subscribe(Close));
            });
        }

        public OrderRecipeWindow(RecipeData recipe) : this()
        {
            ViewModel = new OrderRecipeViewModel(recipe);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
