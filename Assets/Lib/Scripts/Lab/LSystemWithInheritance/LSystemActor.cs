namespace FutureGames.Lab
{
    public abstract class LSystemActor
    {
        protected LSystemState state = null;

        public LSystemActor(LSystemState state)
        {
            this.state = state;
        }

        public abstract void Do();
    }
}