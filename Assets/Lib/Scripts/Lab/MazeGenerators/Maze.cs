using System;
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
            for (int x = 0; x < size.x; x++)
            {
                for (int z = 0; z < size.y; z++)
                {
                    yield return delay;
                    CreateCell(x, z);
                }
            }
        }

        private void CreateCell(int x, int z)
        {
            MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
            newCell.name = "Maze Cell " + x + ", " + z;
            newCell.transform.parent = transform;
            newCell.transform.position = new Vector3(x - size.x * 0.5f + 0.5f, 0f, z - size.y * 0.5f + 0.5f);
        }
    }
}