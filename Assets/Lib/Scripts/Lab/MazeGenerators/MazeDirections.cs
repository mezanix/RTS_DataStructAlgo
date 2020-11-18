using UnityEngine;

namespace FutureGames.Lab
{
    public enum MazeDirection
    {
        North,
        East,
        South,
        West
    }

    public static class MazeDirections
    {
        public const int count = 4;

        public static MazeDirection RandomValue => (MazeDirection)Random.Range(0, count);

        static Vector2Int[] vectors = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        public static Vector2Int ToVector2Int(this MazeDirection t)
        {
            return vectors[(int)t];
        }
    }
}