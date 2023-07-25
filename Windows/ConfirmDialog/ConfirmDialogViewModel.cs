using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.ConfirmDialog
{
    public class ConfirmDialogViewModel : ReactiveObject
    {
        
        public ReactiveCommand<Unit, DialogResult<bool>> ConfirmReq { get; }

        public string Title { get; set; }
        public string Message { get; set; }

        public ConfirmDialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;

            ConfirmReq = ReactiveCommand.Create<DialogResult<bool>>(Confirm);
        }

        DialogResult<bool> Confirm()
        {
            return new DialogResult<bool>(true);
        }

        DialogResult<bool> Cancel()
        {
            return new DialogResult<bool>(false);
        }

        internal void OnWindowShown()
        {
            
        }
    }
}

