using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.Backend.Modules.System
{
    internal abstract class Category : IComparable
    {
        /* Set by Derived Class */
        internal abstract string Name { get; }
        internal abstract int Index { get; }
        internal virtual string CategoryId { get; set; }
        public int CompareTo(object obj)
        {
            return Index.CompareTo(((Category)obj).Index);
        }
    }
}
