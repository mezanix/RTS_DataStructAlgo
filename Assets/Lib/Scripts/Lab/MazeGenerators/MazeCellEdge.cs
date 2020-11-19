using UnityEngine;

namespace FutureGames.Lab
{
    public abstract class MazeCellEdge : MonoBehaviour
    {
        public MazeCell cell;
        public MazeCell other;
        public MazeDirection direction;

        public void Init(MazeCell cell, MazeCell other, MazeDirection direction)
        {
            this.cell = cell;
            this.other = other;
            this.direction = direction;

            cell.SetEdge(this, direction);

            transform.parent = cell.transform;
            transform.localPosition = Vector3.zero;

            transform.localRotation = direction.ToRotation();
        }
    }
}