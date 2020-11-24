using UnityEngine;

namespace FutureGames.Lib
{
    public static class ColorExtensions
    {
        public static float HueDiff(this Color t, Color other)
        {
            float h = 0f;
            float s = 0f;
            float v = 0f;
            Color.RGBToHSV(t, out h, out s, out v);
            
            float hOther = 0f;
            float sOther = 0f;
            float vOther = 0f;
            Color.RGBToHSV(other, out hOther, out sOther, out vOther);

            return HueDiff(h, hOther);
        }

        static float HueDiff(float h1, float h2)
        {
            float d = h1 - h2;
            float ad = Mathf.Abs(d);

            if (ad <= 0.5f)
            {
                return ad;
            }
            else if(ad > 0.5f)
            {
                float min = Mathf.Min(h1, h2);
                float max = Mathf.Max(h1, h2);

                return (min - 0f) + (1f - max);
            }
            return 0;
        }

        public static Vector3 GetHsv(this Color t)
        {
            float h = 0f;
            float s = 0f;
            float v = 0f;
            Color.RGBToHSV(t, out h, out s, out v);

            return new Vector3(h, s, v);
        }
    }
}