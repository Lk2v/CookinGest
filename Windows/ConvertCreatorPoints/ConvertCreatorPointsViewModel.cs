using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.ConvertCreatorPoints
{
    public class ConvertCreatorPointsViewModel : ReactiveObject
    {
        
        public ReactiveCommand<Unit, DialogResult<int>> SubmitReq { get; }

        public ConvertCreatorPointsViewModel(int cSolde)
        {
            MaxCreatorSolde = cSolde;
            CreatorSolde = cSolde;

            SubmitReq = ReactiveCommand.Create<DialogResult<int>>(ConvertSolde);
        }

        int _maxCreatorSolde = 0;
        public int MaxCreatorSolde
        {
            get => _maxCreatorSolde;
            set => this.RaiseAndSetIfChanged(ref _maxCreatorSolde, value);
        }

        int _creatorSolde = 0;
        int _bankSolde = 0;

        public int CreatorSolde
        {
            get => _creatorSolde;
            set {
               
                this.RaiseAndSetIfChanged(ref _creatorSolde, value);

                BankSolde = value / 100;
            }
        }

        public int BankSolde
        {
            get => _bankSolde;
            set => this.RaiseAndSetIfChanged(ref _bankSolde, value);
        }

        DialogResult<int> ConvertSolde()
        {
            return new DialogResult<int>(CreatorSolde);
        }

        internal void OnWindowShown()
        {
            
        }
    }
}

