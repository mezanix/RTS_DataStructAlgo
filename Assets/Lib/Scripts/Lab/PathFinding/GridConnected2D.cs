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

                    cells[x, y] = new GridCell2D(this, index, mono);
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

        public Vector3 IndexToPosition(Vector2Int index)
        {
            return Vector3.one * 0.5f + new Vector3(
                index.x * scale.x,
                index.y * scale.y,
                0f);
        }
    }
}