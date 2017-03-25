using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Models.Subject;
using MLib.Models;
namespace MLib
{
    public class Subject
    {
        public string Name { get; private set; }
        public State State { get; private set; }
        SubjectCore core;
        public Room Room { get; set; }

        State mutation;

        public Subject(string name, ModelState model, IEnumerable<SubjectAction> actions)
        {
            Name = name;
            State = new State(model, model.Values.Select(v => v.Max));
            mutation = new State(model);
            core = new SubjectCore(actions, model);
        }

        public ActionTarget Wish()
        {
            if (Room != null)
                return core.MakeChoiseFor(this, Room.Subjects);
            else
                return core.MakeChoiseFor(this, this);
        }

        public void Apply(State stateEffect)
        {
            mutation.Merge(stateEffect);
        }

        public void Apply()
        {
            State.Merge(mutation);
            State.ToBounds();
            core.Feedback(mutation);

            mutation.ZeroState();

            // Apply feedback and last action
            core.Apply();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
