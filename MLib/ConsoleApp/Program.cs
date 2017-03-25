using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Models.Subject;
using MLib.Models;
using MLib;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelState model = new ModelState(
                new ModelValue[]
                {
                    new ModelValue("Health", 0, 100),
                    new ModelValue("Sleep", 0, 100),
                    new ModelValue("Hungry", 0, 100)
                }
                );

            State Create(params double[] values) => new State(model, values);

            SubjectAction[] godActions = new SubjectAction[]
            {
                new SubjectAction("Wait", Create(new double[3]), Create(0,0,0)),
                new SubjectAction("Fight", Create(new double[3]), Create(-10, 0, 0)),
                new SubjectAction("Feed", Create(new double[3]), Create(0, 0, 10))
            };

            SubjectAction natureAction = new SubjectAction("Time", Create(new double[3]), Create(5, -5, -5));

            SubjectAction[] monkeyActions = new SubjectAction[]
            {
                new SubjectAction("Sleep", Create(0, 10, 0), Create(new double[3])),
                new SubjectAction("Work", Create(0, -5, 0), Create(new double[3])),

            };

            Subject god = new Subject("God", model, godActions);
            Subject monkey = new Subject("Monkey", model, monkeyActions);

            string command;
            while(true)
            {
                if (monkey.State.Magnitude == 0)
                {
                    Console.WriteLine("Monkey is dead");
                    Console.Read();
                    break;
                }
                var monkeyAction = monkey.Wish();
                Console.WriteLine($"{monkey.Name} {monkeyAction.Action.Action.Name} {monkeyAction.Subject}");

                command = GetCommand(godActions);
                if (command == null) break;
                if (!int.TryParse(command, out int godActionIndex) || !(godActionIndex >= 0 && godActionIndex < godActions.Length))
                    continue;

                var godAction = godActions[godActionIndex];

                natureAction.Apply(god, monkey);

                monkeyAction.Apply();

                godAction.Apply(god, monkey);

                monkey.Apply();
                Console.WriteLine($"{monkey.Name} {monkey.State.Magnitude}");
                Console.WriteLine($"{string.Join("\n", model.Values.Zip(monkey.State.Values, (m, v) => $"{m.Name} {v.Magnitude}"))}");
            }
        }
        static string GetCommand(IEnumerable<SubjectAction> actions)
        {
            Console.WriteLine($"Select one of actions [{string.Join<SubjectAction>(", ", actions)}]");
            return Console.ReadLine();
        }
    }
}
