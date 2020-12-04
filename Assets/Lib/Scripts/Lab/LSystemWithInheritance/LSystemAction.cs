using UnityEngine;
using Object = UnityEngine.Object;

namespace FutureGames.Lab
{
    public abstract class LSystemAction
    {
        Vector3 direction = Vector3.zero;
        protected float angle = 0f;
        bool spawn = false;

        protected LSystemState state = null;

        float step = 4f;

        public LSystemAction(Vector3 direction, float angle, bool spawn, LSystemState state)
        {
            this.direction = direction;
            this.angle = angle;
            this.spawn = spawn;

            this.state = state;
        }

        public void Do(object obj)
        {
            DoMove();

            if (spawn)
                DoSpawn(obj);
        }

        private void DoMove()
        {
            state.position += GetStepMove(direction);
        }

        public abstract void DoSpawn(object obj);

        Vector3 GetStepMove(Vector3 dir)
        {
            return dir * step;
        }
    }
}