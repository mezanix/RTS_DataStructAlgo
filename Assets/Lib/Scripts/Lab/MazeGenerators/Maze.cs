using System.Collections;
using UnityEngine;

namespace FutureGames.Lab
{
    public class Maze : MonoBehaviour
    {
        [SerializeField]
        Vector2Int size = Vector2Int.one * 20;

        [SerializeField]
        MazeCell cellPrefab = null;

        MazeCell[,] cells = new MazeCell[0, 0];

        [SerializeField]
        float generationStepDelay = 0.01f;

        public IEnumerator Generate()
        {
            WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
            cells = new MazeCell[size.x, size.y];
            Vector2Int coord = RandomCoordinate;
            while(Contains(coord) && GetCell(coord) == null)
            {
                yield return delay;
                CreateCell(coord);
                coord += MazeDirections.RandomValue.ToVector2Int();
            }
        }

        private void CreateCell(Vector2Int coord)
        {
            MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
            newCell.name = "Maze Cell " + coord.x + ", " + coord.y;
            newCell.transform.parent = transform;

            newCell.transform.position = new Vector3(
                coord.x - size.x * 0.5f + 0.5f,
                coord.y - size.y * 0.5f + 0.5f, 
                0f);

            cells[coord.x, coord.y] = newCell;
        }

        public Vector2Int RandomCoordinate => new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));

        public bool Contains(Vector2Int coord)
        {
            return
                coord.x >= 0f && coord.x < size.x &&
                coord.y >= 0f && coord.y < size.y;
        }
        
        public MazeCell GetCell(Vector2Int coord)
        {
            return cells[coord.x, coord.y];
        }
    }
}