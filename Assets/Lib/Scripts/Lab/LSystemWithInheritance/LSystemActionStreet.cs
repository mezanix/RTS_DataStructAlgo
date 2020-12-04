using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemActionStreet : LSystemAction
    {
        public LSystemActionStreet(Vector3 direction, float angle, bool spawn, LSystemState state) :
            base(direction, angle, spawn, state)
        {
        }

        public override void DoSpawn(object obj)
        {
            GameObject go = obj as GameObject;

            GameObject clone = Object.Instantiate(go, state.position, Quaternion.Euler(0f, angle, 0f));

            clone.GetComponentInChildren<Renderer>().material.color = AngleToColor(angle);
        }

        Color AngleToColor(float angle)
        {
            switch (angle)
            {
                case 0f:
                    return Color.green;

                case 90f:
                    return Color.red;

                case -90f:
                    return Color.cyan;
            }

            return Color.green;
        }
    }
}