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

        public void Generate(Texture2D texture)
        {
            DebugQuads(texture);
        }

        private void DebugQuads(Texture2D texture)
        {
            quadtree.PaintUsingDominantColor(texture);
        }
    }
}