using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Gantt_Diagramm {
    public class MainViewModel : ViewModelBase {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel() {

            TimeLine first = new TimeLine();
            first.Duration = new TimeSpan(1, 0, 0);
            first.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 15, 0), Duration = new TimeSpan(0, 15, 0) });
            first.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 40, 0), Duration = new TimeSpan(0, 10, 0) });
            this.TimeLines.Add(first);

            TimeLine second = new TimeLine();
            second.Duration = new TimeSpan(1, 0, 0);
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 0, 0), Duration = new TimeSpan(0, 25, 0) });
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 30, 0), Duration = new TimeSpan(0, 15, 0) });
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 50, 0), Duration = new TimeSpan(0, 10, 0) });
            this.TimeLines.Add(second);
        }


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
