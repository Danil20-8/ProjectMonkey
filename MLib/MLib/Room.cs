using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib
{
    public class Room
    {
        List<Subject> subjects;

        public IEnumerable<Subject> Subjects => subjects;

        public Room(IEnumerable<Subject> subjects)
        {
            this.subjects = subjects.ToList();
            foreach (var s in this.subjects)
                AddSubject(s);
        }

        public void AddSubject(Subject subject)
        {
            RemoveSubject(subject);
            subjects.Add(subject);
            subject.Room = this;
        }
        public void RemoveSubject(Subject subject)
        {
            if (subject.Room != null)
            {
                if (subject.Room == this)
                {
                    subjects.Remove(subject);
                    subject.Room = null;
                }
            }
        }


    }
}
