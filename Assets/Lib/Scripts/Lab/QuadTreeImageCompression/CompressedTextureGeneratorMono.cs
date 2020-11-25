using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class CompressedTextureGeneratorMono : MonoBehaviour
    {
        QuadtreeTextureCompressionMono QuadtreeTextureCompressionMono =>
            GetComponent<QuadtreeTextureCompressionMono>();

        QuadtreeTextureCompression Quadtree => QuadtreeTextureCompressionMono.quadtree;
        Texture2D SourceImage => QuadtreeTextureCompressionMono.SourceImage;
        Texture2D debugTexture = null;
        Texture2D compressedTexture = null;

        CompressedTextureGenerator compressedGen = null;

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

        private void Update()
        {
            GenerateCompressedTexture();
        }

        private void GenerateCompressedTexture()
        {
            if (Input.GetKeyDown(KeyCode.G) == false)
                return;

            compressedGen = new CompressedTextureGenerator(Quadtree, SourceImage);

            compressedGen.Generate(compressedTexture, MyRenderer);
        }
    }
}