using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using ReactiveUI;
using System;

using Avalonia;
using System.Reactive.Linq;

namespace CookinGest.Windows.AddProduct
{
    public partial class AddProductWindow : ReactiveWindow<AddProductViewModel>
    {
        public AddProductWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
#endif

            ViewModel = new AddProductViewModel();

            this.WhenActivated(d => {
                ViewModel.OnWindowShown();
                d(ViewModel.Submit.Subscribe(Close));
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
