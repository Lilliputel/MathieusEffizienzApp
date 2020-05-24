using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Gantt_Diagramm {
    public class MainViewModel : ViewModelBase {

        private ObservableCollection<TimeLine> _timeLines = new ObservableCollection<TimeLine>();
        public ObservableCollection<TimeLine> TimeLines {
            get {
                return _timeLines;
            }
            set {
                Set(() => TimeLines, ref _timeLines, value);
            }
        }

    }
}
