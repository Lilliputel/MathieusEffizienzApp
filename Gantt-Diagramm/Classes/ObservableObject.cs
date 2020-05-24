using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace Gantt_Diagramm {
    public class ObservableObject : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Set<T>( Expression<Func<T>> property, ref T field, T value ) {
            if( !object.ReferenceEquals(field, value) ) {
                field = value;
                this.OnPropertyChanged(property);
            }
        }

        protected virtual void OnPropertyChanged<T>( Expression<Func<T>> changedProperty ) {
            if( PropertyChanged != null ) {
                string name = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
