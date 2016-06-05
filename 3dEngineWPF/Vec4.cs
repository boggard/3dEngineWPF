using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dEngineWPF
{
    public class Vec4
    {
        float x;
        float y;
        float z;
        float w;

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public float W
        {
            get
            {
                return w;
            }

            set
            {
                w = value;
            }
        }

        public Vec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vec4()
        {
        }
        public static Vec4 Transform(Vec4 vector, Matrix matrix)
        {
            return new Vec4(
                vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42,
                vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43,
                vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
        }
        public static float Dot(Vec4 vector1, Vec4 vector2)
        {
            return vector1.X * vector2.X +
                   vector1.Y * vector2.Y +
                   vector1.Z * vector2.Z +
                   vector1.W * vector2.W;
        }
        public static Vec4 operator -(Vec4 left, Vec4 right)
        {
            return new Vec4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }
        public static Vec4 operator +(Vec4 left, Vec4 right)
        {
            return new Vec4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }
        public float Length()
        {
            if (Vector.IsHardwareAccelerated)
            {
                float ls = Vec4.Dot(this, this);
                return (float)System.Math.Sqrt(ls);
            }
            else
            {

                float ls = X * X + Y * Y + Z * Z + W * W;

                return (float)Math.Sqrt((double)ls);
            }

        }
        public Vec3 toVec3()
        {
            return new Vec3(x/w, y/w, z/w);
        }
        public static Vec4 Normalize(Vec4 vector)
        {
                float ls = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
                float invNorm = 1.0f / (float)Math.Sqrt((double)ls);

                return new Vec4(
                    vector.X * invNorm,
                    vector.Y * invNorm,
                    vector.Z * invNorm,
                    vector.W * invNorm);
        }
    }
}
