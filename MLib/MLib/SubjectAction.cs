using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Models.Subject;
using MLib.Models;
namespace MLib
{
    public class SubjectAction
    {
        public string Name { get; private set; }

        public State ObjectEffect => objectEffect.Clone();
        readonly State objectEffect;
        public State SubjectEffect => subjectEffect.Clone();
        readonly State subjectEffect;

        public SubjectAction(string name, State objectEffect, State subjectEffect)
        {
            this.Name = name;
            this.objectEffect = objectEffect;
            this.subjectEffect = subjectEffect;
        }

        public void Apply(Subject obj, Subject subj)
        {
            obj.Apply(objectEffect);
            subj.Apply(subjectEffect);
        }

        public TryOnResult TryOn(State objectState, State subjectState)
        {
            if (objectState != subjectState)
                return new TryOnResult
                {
                    ObjectResult = objectState + objectEffect,
                    SubjectResult = subjectState + subjectEffect
                };
            else
            {
                var objResult = objectState + objectEffect + subjectEffect;
                return new TryOnResult
                {
                    ObjectResult = objResult,
                    SubjectResult = objResult
                };
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public static SubjectAction Fake
        {
            get
            {
                var model = new ModelState(new ModelValue[0]);
                return new SubjectAction("Fake", new State(model), new State(model));
            }
        }
    }
}
