using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib
{
    public class ActionTarget
    {
        public SubjectActionResult Action { get; private set; }

        public Subject Object { get; private set; }
        public Subject Subject { get; private set; }

        Action<ActionTarget> applyCallback;

        public ActionTarget(SubjectActionResult action, Subject obj, Subject subj, Action<ActionTarget> applyCallback)
        {
            this.Action = action;
            Object = obj;
            Subject = subj;

            this.applyCallback = applyCallback;
        }

        public void Apply()
        {
            Action.Action.Apply(Object, Subject);
            applyCallback(this);
        }

        public override string ToString()
        {
            return $"{Object} {Action.Action} {Subject}";
        }
    }
}
