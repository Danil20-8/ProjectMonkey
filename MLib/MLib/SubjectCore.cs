using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRLib.Algoriphms;
using MLib.Models;
using MLib.Models.Subject;

namespace MLib
{
    public class SubjectCore
    {
        SubjectActionResult[] actions;

        SubjectActionResult lastAction;
        SubjectActionResult unstableLastAction;

        State objectFeedback;
        State subjectFeedback;


        public SubjectCore(IEnumerable<SubjectAction> actions, ModelState model)
        {
            this.actions = actions.Select(a => new SubjectActionResult(a, model)).ToArray();
            objectFeedback = new State(model);
            subjectFeedback = new State(model);
            lastAction = new SubjectActionResult(SubjectAction.Fake, model);
        }

        public ActionTarget MakeChoiseFor(Subject obj, IEnumerable<Subject> subjects)
        {
            return subjects.SelectMany(s => actions.Select(a => Tuple.Create(new ActionTarget(a, obj, s, SetLastAction), a.TryOn(obj.State, s.State))))
                .WithMax(t => t.Item2.ObjectResult.Magnitude).Item1;
        }

        public ActionTarget MakeChoiseFor(Subject obj, Subject subject)
        {
            return actions.Select(a => Tuple.Create(new ActionTarget(a, obj, subject, SetLastAction), a.TryOn(obj.State, subject.State)))
                .WithMax(t => t.Item2.ObjectResult.Magnitude).Item1;
        }

        void SetLastAction(ActionTarget action)
        {
            unstableLastAction = action.Action;
        }

        public void Feedback(State feedbackState)
        {
            objectFeedback.Merge(feedbackState);
        }
        public void Feedback(State objectFeedback, State subjectFeedback)
        {
            this.objectFeedback.Merge(objectFeedback);
            this.subjectFeedback.Merge(subjectFeedback);
        }
        public void Apply()
        {
            if (unstableLastAction != null)
            {
                lastAction = unstableLastAction;
                unstableLastAction = null;
            }

            lastAction.AddFeedback(objectFeedback, subjectFeedback);
            objectFeedback.ZeroState();
            subjectFeedback.ZeroState();
        }
    }
}
