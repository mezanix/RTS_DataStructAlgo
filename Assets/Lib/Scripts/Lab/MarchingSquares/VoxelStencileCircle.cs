namespace FutureGames.Lab
{
    public class VoxelStencileCircle : VoxelStencile
    {
        int sqrRadius = 1;

        public override void Init(bool fillType, int radius, int globalResolution)
        {
            base.Init(fillType, radius, globalResolution);
            sqrRadius = radius * radius;
        }

        public override bool Apply(int x, int y, bool voxel)
        {
            x -= centerX;
            y -= centerY;

            return x * x + y * y <= sqrRadius ? fillType : voxel;
        }
    }
}