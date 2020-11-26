using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class LaplacianOnTextureMono : MonoBehaviour
    {
        public Texture2D texture = null;

        LaplacianOnTexture laplacian = null;
        public Texture2D laplacianMap = null;

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

            laplacianMap = laplacian.LaplacianMapOnHSV();

            MyRenderer.material.mainTexture = laplacianMap;
        }
    }
}