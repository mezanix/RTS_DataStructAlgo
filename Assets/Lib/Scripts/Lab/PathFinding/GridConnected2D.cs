using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class GridConnected2D
    {
        int width = 5;
        public int Width => width;

        int height = 5;
        public int Height => height;

        Vector2 scale = Vector2.one;

        GridCell2D[,] cells = new GridCell2D[0, 0];
        public GridCell2D[,] Cells => cells;

        public GridConnected2D(int width = 5, int height = 5)
        {
            this.width = width;
            this.height = height;
        }

        Material CellMaterial
        {
            get
            {
                return new Material(Resources.Load<Material>("Materials/UnlitColor"));
            }
        }

        RectInt[] walls = new RectInt[]
        {
            new RectInt(0, 0, 4, 2),
            new RectInt(15, 9, 3, 5),
            new RectInt(4, 5, 2, 11),
            new RectInt(4, 16, 2, 4),
            new RectInt(4, 3, 6, 2)
        };

        Vector2Int Target = new Vector2Int(3, 12);
        Vector2Int Start => new Vector2Int(width / 2, Height / 2);

        Dictionary<int, RectInt> costs = new Dictionary<int, RectInt>()
        {
            //{4, new RectInt(15, 9, 3, 5) },
            //{5, new RectInt(4, 3, 6, 2) },
            //{6, new RectInt(4, 5, 2, 10) }
        };

        public GridCell2D TargetCell => cells[Target.x, Target.y];
        public GridCell2D StartCell => cells[Start.x, Start.y];

        public void Generate()
        {
            cells = new GridCell2D[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2Int index = new Vector2Int(x, y);
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GridCell2DMono mono = go.AddComponent<GridCell2DMono>();
                    mono.SetMaterial(CellMaterial);
                    go.transform.position = IndexToPosition(index);
                    go.transform.localScale = 0.9f * scale.x * Vector3.one;

                    cells[x, y] = new GridCell2D(this, index, mono, GetWalkState(index), GetCost(index));
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y].StoreNeighbors();
                }
            }
        }

        int GetCost(Vector2Int index)
        {
            foreach (int t in costs.Keys)
            {
                if (costs[t].Contains(index))
                    return t;
            }

            return 1;
        }

        WalkState GetWalkState(Vector2Int index)
        {
            WalkState r = WalkState.Walkable;

            if (index == Start)
            {
                r = WalkState.Start;
            }
            else if (index == Target)
            {
                r = WalkState.Target;
            }
            else
            {
                r = InWalls(index) ? WalkState.Wall : WalkState.Walkable;
            }

            return r;
        }

        public Vector3 IndexToPosition(Vector2Int index)
        {
            return Vector3.one * 0.5f + new Vector3(
                index.x * scale.x,
                index.y * scale.y,
                0f);
        }

        bool InWalls(Vector2Int index)
        {
            foreach (RectInt t in walls)
            {
                if (t.Contains(index))
                    return true;
            }

            return false;
        }
    }
}