using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Gantt_Diagramm {
    public class TimeLine : ObservableObject {
        private TimeSpan _duration;
        public TimeSpan Duration {
            get {
                return _duration;
            }
            set {
                Set(() => Duration, ref _duration, value);
            }
        }


        private ObservableCollection<TimeLineEvent> _events = new ObservableCollection<TimeLineEvent>();
        public ObservableCollection<TimeLineEvent> Events {
            get {
                return _events;
            }
            set {
                Set(() => Events, ref _events, value);
            }
        }
    }
}
