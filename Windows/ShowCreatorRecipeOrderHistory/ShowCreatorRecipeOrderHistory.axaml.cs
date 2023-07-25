using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;

namespace CookinGest.Windows.ShowCreatorRecipeOrderHistory
{
    public partial class ShowCreatorRecipeOrderHistoryWindow : ReactiveWindow<ShowCreatorRecipeOrderHistoryViewModel>
    {
        public ShowCreatorRecipeOrderHistoryWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif


            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
            });
        }

        public ShowCreatorRecipeOrderHistoryWindow(Client c) : this()
        {
            ViewModel = new ShowCreatorRecipeOrderHistoryViewModel(c);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
