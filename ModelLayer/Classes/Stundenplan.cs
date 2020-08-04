using System;
using System.Collections.Generic;

namespace ModelLayer.Classes {
	public static class Stundenplan {

		#region properties

		public static Dictionary<DayOfWeek, Dictionary<TimeSpan, Guid>> Plan { get; set; }

		#endregion

		#region initializer

		static Stundenplan() {
			Plan = new Dictionary<DayOfWeek, Dictionary<TimeSpan, Guid>>();
			CreateEntries();
		}

		#endregion

		#region methods

		/// <summary>
		/// Methode, um eine Zeitspanne als Unikat einzufügen
		/// </summary>
		/// <param name="zeitfenster">Zeitfenster definiert StartZeit und endZeit (Automatisch auf 15min. gerundet)</param>
		/// <param name="id">Die ID, die als wert in das Lexikon eingetragen werden muss</param>
		/// <param name="doOverride">Ob bestehende einträge überschrieben werden sollen oder nicht, falls nein kann false zurückgegeben werden</param>
		public static bool TryPlan( ZeitSpanne zeitfenster, Guid id, bool doOverride = false ) {

			List<(DayOfWeek wochenTag, TimeSpan zeitStempel)> cacheListe = new List<(DayOfWeek wochenTag, TimeSpan zeitStempel)>();

			// Es werden alle Tage durchlaufen, die im *zeitfenster* zwischen dem Start und Enddatum liegen.
			for( DayOfWeek wochenTag = zeitfenster.Start.DayOfWeek;
				wochenTag <= zeitfenster.Ende.DayOfWeek;
				wochenTag++ ) {

				// Setzt eine Referenz zu dem Lexikon passend zum Tag
				var zeitDictionary = Plan[wochenTag];

				// Es wird der Tag in 15min. abschnitten durchlaufen. 
				// Beginnt bei dem StartZeitpunkt, falls es der StartTag ist oder bei 00h.
				// Endet bei 24h, falls der EndTag nicht erreicht ist oder bei der EndZeit
				for( TimeSpan
					zeitStempel = ( ( wochenTag > zeitfenster.Start.DayOfWeek ) ? new TimeSpan(00, 00, 00) : zeitfenster.Start.TimeOfDay );
					zeitStempel < ( ( wochenTag < zeitfenster.Ende.DayOfWeek ) ? new TimeSpan(24, 00, 00) : zeitfenster.Ende.TimeOfDay );
					zeitStempel.Add(TimeSpan.FromMinutes(15)) ) {

					// wenn Das Zeitlexikon bereits den Zeitstempel enthält
					// und wenn nicht überschrieben werden soll, dann wird falsch zurückgegeben
					if( zeitDictionary.ContainsKey(zeitStempel)
						&& doOverride == false ) {
						return false;
					}
					// Wenn überschrieben werden darf, 
					// oder wenn das lexikon den ZeitStempel noch nicht enhält
					// wird der ZeitStempel in der Cache-Liste gespeichert
					cacheListe.Add((wochenTag, zeitStempel));
				}
			}

			// wenn die for-schlaufe verlassen wird, werden alle Elemente aus der cach-Liste zu dem Lexikon hinzugefügt.
			foreach( var item in cacheListe ) {

				// verständlichere variabeln (referenz-Typen)
				var tag = Plan[item.wochenTag];
				var schlüssel = item.zeitStempel;

				// falls der Schlüssel bereits vorhanden ist, wird der Wert überschrieben
				if( tag.ContainsKey(schlüssel) ) {
					tag[schlüssel] = id;
				}
				// falls noch nicht vorhanden, wird der Schlüssel neu hinzugefügt.
				else {
					tag.Add(schlüssel, id);
				}

			}

			// If the for-loop was successful
			return true;
		}

		/// <summary>
		/// Eine Methode, die eine Liste von WochenTagen und Zeitpunkten (TimeSpan) zurückgiebt
		/// </summary>
		/// <param name="id">ID, zu welcher die Passenden TimeSpans gefunden werden sollen</param>
		public static List<(DayOfWeek wochentag, List<TimeSpan> zeitSpannen)> GetZeitSpannen( Guid id ) {

			// erstellt eine Transferliste, welche zurückgegeben wird.
			var ZeitSpannen = new List<(DayOfWeek, List<TimeSpan>)>();

			//durchläuft alle Paare in dem Lexikon Plan
			foreach( var listenPaar in Plan ) {
				// erstellt eine Transferliste, für die zeitSpannen
				var TäglicheZeitpunkte = new List<TimeSpan>();

				//durchläuft alle Paare in dem Lexikon Zeitspannen
				foreach( var itemPaar in listenPaar.Value ) {

					// Wenn der Wert im Lexikon mit dem gesuchten wert übereinstimmt, wird die Timespan zu der Transferliste hinzugefügt
					if( id == itemPaar.Value ) {
						TäglicheZeitpunkte.Add(itemPaar.Key);
					}
				}

				// Fügt das Paar <Wochentag, Zeitspannenliste> zu der Transferliste hinzu, wenn diese nicht leer ist
				if( TäglicheZeitpunkte.Count >= 0 ) {
					ZeitSpannen.Add((listenPaar.Key, TäglicheZeitpunkte));
				}
			}

			return ZeitSpannen;
		}

		#endregion

		#region helperMethods

		private static void CreateEntries() {
			CreateEntry(DayOfWeek.Monday);
			CreateEntry(DayOfWeek.Tuesday);
			CreateEntry(DayOfWeek.Wednesday);
			CreateEntry(DayOfWeek.Thursday);
			CreateEntry(DayOfWeek.Friday);
			CreateEntry(DayOfWeek.Saturday);
			CreateEntry(DayOfWeek.Sunday);
		}
		private static void CreateEntry( DayOfWeek wochenTag ) {
			Plan.Add(wochenTag, new Dictionary<TimeSpan, Guid>());
		}

		#endregion

	}
}
