using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gantt_Diagramm {

	public partial class MainWindow : Window {

        private MainViewModel ViewModel = new MainViewModel();
		public MainWindow() {
			InitializeComponent();
			DataContext = ViewModel;

            CreateObjects();

		}

		private void CreateObjects() {

            TimeLine first = new TimeLine();
            first.Duration = new TimeSpan(1, 0, 0);
            first.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 15, 0), Duration = new TimeSpan(0, 15, 0) });
            first.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 40, 0), Duration = new TimeSpan(0, 10, 0) });
            ViewModel.TimeLines.Add(first);

            TimeLine second = new TimeLine();
            second.Duration = new TimeSpan(1, 0, 0);
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 0, 0), Duration = new TimeSpan(0, 25, 0) });
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 30, 0), Duration = new TimeSpan(0, 15, 0) });
            second.Events.Add(new TimeLineEvent() { Start = new TimeSpan(0, 50, 0), Duration = new TimeSpan(0, 10, 0) });
            ViewModel.TimeLines.Add(second);
        }
	}
}
