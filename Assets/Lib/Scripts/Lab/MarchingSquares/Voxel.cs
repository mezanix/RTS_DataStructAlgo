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

        public Voxel()
        {

        }

        public Voxel (int x, int y, float size)
        {
            position.x = (x + 0.5f) * size;
            position.y = (y + 0.5f) * size;

            x_edgePosition = position;
            x_edgePosition.x += size * 0.5f;

            y_edgePosition = position;
            y_edgePosition.y += size * 0.5f;
        }

        public void Become_X_DummyOf(Voxel voxel, float offset)
        {
            state = voxel.state;

            position = voxel.position;
            x_edgePosition = voxel.x_edgePosition;
            y_edgePosition = voxel.y_edgePosition;

            position.x += offset;
            x_edgePosition.x += offset;
            y_edgePosition.x += offset;
        }

        public void Become_Y_DummyOf(Voxel voxel, float offset)
        {
            state = voxel.state;

            position = voxel.position;
            x_edgePosition = voxel.x_edgePosition;
            y_edgePosition = voxel.y_edgePosition;

            position.y += offset;
            x_edgePosition.y += offset;
            y_edgePosition.y += offset;
        }

        public void Become_XY_DummyOf(Voxel voxel, float offset)
        {
            state = voxel.state;

            position = voxel.position;
            x_edgePosition = voxel.x_edgePosition;
            y_edgePosition = voxel.y_edgePosition;

            position.x += offset;
            x_edgePosition.x += offset;
            y_edgePosition.x += offset;

            position.y += offset;
            x_edgePosition.y += offset;
            y_edgePosition.y += offset;
        }
    }
}