using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaDeObfuscator
{
    public class ObfuscatedMethod
    {
        public string ParentClass { get; set; }
        public string Signature { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
