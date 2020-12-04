using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    /// <summary>
    /// Hold a set of rules
    /// Interprets according to the rules
    /// </summary>
    public class LSystemInterpreter
    {
        public LSystemAction[] Interpret(string input, LSystemState state, bool isTree)
        {
            string inputTransformed = TransformInput(input);

            List<LSystemAction> r = new List<LSystemAction>();

            foreach (char c in inputTransformed)
            {
                switch (c)
                {
                    case 'F': // spawn in forward direction
                        r.Add(CreateLSystemAction(isTree, Vector3.zero, 0f, true, state));
                        break;
                    case 'f': // move in forward direction
                        r.Add(CreateLSystemAction(isTree, Vector3.forward, 0f, false, state));
                        break;

                    case 'R':
                        r.Add(CreateLSystemAction(isTree, Vector3.zero, 90f, true, state));
                        break;
                    case 'r':
                        r.Add(CreateLSystemAction(isTree, Vector3.right, 90f, false, state));
                        break;

                    case 'L':
                        r.Add(CreateLSystemAction(isTree, Vector3.zero, -90f, true, state));
                        break;
                    case 'l':
                        r.Add(CreateLSystemAction(isTree, Vector3.left, -90f, false, state));
                        break;

                    case 'd':
                        r.Add(CreateLSystemAction(isTree, Vector3.back, 0f, false, state));
                        break;
                }
            }
    
            return r.ToArray();
        }

        LSystemAction CreateLSystemAction(bool isTree, Vector3 dir, float angle, bool spawn, LSystemState state)
        {
            if (isTree)
                return new LSystemActionTree(dir, angle, spawn, state);
            else
                return new LSystemActionStreet(dir, angle, spawn, state);
        }

        private string TransformInput(string input)
        {
            string r = "";

            foreach (char c in input)
            {
                switch (c)
                {
                    case 'F':
                        r += "FfRL";
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