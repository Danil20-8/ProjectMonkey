using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRLib.Algoriphms;

namespace MLib.Models.Subject
{
    public class State
    {
        ModelState model;
        public IEnumerable<Value> Values => values;
        Value[] values;

        public double Magnitude => values.Product(1.0, (v, m) => m * v.Magnitude);

        public State(ModelState model)
        {
            this.model = model;
            values = model.Values.Select(m => new Value(m)).ToArray();
        }
        public State(ModelState model, IEnumerable<double> values)
        {
            if (values.Count() != model.Values.Length)
                throw new Exception("Value sequence have to contain same element count as element count of model values");

            this.model = model;
            this.values = model.Values.Zip(values, (m, v) => new Value(m, v)).ToArray();
        }

        public void Merge(State effectState)
        {
            for (int i = 0; i < values.Length; ++i)
                values[i] += effectState.values[i];
        }

        public void DropHalf()
        {
            for (int i = 0; i < values.Length; ++i)
                values[i].DropHalf();
        }
        public void Divide(double divider)
        {
            for (int i = 0; i < values.Length; ++i)
                values[i] /= divider;
        }
        public void ZeroState()
        {
            for (int i = 0; i < values.Length; ++i)
                values[i].ZeroValue();
        }

        public void ToBounds()
        {
            for (int i = 0; i < values.Length; ++i)
                values[i].ToBounds();
        }

        public State Clone()
        {
            return new State
            {
                model = model,
                values = values.Select(v => v.Clone()).ToArray()
            };
        }

        State()
        {
        }
        public static State operator +(State lv, State rv)
        {
            return new State
            {
                model = lv.model,
                values = lv.values.Zip(rv.values, (l, r) => l + r).ToArray()
            };
        }
    }
}
