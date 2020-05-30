using Effizienz.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Effizienz.Views {

	public partial class ViewPomodoro : UserControl {

		private DispatcherTimer zähler;
		private TimeSpan zeit;
		private TimeSpan zeitInput_Arbeit;
		private TimeSpan zeitInput_Pause;
		private int intervall = 1;
		private bool isPause = false;
		private bool isStarted = false;


		public ViewPomodoro() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Pomodoro);
		}

		~ViewPomodoro() { }


		private void Button_ZeitStart_Click( object sender, RoutedEventArgs e ) {
			this.Button_ZeitStopReset.Content = "Stoppen!";

			if( TimeSpan.TryParse(TextBox_ZeitArbeit.Text, App.zeitformat, out this.zeitInput_Arbeit) &&
			TimeSpan.TryParse(TextBox_ZeitPause.Text, App.zeitformat, out this.zeitInput_Pause) ) {
				if( zähler == null ) {
					Zähler_Inizialisieren();
				}
				else if( zeitInput_Arbeit <= TimeSpan.Zero || zeitInput_Pause <= TimeSpan.Zero ) {
					MessageBoxDisplayer.ZeitFormatInkorrekt();
				}
				Zähler_Starten();
			}
			else {
				MessageBoxDisplayer.ZeitFormatInkorrekt();
			}
		}
		private void Button_ZeitStopReset_Click( object sender, RoutedEventArgs e ) {
			if( !isStarted ) {
				Zähler_Zurücksetzen();
				this.Button_ZeitStopReset.Content = "Stoppen!";
			}
			else {
				Zähler_Stoppen();
				this.Button_ZeitStopReset.Content = "Zurücksetzen!";
			}
		}
		private void Button_ArbeitPause_Click( object sender, RoutedEventArgs e ) {
			if( !isPause )
				Zeit_Speichern();
			isPause = !isPause;
			Zähler_Zurücksetzen();
		}
		private void Button_Speichern_Click( object sender, RoutedEventArgs e ) {
			if( !isPause ) {
				Zeit_Speichern();
				Zähler_Zurücksetzen();
			}
			else {
				MessageBox.Show("Es kann nur Arbeitszeit gespeichert werden!", "Zeiterfassung", MessageBoxButton.OK);
			}
		}


		private void Zähler_Inizialisieren() {
			zähler = new DispatcherTimer();
			zähler.Tick += Zähler_Tick;
			zähler.Interval = TimeSpan.FromSeconds(intervall);
			Zeit_ZyklusSwitch();
		}
		private void Zähler_Tick( object sender, EventArgs e ) {
			zeit -= zähler.Interval;
			Zeit_Aktualisieren();
			if( zeit == TimeSpan.Zero ) {
				Alarm_Auslösen();
			}
		}

		private void Zähler_Starten() {
			Zeit_Aktualisieren();
			isStarted = true;
			zähler.Start();
		}
		private void Zähler_Stoppen() {
			if( zähler != null ) {
				zähler.Stop();
				isStarted = false;
			}
		}
		private void Zähler_Zurücksetzen() {
			Zeit_ZyklusSwitch();
			Zeit_Aktualisieren();
		}

		private void Zeit_ZyklusSwitch() {
			if( isPause ) {
				zeit = zeitInput_Pause;
				this.Button_ArbeitPause.Content = "Arbeiten!";
			}
			else {
				zeit = zeitInput_Arbeit;
				this.Button_ArbeitPause.Content = "Pause machen!";
			}
		}
		private void Zeit_Aktualisieren() {
			TextBlock_ZeitAktuell.Text = zeit.ToString(@"hh\:mm\:ss", App.zeitformat);
		}
		private void Zeit_Speichern() {
			TimeSpan gesamteZeit = TimeSpan.ParseExact((string)TextBlock_ZeitGesamt.Text, @"hh\:mm\:ss", App.zeitformat);
			TimeSpan arbeitsZeit = zeitInput_Arbeit.Subtract(zeit);
			TextBlock_ZeitGesamt.Text = ( gesamteZeit.Add(arbeitsZeit) ).ToString(@"hh\:mm\:ss", App.zeitformat);
		}

		private void Alarm_Auslösen() {
			Zähler_Stoppen();
			Zeit_Speichern();
			isPause = !isPause;
			Zeit_ZyklusSwitch();
			Zähler_Starten();
		}

	}
}
