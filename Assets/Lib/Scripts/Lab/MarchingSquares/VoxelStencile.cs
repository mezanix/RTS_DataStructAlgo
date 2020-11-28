namespace FutureGames.Lab
{
    public class VoxelStencile
    {
        protected bool fillType = false;

        protected int radius = 1;
        protected int centerX = 0;
        protected int centerY = 0;

        int globalResolution = 8;

        public int xStart
        {
            get
            {
                int r = centerX - radius;
                return r < 0 ? 0 : r;
            }
        }
        public int xEnd
        {
            get
            {
                int r = centerX + radius;
                return r > globalResolution - 1 ? globalResolution - 1 : r;
            }
        }

        public int yStart
        {
            get
            {
                int r = centerY - radius;
                return r < 0 ? 0 : r;
            }
        }

        public int yEnd
        {
            get
            {
                int r = centerY + radius;
                return r > globalResolution - 1 ? globalResolution - 1 : r;
            }
        }

        public virtual void Init(bool fillType, int radius, int globalResolution)
        {
            this.fillType = fillType;
            this.radius = radius;

            this.globalResolution = globalResolution;
        }

        public virtual bool Apply(int x, int y, bool voxel)
        {
            return fillType;
        }

        public virtual void SetCenter(int x, int y)
        {
            centerX = x;
            centerY = y;
        }
    }
}