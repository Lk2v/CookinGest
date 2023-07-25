using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows.ConfirmDialog
{
    public partial class ConfirmDialogWindow : ReactiveWindow<ConfirmDialogViewModel>
    {
        
        public ConfirmDialogWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.ConfirmReq.Subscribe(Close));
            });
        }

        public ConfirmDialogWindow(string t, string m) : this()
        {
            ViewModel = new ConfirmDialogViewModel(t, m);
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
