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

namespace CookinGest.Windows.BankAccount
{
    public class BankAccountViewModel : ReactiveObject
    {
        
        public ReactiveCommand<Unit, DialogResult<bool>> RefillReq { get; }

        int _soldeBank;

        public int SoldeBank {
            get => _soldeBank;
            set => this.RaiseAndSetIfChanged(ref _soldeBank, value);
        }

        public bool CanRefill
        {
            get => SoldeBank < 1000;
        }

        public BankAccountViewModel(int solde)
        {
            SoldeBank = solde;
            RefillReq = ReactiveCommand.Create<DialogResult<bool>>(RefillBankRequest);
        }

        DialogResult<bool> RefillBankRequest()
        {
            return new DialogResult<bool>(false);
        }

        internal void OnWindowShown()
        {
            
        }
    }
}

