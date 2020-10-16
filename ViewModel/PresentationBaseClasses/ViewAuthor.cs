using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class ViewAuthor
    {
        #region Properties
        /// <summary>
        /// The authors name
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ViewAuthor()
        {

        }
        /// <summary>
        /// A constuctor that makes an Author object.
        /// </summary>
        /// <param name="name">The name of the author.</param>
        public ViewAuthor(string name)
        {
            Name = name;
        }

        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is ViewAuthor author &&
                   Name == author.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        #endregion
    }
}
