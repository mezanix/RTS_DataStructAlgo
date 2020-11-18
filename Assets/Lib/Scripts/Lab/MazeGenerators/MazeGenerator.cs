using UnityEngine;

namespace FutureGames.Lab
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField]
        Maze mazePrefab = null;
        Maze mazeInstance = null;

        private void Start()
        {
            Begin();
        }

        private void Update()
        {
            Restart();
        }

        private void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Space) == false)
                return;

            StopAllCoroutines();

            Destroy(mazeInstance.gameObject);

            Begin();
        }

        private void Begin()
        {
            mazeInstance = Instantiate(mazePrefab) as Maze;
            StartCoroutine(mazeInstance.Generate());
        }
    }
}