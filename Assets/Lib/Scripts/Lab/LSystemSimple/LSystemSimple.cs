using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemSimple : MonoBehaviour
    {
        [SerializeField]
        GameObject toClone = null;

        [SerializeField]
        string input = "NEIWS";

        class State
        {
            public Vector3 position = Vector3.zero;
            public List<Vector3> positions = new List<Vector3>();
        }

        State state = new State();

        float step = 1f;

        private void Start()
        {
            Generate();
            Draw();
        }

        void Generate()
        {
            state.positions.Clear();

            foreach (char c in input)
            {
                switch (c)
                {
                    case 'N':
                        state.position += GetMove(Vector3.up);
                        state.positions.Add(state.position);
                        break;

                    case 'E':
                        state.position += GetMove(Vector3.right);
                        state.positions.Add(state.position);
                        break;

                    case 'W':
                        state.position += GetMove(Vector3.left);
                        state.positions.Add(state.position);
                        break;

                    case 'S':
                        state.position += GetMove(Vector3.down);
                        state.positions.Add(state.position);
                        break;

                    case 'I':
                        step += 1f;
                        break;

                    case 'D':
                        step -= 1f;
                        break;
                }
            }
        }

        Vector3 GetMove(Vector3 dir)
        {
            return dir * step;
        }

        private void Draw()
        {
            foreach (Vector3 v in state.positions)
            {
                GameObject go = Instantiate(toClone);
                go.transform.position = v;
            }
        }
    }
}