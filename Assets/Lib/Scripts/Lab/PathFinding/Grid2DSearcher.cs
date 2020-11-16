using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class Grid2DSearcher
    {
        GridConnected2D grid = null;

        float animationWait = 0.3f;

        public Grid2DSearcher(GridConnected2D grid)
        {
            this.grid = grid;
        }

        public IEnumerator BreadthFirstTravel(GridCell2D start)
        {
            Queue<GridCell2D> frontier = new Queue<GridCell2D>();
            HashSet<GridCell2D> reached = new HashSet<GridCell2D>();

            frontier.Enqueue(start);
            reached.Add(start);

            while(frontier.Count > 0)
            {
                GridCell2D current = frontier.Dequeue();
                foreach(GridCell2D t in current.Neighbors)
                {
                    if (reached.Contains(t))
                        continue;

                    frontier.Enqueue(t);
                    reached.Add(t);
                }

                Debug.Log("BF");

                foreach (GridCell2D t in reached)
                {
                    t.SetColor(Color.white);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }
    }
}