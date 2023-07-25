using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows.ConvertCreatorPoints
{
    public partial class ConvertCreatorPointsWindow : ReactiveWindow<ConvertCreatorPointsViewModel>
    {
        public ConvertCreatorPointsWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.SubmitReq.Subscribe(Close));
            });
        }

        public ConvertCreatorPointsWindow(int cSolde) : this()
        {
            ViewModel = new ConvertCreatorPointsViewModel(cSolde);
        }

        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
