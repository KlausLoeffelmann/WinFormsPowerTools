using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrueddelLib
{
    internal class Develop2
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public static Develop2 GetInstance()
        {
            return new Develop2();
        }
    }

    internal class DevelopAttribute : Attribute
    {
        public DevelopAttribute()
        {
        }

        public Develop2? DevProperty { get; set; }
    }
}
