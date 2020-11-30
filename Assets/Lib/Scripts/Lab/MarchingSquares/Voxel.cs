using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FutureGames.Lab
{
    public class Voxel
    {
        public bool state = false;
        public Vector3 position = Vector3.zero;
        public Vector3 x_edgePosition = Vector3.zero;
        public Vector3 y_edgePosition = Vector3.zero;

        public Voxel (int x, int y, float size)
        {
            position.x = (x + 0.5f) * size;
            position.y = (y + 0.5f) * size;

            x_edgePosition = position;
            x_edgePosition.x += size * 0.5f;

            y_edgePosition = position;
            y_edgePosition.y += size * 0.5f;
        }
    }
}