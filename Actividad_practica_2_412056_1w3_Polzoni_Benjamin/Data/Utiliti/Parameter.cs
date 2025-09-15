using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Utiliti
{
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Parameter()
        {
            Name = string.Empty;
            Value = null;
        }

        public Parameter(string Name, object value)
        {
            this.Name = Name;
            this.Value = value;
        }
    }
}
