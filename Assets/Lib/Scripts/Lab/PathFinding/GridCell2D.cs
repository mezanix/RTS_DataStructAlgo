using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public enum MouseState
    {
        None,
        Hover,
        NeighborOfHover,
    }

    public class GridCell2D
    {
        GridConnected2D grid;
        Vector2Int index = Vector2Int.zero;

        List<GridCell2D> neighbors = new List<GridCell2D>();
        public List<GridCell2D> Neighbors => neighbors;

        GridCell2DMono mono = null;


        public static Color hoverColor = Color.black;
        public static Color neiborColor = Color.blue;

        MouseState mouseState = MouseState.None;

        public GridCell2D(GridConnected2D grid, Vector2Int index, GridCell2DMono mono)
        {
            this.grid = grid;
            this.index = index;

            this.mono = mono;
            this.mono.cell = this;
        }

        public void Run()
        {
            ColorOnMouseState();

            //SetNeighborsMouseState();
        }

        //private void SetNeighborsMouseState()
        //{
        //    switch(mouseState)
        //    {
        //        case MouseState.None:
        //            //SetNeiborsMouseState(MouseState.None);
        //            break;

        //        case MouseState.Hover:
        //            SetNeiborsMouseState(MouseState.NeighborOfHover);
        //            break;

        //        case MouseState.NeighborOfHover:
        //            break;
        //    }    
        //}

        public void SetNeiborsMouseState(MouseState state)
        {
            foreach(GridCell2D t in neighbors)
            {
                t.SetMouseState(state);
            }
        }

        public void SetMouseState(MouseState newState)
        {
            mouseState = newState;
        }

        private void ColorOnMouseState()
        {
            switch(mouseState)
            {
                case MouseState.None:
                    mono.SetColor(Color.white);
                    break;

                case MouseState.Hover:
                    mono.SetColor(hoverColor);
                    break;

                case MouseState.NeighborOfHover:
                    mono.SetColor(neiborColor);
                    break;
            }
        }

        public void StoreNeighbors()
        {
            neighbors.Clear();
            if (IsDownLimit() && !IsDownLeftLimit() && !IsDownRightLimit())
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
                neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }
            else if (IsUpLimit() && !IsUpLeftLimit() && !IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
                neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }

            else if (IsLeftLimit() && !IsDownLeftLimit() && !IsUpLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);
                neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
            }
            else if (IsRightLimit() && !IsDownRightLimit() && !IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
                neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
            }

            else if (IsDownLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }
            else if (IsUpLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }

            else if (IsDownRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
            }
            else if (IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
            }

            else
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);
                neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y]);

                neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x, index.y - 1]);
                neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);
            }
        }

        bool IsDownLimit()
        {
            return index.y == 0;
        }

        bool IsUpLimit()
        {
            return index.y == grid.Height - 1;
        }

        bool IsLeftLimit()
        {
            return index.x == 0;
        }

        bool IsRightLimit()
        {
            return index.x == grid.Width - 1;
        }

        bool IsDownLeftLimit()
        {
            return IsDownLimit() && IsLeftLimit();
        }

        bool IsUpLeftLimit()
        {
            return IsUpLimit() && IsLeftLimit();
        }

        bool IsUpRightLimit()
        {
            return IsUpLimit() && IsRightLimit();
        }

        bool IsDownRightLimit()
        {
            return IsDownLimit() && IsRightLimit();
        }
    }
}