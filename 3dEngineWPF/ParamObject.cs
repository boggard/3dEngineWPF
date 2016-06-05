using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dEngineWPF
{
    public class ParamObject
    {
        public List<Triangle> triangles;
        public Matrix model;

        public double h_dgr, v_dgr, d_dgr;
        public float x_pos, y_pos, z_pos;
        public float scale_x, scale_y, scale_z;

        private float outside_tower_height;
        private float outside_tower_radius;
        private float outside_tower_top_height;
        private float outside_tower_top_radius;
        private float outside_tower_sphere_radius;
        private float inside_tower_height;
        private float inside_tower_radius;
        private float inside_tower_top_height;
        private float inside_tower_top_radius;
        private float inside_tower_sphere_radius;
        private float outside_wall_lenght;
        private float inside_wall_lenght;
        private float outside_box_count;

        public ParamObject()
        {
            h_dgr = 0;
            v_dgr = 0;
            d_dgr = 0;
            scale_x = 1;
            scale_y = 1;
            scale_z = 1;
            Matrix rotate = Matrix.Multiply(Matrix.RotationX((float)(h_dgr * Math.PI / 180)), Matrix.RotationY((float)(v_dgr * Math.PI / 180)));
            rotate = Matrix.Multiply(rotate, Matrix.RotationZ((float)(d_dgr * Math.PI / 180)));
            model = Matrix.Multiply(Matrix.CreateTranslate(0, 0, 0), Matrix.CreateScale(1, 1, 1));
            model = Matrix.Multiply(model, rotate);
        }
        public void Rotate(float dgr,string rot)
        {
            /*меняем углы поворота  и матрицу модели*/
            if (rot == "x")
            {
                h_dgr += dgr;
            }
            if (rot == "y")
            {
                v_dgr += dgr;
            }
            if (rot == "z")
            {
                d_dgr += dgr;
            }
            CalculateModel();
        }
        public void Translate(float pos_x,float pos_y,float pos_z)
        {
            /*меняем параметры размещения и матрицу модели*/
            x_pos = pos_x;
            y_pos = pos_y;
            z_pos = pos_z;
            CalculateModel();
        }
        public void Scale(float scale_x,float scale_y,float scale_z)
        {
            /*меняем параметры масштабирвоания и матрицу модели*/
            this.scale_x = scale_x;
            this.scale_y = scale_y;
            this.scale_z = scale_z;
            CalculateModel();
        }
        public void CalculateModel()
        {
            Matrix rotate = Matrix.Multiply(Matrix.RotationX((float)(h_dgr * Math.PI / 180)), Matrix.RotationY((float)(v_dgr * Math.PI / 180)));
            rotate = Matrix.Multiply(rotate, Matrix.RotationZ((float)(d_dgr * Math.PI / 180)));
            model = Matrix.Multiply(Matrix.CreateTranslate(x_pos, y_pos, z_pos), Matrix.CreateScale(scale_x, scale_y, scale_z));
            model = Matrix.Multiply(model, rotate);
        }
        public float Outside_tower_height
        {
            get
            {
                return outside_tower_height;
            }

            set
            {
                outside_tower_height = value;
            }
        }

        public float Outside_tower_radius
        {
            get
            {
                return outside_tower_radius;
            }

            set
            {
                outside_tower_radius = value;
            }
        }

        public float Outside_tower_top_height
        {
            get
            {
                return outside_tower_top_height;
            }

            set
            {
                outside_tower_top_height = value;
            }
        }

        public float Outside_tower_top_radius
        {
            get
            {
                return outside_tower_top_radius;
            }

            set
            {
                if (value < outside_tower_radius)
                    outside_tower_top_radius = outside_tower_radius + 20;
                else
                    outside_tower_top_radius = value;
            }
        }

        public float Outside_tower_sphere_radius
        {
            get
            {
                return outside_tower_sphere_radius;
            }

            set
            {
                if (value >= outside_tower_top_radius)
                    outside_tower_sphere_radius = outside_tower_top_radius / 2;
                else
                    outside_tower_sphere_radius = value;
            }
        }
        public float Inside_tower_height
        {
            get
            {
                return inside_tower_height;
            }

            set
            {
                inside_tower_height = value;
            }
        }

        public float Inside_tower_radius
        {
            get
            {
                return inside_tower_radius;
            }

            set
            {
                inside_tower_radius = value;
            }
        }

        public float Inside_tower_top_height
        {
            get
            {
                return inside_tower_top_height;
            }

            set
            {
                inside_tower_top_height = value;
            }
        }

        public float Inside_tower_top_radius
        {
            get
            {
                return inside_tower_top_radius;
            }

            set
            {
                if (value < inside_tower_radius)
                    inside_tower_top_radius = inside_tower_radius + 20;
                else
                    inside_tower_top_radius = value;
            }
        }

        public float Inside_tower_sphere_radius
        {
            get
            {
                return inside_tower_sphere_radius;
            }

            set
            {
                if (value >= inside_tower_top_radius)
                    inside_tower_sphere_radius = inside_tower_top_radius / 2;
                else
                    inside_tower_sphere_radius = value;
            }
        }

        public float Outside_wall_lenght
        {
            get
            {
                return outside_wall_lenght;
            }

            set
            {
                outside_wall_lenght = value;
            }
        }

        public float Inside_wall_lenght
        {
            get
            {
                return inside_wall_lenght;
            }

            set
            {
                inside_wall_lenght = value;
            }
        }

        public float Outside_box_count
        {
            get
            {
                return outside_box_count;
            }

            set
            {
                outside_box_count = value;
            }
        }

        public void SaveInFile(string fname)
        {
            StreamWriter file = new StreamWriter(fname,true);
            file.WriteLine("Поворот по X:" + h_dgr);
            file.WriteLine("Поворот по Y:" + v_dgr);
            file.WriteLine("Поворот по Z:" + d_dgr);
            file.WriteLine("Позиция по X:" + x_pos);
            file.WriteLine("Позиция по Y:" + y_pos);
            file.WriteLine("Позиция по Z:" + z_pos);
            file.WriteLine("Масштабирование по X:" + scale_x);
            file.WriteLine("Масштабирование по Y:" + scale_y);
            file.WriteLine("Масштабирование по Z:" + scale_z);
            file.WriteLine("Высота внешней башни:" + outside_tower_height);
            file.WriteLine("Радиус внешней башни:" + outside_tower_radius);
            file.WriteLine("Высота верхушки внешней башни:" + outside_tower_top_height);
            file.WriteLine("Радиус верхушки внешней башни:" + outside_tower_top_radius);
            file.WriteLine("Радиус сферы внешней башни:" + outside_tower_sphere_radius);
            file.WriteLine("Высота внутренней башни:" + inside_tower_height);
            file.WriteLine("Радиус внутренней башни:" + inside_tower_radius);
            file.WriteLine("Высота верхушки внутренней башни:" + inside_tower_top_height);
            file.WriteLine("Радиус верхушки внутренней башни:" + inside_tower_top_radius);
            file.WriteLine("Радиус сферы внутренней башни:" + inside_tower_sphere_radius);
            file.WriteLine("Длина внешней стены:" + outside_wall_lenght);
            file.WriteLine("Длина внутренней стены:" + inside_wall_lenght);
            file.WriteLine("Количество блоков на внешней стене:" + outside_box_count);
            file.WriteLine();
            file.Close();
        }
        public static ParamObject UploadFromFile(string fname,int index)
        {
            ParamObject newobject = new ParamObject();
            StreamReader file = new StreamReader(fname);
            file.ReadLine();
            for (int i = 0; i < index * 23+11; i++)
                file.ReadLine();
            newobject.h_dgr = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.v_dgr = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.d_dgr = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.x_pos = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.y_pos = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.z_pos = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.scale_x = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.scale_y = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.scale_z = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_tower_height = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_tower_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_tower_top_height = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_tower_top_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_tower_sphere_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_tower_height = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_tower_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_tower_top_height = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_tower_top_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_tower_sphere_radius = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_wall_lenght = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.inside_wall_lenght = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            newobject.outside_box_count = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            file.Close();
            newobject.CalculateModel();
            return newobject;
        }
    }
    
}
