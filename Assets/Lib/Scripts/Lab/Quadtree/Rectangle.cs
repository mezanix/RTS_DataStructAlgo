namespace FutureGames.Lab.QuadtreeSpace
{
    public class Rectangle
    {
        public float centerX = 0f;
        public float centerY = 0f;

        float width = 0f;
        float height = 0f;

        public float HalfWidth => 0.5f * width;
        public float HalfHeight => 0.5f * height;

        public float DoubleWidth => 2f * width;
        public float DoubleHeight => 2f * height;

        public float HalfToWest => centerX - HalfWidth;
        public float HalfToEast => centerX + HalfWidth;

        public float HalfToNorth => centerY + HalfHeight;
        public float HalfToSouth => centerY - HalfHeight;

        public float West => centerX - width;
        public float East => centerX + width;

        public float South => centerY - height;
        public float North => centerY + height;

        public Rectangle NorthEast => new Rectangle(HalfToEast, HalfToNorth, HalfWidth, HalfHeight);
        public Rectangle NorthWest => new Rectangle(HalfToWest, HalfToNorth, HalfWidth, HalfHeight);

        public Rectangle SouthEast => new Rectangle(HalfToEast, HalfToSouth, HalfWidth, HalfHeight);
        public Rectangle SouthWest => new Rectangle(HalfToWest, HalfToSouth, HalfWidth, HalfHeight);

        public Rectangle(float centerX, float centerY, float width, float height)
        {
            this.centerX = centerX;
            this.centerY = centerY;

            this.width = width;
            this.height = height;
        }

        public bool Contains(Point point)
        {
            return InsideX(point.x) && InsideY(point.y);
        }

        public bool InsideX(float x)
        {
            return x >= West && x <= East;
        }

        public bool InsideY(float y)
        {
            return y >= South && y <= North;
        }
    }
}