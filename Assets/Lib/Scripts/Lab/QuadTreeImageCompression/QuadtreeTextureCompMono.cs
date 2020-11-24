﻿using System;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeTextureCompMono : MonoBehaviour
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

        QuadtreeTextureComp quadtree = null;

        LaplacianOnTextureMono LaplacianMono => GetComponent<LaplacianOnTextureMono>();

        Texture2D SourceImage => LaplacianMono.texture;
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

            quadtree = new QuadtreeTextureComp(new Rectangle(width / 2, height / 2, width / 2, height / 2),
                LaplacianMap);

            ClearQuadtreeTexture();

            quadtree.Show(quadtreeTexture);

            MyRenderer.material.mainTexture = quadtreeTexture;

            Subdivide();

            quadtreeTexture.Apply();
        }

        private void Subdivide()
        {
            quadtree.Subdivide(LaplacianMap);

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