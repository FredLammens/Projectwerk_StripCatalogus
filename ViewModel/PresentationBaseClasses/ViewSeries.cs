using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class ViewSeries : IComparable<ViewSeries>
    {
        #region Properties
        [JsonProperty("Naam")]
        public string Name { get; set; }
        #endregion

        #region Constructors
        public ViewSeries()
        {

        }

        [JsonConstructor]
        public ViewSeries(string name)
        {
            Name = name;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is ViewSeries series &&
                   Name == series.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        public int CompareTo([AllowNull] ViewSeries other)
        {
            return this.Name.CompareTo(other.Name);
        }
        #endregion

        #region toString
        public override string ToString()
        {
            return Name;
        }
        #endregion


    }
}
