using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutSchema<T> 
        : List<AutoLayoutSchemaItem<T>> 
        where T : INotifyPropertyChanged
    {
    }

    public class AutoLayoutSchemaItem<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutSchemaItem(string columnName, Type columnType) 
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }

        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
    }
}
