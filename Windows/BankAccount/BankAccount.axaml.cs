using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows.BankAccount
{
    public partial class BankAccountWindow : ReactiveWindow<BankAccountViewModel>
    {
        public BankAccountWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif

            

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.RefillReq.Subscribe(Close));
            });
        }

        public BankAccountWindow(int solde) : this()
        {
            ViewModel = new BankAccountViewModel(solde);
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
