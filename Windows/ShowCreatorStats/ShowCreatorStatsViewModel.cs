using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using CookinGest.src.DataTemplate;
using CookinGest.src.DB;
using ReactiveUI;

namespace CookinGest.Windows.ShowCreatorStats
{
    public class ShowCreatorStatsViewModel : ReactiveObject
    {

        
        CreatorStats _data;
        public CreatorStats Data {
            get => _data;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }

        int _creatorId = 0;
        public ShowCreatorStatsViewModel(int creId)
        {
            _creatorId = creId;
        }

        internal void OnWindowShown()
        {
            Data = Service.ObtenirStatsCreateur(_creatorId);
        }
    }
}

