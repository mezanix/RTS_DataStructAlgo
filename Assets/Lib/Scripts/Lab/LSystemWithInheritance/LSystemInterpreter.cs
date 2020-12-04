using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public enum Actions
    {
        GoForward,
        GoRight,
        GoLeft
    }

    /// <summary>
    /// Hold a set of rules
    /// Interprets according to the rules
    /// </summary>
    public class LSystemInterpreter
    {
        public Actions[] Interpret(string input)
        {
            string inputTransformed = TransformInput(input);

            List<Actions> r = new List<Actions>();

            foreach (char c in inputTransformed)
            {
                switch (c)
                {
                    case 'F':
                        r.Add(Actions.GoForward);
                        break;

                    case 'R':
                        r.Add(Actions.GoRight);
                        break;

                    case 'L':
                        r.Add(Actions.GoLeft);
                        break;
                }
            }

            return r.ToArray();
        }

        private string TransformInput(string input)
        {
            string r = "";

            foreach (char c in input)
            {
                switch (c)
                {
                    case 'F':
                        r += "FRLLR";
                        break;

                    default:
                        r += c;
                        break;
                }
            }

            Debug.Log(r);
            return r;
        }
    }
}