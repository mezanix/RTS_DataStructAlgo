using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemActorStreet : LSystemActor
    {
        GameObject street = null;

        public LSystemActorStreet(LSystemState state, GameObject street) : base(state)
        {
            this.street = street;
        }

        public override void Do()
        {
            for (int i = 0; i < state.positions.Count; i++)
            {
                GameObject go = Object.Instantiate(street, state.positions[i], Quaternion.Euler(0f, state.angles[i], 0f));
                go.GetComponentInChildren<MeshRenderer>().material.color = AngleToColor(state.angles[i]);
            }
        }

        Color AngleToColor(float angle)
        {
            switch(angle)
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