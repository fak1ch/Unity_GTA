using System;
using UnityEngine;

namespace App.Scripts.Scenes.General.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            vector.x = Math.Clamp(vector.x, min.x, max.x);
            vector.y = Math.Clamp(vector.y, min.y, max.y);
            vector.z = Math.Clamp(vector.z, min.z, max.z);

            return vector;
        }

        public static Vector3 ToEulerAngles(this Vector3 vector)
        {
            return Quaternion.LookRotation(vector).eulerAngles;
        }
        
        public static Vector3 Multiply(this Vector3 left, Vector3 right)
        {
            return new Vector3(right.x * left.x, right.y * left.y, right.z * left.z);
        }
    }
}