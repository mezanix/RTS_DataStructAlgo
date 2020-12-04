using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemActionTree : LSystemAction
    {
        public LSystemActionTree(Vector3 direction, float angle, bool spawn, LSystemState state) :
            base(direction, angle, spawn, state)
        {
        }

        public override void DoSpawn(object obj)
        {
            GameObject go = obj as GameObject;

            GameObject clone = Object.Instantiate(go, state.position, Quaternion.Euler(0f, angle, 0f));
        }
    }
}