using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace _3dEngineWPF
{
    public class Vec3
    {
        float x;
        float y;
        float z;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public float Length()
        {
            if (Vector.IsHardwareAccelerated)
            {
                float ls = Vec3.Dot(this, this);
                return (float)System.Math.Sqrt(ls);
            }
            else
            {
                float ls = X * X + Y * Y + Z * Z;
                return (float)System.Math.Sqrt(ls);
            }
        }
        public static Vec3 Normalize(Vec3 value)
        {
            if (Vector.IsHardwareAccelerated)
            {
                float length = value.Length();
                return value / length;
            }
            else
            {
                float ls = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
                float length = (float)System.Math.Sqrt(ls);
                return new Vec3(value.X / length, value.Y / length, value.Z / length);
            }
        }
        public static Vec3 Cross(Vec3 vector1, Vec3 vector2)
        {
            return new Vec3
                (
                    vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                    vector1.Z * vector2.X - vector1.X * vector2.Z,
                    vector1.X * vector2.Y - vector1.Y * vector2.X
                );
        }
        public static float Dot(Vec3 vector1, Vec3 vector2)
        {
            return vector1.X * vector2.X +
                   vector1.Y * vector2.Y +
                   vector1.Z * vector2.Z;
        }
        public static Vec3 operator -(Vec3 left, Vec3 right)
        {
            return new Vec3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        public static Vec3 operator +(Vec3 left, Vec3 right)
        {
            return new Vec3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        public static Vec3 operator /(Vec3 value1, float value2)
        {
            float invDiv = 1.0f / value2;

            return new Vec3(
                value1.X * invDiv,
                value1.Y * invDiv,
                value1.Z * invDiv);
        }

        public Vec4 toVec4()
        {
            return new Vec4(x, y, z, 1);
        }
    }
}
