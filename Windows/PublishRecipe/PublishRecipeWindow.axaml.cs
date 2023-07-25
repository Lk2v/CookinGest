using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using CookinGest.Windows.PublishRecipe;
using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows
{
    public partial class PublishRecipeWindow : ReactiveWindow<PublishRecipeViewModel>
    {
        public PublishRecipeWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif

            ViewModel = new PublishRecipeViewModel();

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.SubmitRecipe.Subscribe(Close));
            });
        }

        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
