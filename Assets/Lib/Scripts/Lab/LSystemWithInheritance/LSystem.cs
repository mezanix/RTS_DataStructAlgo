using UnityEngine;

namespace FutureGames.Lab
{
    /// <summary>
    /// Holding the string input
    /// Asking for intrepretation of input
    /// Deciding tree or building
    /// Asking for generation
    /// </summary>
    public class LSystem
    {
        string input = "";
        bool isTree = true;

        LSystemState state = null;

        LSystemActor actor = null;

        GameObject toClone = null;

        LSystemInterpreter interpreter = new LSystemInterpreter();

        public LSystem(string input, bool isTree, GameObject toClone)
        {
            this.input = input;
            this.isTree = isTree;
            this.toClone = toClone;

            Init();
        }

        public void Init()
        {
            state = new LSystemState();

            actor = new LSystemActor(state, interpreter, toClone);
        }

        public void Run()
        {
            // act using the state
            actor.Do(input, isTree);
        }
    }
}