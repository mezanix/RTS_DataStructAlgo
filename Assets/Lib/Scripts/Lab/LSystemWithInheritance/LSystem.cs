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

        LSystemStateModifier stateModifier = null;
        LSystemState state = null;

        LSystemActor actor = null;

        GameObject toClone = null;

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
            stateModifier = new LSystemStateModifier(state);

            if(isTree)
                actor = new LSystemActorTree(state, toClone);
            else
                actor = new LSystemActorStreet(state, toClone);
        }

        public void Run()
        {
            // modify the state
            stateModifier.Do(input);

            // act using the state
            actor.Do();
        }
    }
}