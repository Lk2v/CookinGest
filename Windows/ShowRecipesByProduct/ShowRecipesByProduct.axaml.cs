using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;
using CookinGest.src.DataTemplate;

namespace CookinGest.Windows.ShowRecipesByProduct
{
    public partial class ShowRecipesByProductWindow : ReactiveWindow<ShowRecipesByProductViewModel>
    {
        public ShowRecipesByProductWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif


            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
            });
        }

        public ShowRecipesByProductWindow(ProductData product) : this()
        {
            ViewModel = new ShowRecipesByProductViewModel(product);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
