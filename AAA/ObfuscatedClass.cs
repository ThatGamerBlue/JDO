using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaDeObfuscator
{
    public class ObfuscatedClass
    {
        public string Name { get; set;  }
        public string Superclass { get; set;  }
        public IList<string> Interfaces { get; set; }
        public IList<ObfuscatedMethod> Methods { get; set; }
        public IList<ObfuscatedField> Fields { get; set; }
    }
}
