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
        private static MazeDirection[] opposites =
            {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
        };

        public const int count = 4;

        public static MazeDirection RandomValue => (MazeDirection)Random.Range(0, count);

        static Vector2Int[] vectors = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, -180f),
        Quaternion.Euler(0f, 0f, -270f)
    };

        public static Vector2Int ToVector2Int(this MazeDirection t)
        {
            return vectors[(int)t];
        }

        public static MazeDirection GetOpposite(this MazeDirection t)
        {
            return opposites[(int)t];
        }

        public static Quaternion ToRotation(this MazeDirection t)
        {
            return rotations[(int)t];
        }
    }
}