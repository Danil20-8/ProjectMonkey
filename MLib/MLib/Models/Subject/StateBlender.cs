using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib.Models.Subject
{
    public class StateBlender
    {
        public State State { get { var result = state.Clone(); if(count > 0) result.Divide(count); return result; } }
        State state;
        int count;

        public StateBlender(ModelState model)
        {
            state = new State(model);
        }

        public void AddState(State state)
        {
            this.state.Merge(state);
            ++count;
        }
    }
}
