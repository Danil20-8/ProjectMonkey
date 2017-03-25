using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Models.Subject;
using MLib.Models;
namespace MLib
{
    public class SubjectActionResult
    {
        public SubjectAction Action { get; private set; }

        StateBlender objectResult;
        StateBlender subjectResult;

        public SubjectActionResult(SubjectAction action, ModelState model)
        {
            Action = action;

            objectResult = new StateBlender(model);
            subjectResult = new StateBlender(model);
        }

        public void AddFeedback(State objectFeedback, State subjectFeedback)
        {
            objectResult.AddState(objectFeedback);
            subjectResult.AddState(subjectFeedback);
        }

        public TryOnResult TryOn(State objectState, State subjectState)
        {
            if (objectState != subjectState)
            {
                var objResult = objectState + objectResult.State;
                var subjResult = subjectState + subjectResult.State;
                objResult.ToBounds();
                subjResult.ToBounds();

                return new TryOnResult
                {
                    ObjectResult = objResult,
                    SubjectResult = subjResult
                };
            }
            else
            {
                var objResult = objectResult.State + subjectResult.State;
                objResult.Merge(objectState);
                objResult.ToBounds();
                return new TryOnResult
                {
                    ObjectResult = objResult,
                    SubjectResult = objResult
                };
            }
        }
    }
    public struct TryOnResult
    {
        public State ObjectResult;
        public State SubjectResult;
    }
}
