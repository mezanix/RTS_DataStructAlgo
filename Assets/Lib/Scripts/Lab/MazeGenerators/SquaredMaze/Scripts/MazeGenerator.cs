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
            if (Input.GetKeyDown(KeyCode.Space) == true)
                Restart();
        }

        protected virtual void Restart()
        {
            StopAllCoroutines();

            Destroy(mazeInstance.gameObject);

            Begin();
        }

        protected virtual void Begin()
        {
            mazeInstance = Instantiate(mazePrefab) as Maze;
            StartCoroutine(mazeInstance.Generate());
        }
    }
}