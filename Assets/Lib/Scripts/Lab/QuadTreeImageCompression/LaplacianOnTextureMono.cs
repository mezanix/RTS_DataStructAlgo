using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class LaplacianOnTextureMono : MonoBehaviour
    {
        [SerializeField]
        Texture2D texture = null;

        LaplacianOnTexture laplacian = null;
        Texture2D laplacianMap = null;

        Renderer myRenderer = null;
        Renderer MyRenderer
        {
            get
            {
                if (myRenderer == null)
                    myRenderer = GetComponent<Renderer>();
                return myRenderer;
            }
        }

        private void Start()
        {
            laplacian = new LaplacianOnTexture(texture);

            laplacianMap = laplacian.LaplacianMap();

            MyRenderer.material.mainTexture = laplacianMap;
        }
    }
}