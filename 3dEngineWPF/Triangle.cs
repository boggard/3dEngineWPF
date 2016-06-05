using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _3dEngineWPF
{
    public class Triangle
    {
        Vec3 v1;
        Vec3 v2;
        Vec3 v3;
        Color color;
        public Vec3 V1
        {
            get { return v1; }
            set { v1 = value; }
        }
        public Vec3 V2
        {
            get { return v2; }
            set { v2 = value; }
        }
        public Vec3 V3
        {
            get { return v3; }
            set { v3 = value; }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public Triangle(Vec3 v1, Vec3 v2, Vec3 v3, Color color)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.color = color;
        }
    }
}
