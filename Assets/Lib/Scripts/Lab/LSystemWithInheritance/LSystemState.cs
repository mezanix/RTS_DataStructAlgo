using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemState
    {
        Vector3 position = Vector3.zero;
        float angle = 0f;

        public List<Vector3> positions = new List<Vector3>();
        public List<float> angles = new List<float>();

        float step = 4f;

        internal void GoForward()
        {
            positions.Add(position);
            position += GetStepMove(Vector3.forward);

            angle = 0f;
            angles.Add(angle);
        }

        internal void GoLeft()
        {
            positions.Add(position);
            position += GetStepMove(Vector3.left);

            angle = -90f;
            angles.Add(angle);
        }

        internal void GoRight()
        {
            positions.Add(position);
            position += GetStepMove(Vector3.right);

            angle = 90f;
            angles.Add(angle);
        }

        Vector3 GetStepMove(Vector3 dir)
        {
            return dir * step;
        }
    }
}