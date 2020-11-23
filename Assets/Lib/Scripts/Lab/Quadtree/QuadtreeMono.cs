using System;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeMono : MonoBehaviour
    {
        int resolusion = 256;

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

        Texture2D texture = null;

        Quadtree quadtree = null;

        [SerializeField]
        int capacity = 3;



        void Start()
        {
            texture = new Texture2D(resolusion, resolusion, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Point;

            int size = resolusion / 2;
            quadtree = new Quadtree(new Rectangle(size, size, size, size), capacity);

            ClearTexture();

            quadtree.Show(texture);

            MyRenderer.material.mainTexture = texture;
        }

        void Update()
        {
            InsertPoint();
        }

        private void InsertPoint()
        {
            if (Input.GetMouseButtonDown(0) == false)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) == false)
                return;

            Vector2 hitUv = hit.textureCoord;
            Vector2 pixelCoord = new Vector2(hitUv.x * texture.width, hitUv.y * texture.height);

            //Vector2 localPoint = transform.TransformPoint(pixelCoord);

            quadtree.Insert(new Point(pixelCoord.x, pixelCoord.y));


            quadtree.Show(texture);

            texture.Apply();
        }

        private void ClearTexture()
        {
            Color[] colors = new Color[resolusion * resolusion];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            texture.SetPixels(colors);
            texture.Apply();
        }
    }
}