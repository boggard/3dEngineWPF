using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dEngineWPF
{
    public class Matrix
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public Matrix(float m11, float m12, float m13, float m14,
                          float m21, float m22, float m23, float m24,
                          float m31, float m32, float m33, float m34,
                          float m41, float m42, float m43, float m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;

            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;

            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;

            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }
        public Matrix()
        {

        }
        public static Matrix Identity
        {
            get {
                return new Matrix
                    (
                          1f, 0f, 0f, 0f,
                          0f, 1f, 0f, 0f,
                          0f, 0f, 1f, 0f,
                          0f, 0f, 0f, 1f
                    );
                }
        }
        public static Matrix Multiply(Matrix value1, Matrix value2)
        {
            Matrix result = Matrix.Identity;
            result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
            result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
            result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
            result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

            result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
            result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
            result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
            result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

            result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
            result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
            result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
            result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

            result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
            result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
            result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
            result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

            return result;
        }

        public static Matrix RotationX(float radians)
        {
            Matrix result=Matrix.Identity;

            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            result.M11 = 1.0f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = cos;
            result.M23 = sin;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = -sin;
            result.M33 = cos;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }
        public static Matrix RotationY(float radians)
        {
            Matrix result=Matrix.Identity;

            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            result.M11 = cos;
            result.M12 = 0.0f;
            result.M13 = -sin;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1.0f;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = sin;
            result.M32 = 0.0f;
            result.M33 = cos;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }
        public static Matrix RotationZ(float radians)
        {
            Matrix result=Matrix.Identity;

            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            result.M11 = cos;
            result.M12 = sin;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = -sin;
            result.M22 = cos;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1.0f;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }
        public static Matrix CreateLookAt(Vec3 cameraPosition, Vec3 cameraTarget, Vec3 cameraUpVector)
        {
            Vec3 zaxis = Vec3.Normalize(cameraPosition - cameraTarget);
            Vec3 xaxis = Vec3.Normalize(Vec3.Cross(zaxis,cameraUpVector));
            Vec3 yaxis = Vec3.Cross(zaxis,xaxis);

            Matrix result=Matrix.Identity;

            result.M11 = xaxis.X;
            result.M12 = yaxis.X;
            result.M13 = zaxis.X;
            result.M14 = 0.0f;
            result.M21 = xaxis.Y;
            result.M22 = yaxis.Y;
            result.M23 = zaxis.Y;
            result.M24 = 0.0f;
            result.M31 = xaxis.Z;
            result.M32 = yaxis.Z;
            result.M33 = zaxis.Z;
            result.M34 = 0.0f;
            result.M41 = -Vec3.Dot(xaxis, cameraPosition);
            result.M42 = -Vec3.Dot(yaxis, cameraPosition);
            result.M43 = -Vec3.Dot(zaxis, cameraPosition);
            result.M44 = 1.0f;

            return result;
        }
        public static Matrix CreatePerspective(float fov, float aspect, float near, float far)
        {
            Matrix result = Matrix.Identity;

            double yScale = Convert.ToSingle( 1 / Math.Tan((fov /2)*(Math.PI/180)));
            double xScale = yScale / aspect;

            result.M11 = (float) xScale;
            result.M22 = (float) yScale;
            result.M33 = far / (far - near);
            result.M34 = 1.0f;
            result.M43 = -near * far / (far - near);

            return result;
        }
        public static Matrix CreateTranslate(float xPosition, float yPosition, float zPosition)
        {
            Matrix result = new Matrix();

            result.M11 = 1.0f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1.0f;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1.0f;
            result.M34 = 0.0f;

            result.M41 = xPosition;
            result.M42 = yPosition;
            result.M43 = zPosition;
            result.M44 = 1.0f;

            return result;
        }
        public static Matrix CreateScale(float xScale, float yScale, float zScale)
        {
            Matrix result= new Matrix();

            result.M11 = xScale;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = yScale;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = zScale;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }
        public bool isIdentity()
        {
            return M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && 
                                    M12 == 0f && M13 == 0f && M14 == 0f &&
                       M21 == 0f && M23 == 0f && M24 == 0f &&
                       M31 == 0f && M32 == 0f && M34 == 0f &&
                       M41 == 0f && M42 == 0f && M43 == 0f;
        }
    }
}
