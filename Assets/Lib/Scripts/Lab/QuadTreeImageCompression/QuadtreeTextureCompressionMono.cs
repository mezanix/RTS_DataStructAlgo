using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeTextureCompressionMono : MonoBehaviour
    {
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

        Texture2D quadtreeTexture = null;

        public QuadtreeTextureCompression quadtree = null;

        LaplacianOnTextureMono LaplacianMono => GetComponent<LaplacianOnTextureMono>();

        public Texture2D SourceImage => LaplacianMono.texture;
        Texture2D LaplacianMap => LaplacianMono.laplacianMap;

        private void Update()
        {
            GenerateQuadtree();
        }

        private void GenerateQuadtree()
        {
            if (Input.GetKeyDown(KeyCode.Space) == false)
                return;

            int width = SourceImage.width;
            int height = SourceImage.height;

            quadtreeTexture = new Texture2D(width, height,TextureFormat.RGB24, false);
            quadtreeTexture.filterMode = FilterMode.Point;

            quadtree = new QuadtreeTextureCompression(new Rectangle(width / 2, height / 2, width / 2, height / 2),
                LaplacianMap, SourceImage);

            ClearQuadtreeTexture();

            quadtree.Show(quadtreeTexture);

            MyRenderer.material.mainTexture = quadtreeTexture;

            Subdivide();

            quadtreeTexture.Apply();

            Debug.Log("Compressed texture Size: " + quadtree.CompressedTextureSize());
        }

        private void Subdivide()
        {
            quadtree.Subdivide();

            quadtree.Show(quadtreeTexture);
        }

        private void ClearQuadtreeTexture()
        {
            int width = SourceImage.width;
            int height = SourceImage.height;

            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            quadtreeTexture.SetPixels(colors);
            quadtreeTexture.Apply();
        }
    }
}