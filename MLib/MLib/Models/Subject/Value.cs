using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib.Models.Subject
{
    public struct Value
    {
        public double Magnitude => (value - model.Min) / model.Max;

        ModelValue model;

        double value;

        public Value(ModelValue model, double value = 0)
        {
            this.model = model;
            this.value = value;
        }

        public void ToBounds()
        {
            value = model.Bounds.ToBounds(value);
        }

        public void DropHalf()
        {
            value *= .5;
        }

        public void ZeroValue()
        {
            value = 0;
        }

        public Value Clone()
        {
            return new Value
            {
                model = model,
                value = value
            };
        }

        public static Value operator +(Value lv, Value rv)
        {
            return new Value(lv.model, lv.value + rv.value);
        }
        public static Value operator -(Value lv, Value rv)
        {
            return new Value(lv.model, lv.value - rv.value);
        }
        public static Value operator /(Value lv, double rv)
        {
            return new Value(lv.model, lv.value / rv);
        }
        public static Value operator *(Value lv, double rv)
        {
            return new Value(lv.model, lv.value * rv);
        }
        public static Value Max(ModelValue model)
        {
            return new Value(model, model.Max);
        }
    }
}
