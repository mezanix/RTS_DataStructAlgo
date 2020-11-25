using System;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class CompressedTextureGenerator
    {
        QuadtreeTextureCompression quadtree = null;
        Texture2D sourceImage = null;

        public CompressedTextureGenerator(QuadtreeTextureCompression quadtree, Texture2D sourceImage)
        {
            this.quadtree = quadtree;
            this.sourceImage = sourceImage;
        }

        public void Generate(Texture2D texture, Renderer renderer)
        {
            Vector2Int compressedSize = quadtree.CompressedTextureSize();
            texture = new Texture2D(compressedSize.x, compressedSize.y, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Point;


            renderer.material.mainTexture = texture;

            Colorize(texture);

            texture.Apply();
        }

        private void Colorize(Texture2D texture)
        {
            quadtree.Colorize(texture);
        }

        private void DebugQuads(Texture2D texture)
        {
            quadtree.PaintUsingDominantColor(texture);
        }
    }
}