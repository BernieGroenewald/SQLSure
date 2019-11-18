using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSentry
{
    [Serializable]
    public class ObjectExplorerNodeDescriptorBase
    {
        public virtual string TypeDescription
        {
            get
            {
                return string.Format("{0}", (object)this.GetType());
            }
        }
    }
}
