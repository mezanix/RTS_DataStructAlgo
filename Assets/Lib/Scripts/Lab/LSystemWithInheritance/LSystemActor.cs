using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemActor
    {
        protected LSystemState state = null;

        protected LSystemInterpreter interpreter = new LSystemInterpreter();

        GameObject toClone = null;

        public LSystemActor(LSystemState state, LSystemInterpreter interpreter, GameObject toClone)
        {
            this.state = state;
            this.interpreter = interpreter;
            this.toClone = toClone;
        }

        public void Do(string input, bool isTree)
        {
            LSystemAction[] actions = interpreter.Interpret(input, state, isTree);

            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Do(toClone);
            }
        }
    }
}