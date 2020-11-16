using UnityEngine;

namespace FutureGames.Lab
{
    public class GridConnected2DMono : MonoBehaviour
    {
        [SerializeField] int width = 5;

        [SerializeField] int height = 5;

        GridConnected2D grid = null;

        private void Start()
        {
            grid = new GridConnected2D(width, height);
            grid.Generate();
        }
    }
}