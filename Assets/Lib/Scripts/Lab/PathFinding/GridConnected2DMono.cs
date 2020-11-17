using UnityEngine;

namespace FutureGames.Lab
{
    public class GridConnected2DMono : MonoBehaviour
    {
        [SerializeField] int width = 5;

        [SerializeField] int height = 5;

        GridConnected2D grid = null;

        Grid2DSearcher searcher = null;

        private void Start()
        {
            grid = new GridConnected2D(width, height);
            grid.Generate();

            searcher = new Grid2DSearcher(grid);
            StartCoroutine(searcher.BreadthFirstTravelPathTrackAstar());
        }

        private void Update()
        {
            searcher.Run();
        }
    }
}