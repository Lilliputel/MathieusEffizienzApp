using System;
using System.Collections.Generic;
using System.Text;

namespace Gantt_Diagramm {
    public class TimeLineEvent : ObservableObject {

        private TimeSpan _start;
        public TimeSpan Start {
            get {
                return _start;
            }
            set {
                Set(() => Start, ref _start, value);
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration {
            get {
                return _duration;
            }
            set {
                Set(() => Duration, ref _duration, value);
            }
        }

    }
}
