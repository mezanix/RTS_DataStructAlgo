using System.Collections;
using System.Collections.Generic;
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

            List<MazeCell> activeCells = new List<MazeCell>();
            FirstGenerationStep(activeCells);

            while(activeCells.Count > 0)
            {
                yield return delay;
                //CreateCell(coord);
                //coord += MazeDirections.RandomValue.ToVector2Int();

                NextGenerationStep(activeCells);
            }

            Debug.Log("done");
        }

        void FirstGenerationStep(List<MazeCell> activeCells)
        {
            activeCells.Add(CreateCell(RandomCoordinate));
        }

        void NextGenerationStep(List<MazeCell> activeCells)
        {
            int currentIndex = activeCells.Count - 1;
            MazeCell currentCell = activeCells[currentIndex];
            MazeDirection direction = MazeDirections.RandomValue;
            Vector2Int coord = currentCell.coord + direction.ToVector2Int();

            if (Contains(coord) && GetCell(coord) == null)
                activeCells.Add(CreateCell(coord));
            else
                activeCells.RemoveAt(currentIndex);
        }

        private MazeCell CreateCell(Vector2Int coord)
        {
            MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
            newCell.name = "Maze Cell " + coord.x + ", " + coord.y;
            newCell.transform.parent = transform;

            newCell.transform.position = new Vector3(
                coord.x - size.x * 0.5f + 0.5f,
                coord.y - size.y * 0.5f + 0.5f, 
                0f);

            newCell.coord = coord;

            cells[coord.x, coord.y] = newCell;

            return newCell;
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