using UnityEngine;

namespace FutureGames.Lab
{
    public class MazeCell : MonoBehaviour
    {
        public Vector2Int coord = Vector2Int.zero;

        MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.count];

        public MazeCellEdge GetEdge(MazeDirection direction)
        {
            return edges[(int)direction];
        }

        public void SetEdge(MazeCellEdge edge, MazeDirection direction)
        {
            edges[(int)direction] = edge;
        }
    }
}