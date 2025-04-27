using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.Backend.Modules.System
{
    internal abstract class Module : IComparable
    {
        /* Set by Derived Class */
        internal abstract string Name { get; }
        internal abstract string Category { get; }
        internal abstract string ToolTip { get; }
        internal abstract bool IsTogglable { get; }
        internal abstract bool Pinned { get; }

        /* Set by Handler */
        internal abstract bool State { get; set; }

        internal virtual string ModuleId { get; set; }


        /* Derived Class Methods */
        internal virtual void OnStateChanged() { }
        internal virtual void Update() { }
        internal virtual void OnGui() { }

        public int CompareTo(object _module)
        {
            Module module = (Module)_module;

            int position = Name.CompareTo(module.Name);
            return position;
        }
    }
}
