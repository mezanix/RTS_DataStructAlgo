namespace FutureGames.Lab
{
    public class LSystemStateModifier
    {
        LSystemInterpreter interpreter = new LSystemInterpreter();

        LSystemState state = null;

        public LSystemStateModifier(LSystemState state)
        {
            this.state = state;
        }

        public void Do(string input)
        {
            Actions[] actions = interpreter.Interpret(input);

            foreach (Actions a in actions)
            {
                Do(a);
            }
        }

        private void Do(Actions a)
        {
            switch (a)
            {
                case Actions.GoForward:
                    state.GoForward();
                    break;

                case Actions.GoLeft:
                    state.GoLeft();
                    break;

                case Actions.GoRight:
                    state.GoRight();
                    break;
            }
        }
    }
}