using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class ViewPublisher
    {
        #region Properties
        /// <summary>
        /// The name of the publisher.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ViewPublisher()
        {

        }

        /// <summary>
        /// A constuctor that makes a Publisher object..
        /// </summary>
        /// <param name="name">The name of the publisher.</param>
        public ViewPublisher(string name)
        {
            Name = name;
        }

        #endregion
        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is ViewPublisher publisher &&
                   Name == publisher.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
