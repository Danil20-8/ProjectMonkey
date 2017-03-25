using System.Collections.Generic;
using System.Linq;

namespace MLib.Models
{
    public class ModelState
    {
        public ModelValue[] Values { get; private set; }

        public ModelState(IEnumerable<ModelValue> values)
        {
            Values = values.ToArray();
        }
    }
}