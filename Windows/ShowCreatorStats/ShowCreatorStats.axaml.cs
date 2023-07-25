using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows.ShowCreatorStats
{
    public partial class ShowCreatorStatsWindow : ReactiveWindow<ShowCreatorStatsViewModel>
    {
        public ShowCreatorStatsWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
            });
        }

        public ShowCreatorStatsWindow(int idCre) : this()
        {
            ViewModel = new ShowCreatorStatsViewModel(idCre);
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
