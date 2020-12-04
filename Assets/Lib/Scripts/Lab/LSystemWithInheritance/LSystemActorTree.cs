using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemActorTree : LSystemActor
    {
        GameObject branch = null;

        public LSystemActorTree(LSystemState state, GameObject branch) : base(state)
        {
            this.branch = branch;
        }

        public override void Do()
        {
            for (int i = 0; i < state.positions.Count; i++)
            {
                Object.Instantiate(branch, state.positions[i], Quaternion.Euler(0f, state.angles[i], 0f));
            }
        }
    }
}