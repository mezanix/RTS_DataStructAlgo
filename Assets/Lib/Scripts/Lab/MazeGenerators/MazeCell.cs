using UnityEngine;

namespace FutureGames.Lab
{
    public class MazeCell : MonoBehaviour
    {
        public Vector2Int coord = Vector2Int.zero;

        MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.count];

        int initializedEdgeCount = 0;
        public bool IsFullyInitialized => initializedEdgeCount == MazeDirections.count;

        public MazeDirection RandomUninitializedDirection
        {
            get
            {
                int skips = Random.Range(0, MazeDirections.count - initializedEdgeCount);
                for (int i = 0; i < MazeDirections.count; i++)
                {
                    if(edges[i] == null)
                    {
                        if(skips == 0)
                        {
                            return (MazeDirection)i;
                        }
                        skips--;
                    }
                }
                throw new System.InvalidOperationException("MazeCell has all directions initialized.");
            }
        }

        public MazeCellEdge GetEdge(MazeDirection direction)
        {
            return edges[(int)direction];
        }

        public void SetEdge(MazeCellEdge edge, MazeDirection direction)
        {
            edges[(int)direction] = edge;
            initializedEdgeCount++;
        }
    }
}