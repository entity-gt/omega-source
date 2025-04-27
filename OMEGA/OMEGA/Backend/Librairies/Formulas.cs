using System;
using UnityEngine;

namespace OMEGA.Backend.Librairies
{
    internal static class Formulas
    {
        public static Vector2 Circle(float Radius, float Angle)
        {
            float x = (float)(Radius * Math.Cos(Angle * Math.PI / 180f));
            float y = (float)(Radius * Math.Sin(Angle * Math.PI / 180f));

            return new Vector2(x, y);
        }
    }
}
