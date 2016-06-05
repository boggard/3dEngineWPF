using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _3dEngineWPF
{
    class SceneWork
    {
        private List<Triangle> tr = new List<Triangle>();
        public List<ParamObject> objects = new List<ParamObject>();
        float near=100;
        float far = 5000;
        Vec3 eye = new Vec3(0, 0, -2000);
        Vec3 center = new Vec3(0, 0, 0);
        public float pos_cam_x=180, pos_cam_y=0;
        public float pos_center_x, pos_center_y;
        bool projection_centr = true;
        bool carcas = true;
        public SceneWork()
        {
            //TetraCreate();
            //RectangleCreate(new Vec3(-500,0,0),500, 300, 100, Colors.Yellow);
            //RectangleCreate(new Vec3(200, 0, 0), 500, 300, 100, Colors.Red);
            //tr = ConstructionObject();
        }
        public WriteableBitmap bmp;
        byte[] pixels;
        double[] zBuffer;

        public bool Projection_centr
        {
            get
            {
                return projection_centr;
            }

            set
            {
                projection_centr = value;
            }
        }

        public Vec3 Eye
        {
            get
            {
                return eye;
            }

            set
            {
                eye = value;
            }
        }

        public Vec3 Center
        {
            get
            {
                return center;
            }

            set
            {
                center = value;
            }
        }

        public bool Carcas
        {
            get
            {
                return carcas;
            }

            set
            {
                carcas = value;
            }
        }

        public void RotateCamera(string cam)
        {
            if (cam == "right")
            {
                pos_cam_x += 10;
            }
            if (cam == "left")
            {
                pos_cam_x -= 10;
            }
            if (cam == "up")
            {
                if(pos_cam_y+10<90)
                    pos_cam_y += 10;
            }
            if (cam == "down")
            {
                if (pos_cam_y - 10 > -90)
                    pos_cam_y -= 10;
            }
            if (cam == "front")
            {
                if (eye.Z < 0)
                    eye.Z += 100;
                else
                    eye.Z -= 100;
            }
            if (cam == "back")
            {
                if (eye.Z < 0)
                    eye.Z -= 100;
                else
                    eye.Z += 100;
            }
            float x = center.X;
            float y = center.Y;
            float z = center.Z;
            float R = (eye - center).Length();
            float posz = Convert.ToSingle(z + R * Math.Cos(pos_cam_x * Math.PI / 180) * Math.Cos(pos_cam_y * Math.PI / 180));
            float posy = Convert.ToSingle(y + R * Math.Cos(pos_cam_x * Math.PI / 180) * Math.Sin(pos_cam_y * Math.PI / 180));
            float posx = Convert.ToSingle(x + R * Math.Sin(pos_cam_x * Math.PI / 180));


            eye = new Vec3(posx, posy, posz);
        }
        public void RotateCenter(string cam)
        {
            if (cam == "right")
            {
                pos_center_x += 10;
            }
            if (cam == "left")
            {
                pos_center_x -= 10;
            }
            if (cam == "up")
            {
                pos_center_y += 10;
            }
            if (cam == "down")
            {
                pos_center_y -= 10;
            }
            float x = eye.X;
            float y = eye.Y;
            float z = eye.Z;
            float R = (center-eye).Length();
            float posz = Convert.ToSingle(z + R * Math.Cos(pos_center_x * Math.PI / 180) * Math.Cos(pos_center_y * Math.PI / 180));
            float posy = Convert.ToSingle(y + R * Math.Cos(pos_center_x * Math.PI / 180) * Math.Sin(pos_center_y * Math.PI / 180));
            float posx = Convert.ToSingle(x + R * Math.Sin(pos_center_x * Math.PI / 180));

            center = new Vec3(posx, posy, posz);
        }

        public List<Triangle> ConstructionObject(Vec3 down_left, ParamObject param_obj)
        {
            int segments = 20;
            List<Triangle> obj = new List<Triangle>();
            List<Triangle> left_out_tower = initCyl(down_left.X+param_obj.Outside_tower_radius, down_left.Y, down_left.Z-param_obj.Outside_tower_radius, param_obj.Outside_tower_radius, param_obj.Outside_tower_height, segments);
            foreach (Triangle t in left_out_tower)
            {
                obj.Add(t);
            }
            List<Triangle> left_out_tower_top = initCyl(down_left.X + param_obj.Outside_tower_radius, down_left.Y+param_obj.Outside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Outside_tower_top_radius, param_obj.Outside_tower_top_height, segments);
            foreach (Triangle t in left_out_tower_top)
            {
                obj.Add(t);
            }
            List<Triangle> left_out_tower_sphere = initSph(down_left.X + param_obj.Outside_tower_radius, down_left.Y + param_obj.Outside_tower_top_height+param_obj.Outside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Outside_tower_sphere_radius, segments);
            foreach (Triangle t in left_out_tower_sphere)
            {
                obj.Add(t);
            }
            List<Triangle> left_out_wall = initPar(down_left.X+param_obj.Outside_tower_radius*2, down_left.Y,down_left.Z-param_obj.Outside_tower_radius-param_obj.Inside_tower_radius, param_obj.Inside_tower_height/2, param_obj.Outside_wall_lenght, param_obj.Inside_tower_radius*2);
            foreach (Triangle t in left_out_wall)
            {
                obj.Add(t);
            }

            int box_count = (int)param_obj.Outside_box_count;
            float box_lenght = param_obj.Outside_wall_lenght - param_obj.Inside_tower_radius;
            float box_empty = 20;
            float box_widht = -(box_empty * box_count - box_lenght)/box_count;
            float box_height = 30;

            for (int i = 0; i < box_count; i++)
            {
                List<Triangle> left_out_wall_box = initPar(down_left.X + param_obj.Outside_tower_radius * 2+(box_empty+box_widht)*i, down_left.Y+ param_obj.Inside_tower_height / 2, down_left.Z - param_obj.Outside_tower_radius - param_obj.Inside_tower_radius, box_height, box_widht, param_obj.Inside_tower_radius * 2);
                foreach (Triangle t in left_out_wall_box)
                {
                    obj.Add(t);
                }
            }
            List<Triangle> left_in_tower = initCyl(down_left.X + param_obj.Outside_tower_radius*2+param_obj.Outside_wall_lenght, down_left.Y, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_radius, param_obj.Inside_tower_height, segments);
            foreach (Triangle t in left_in_tower)
            {
                obj.Add(t);
            }
            List<Triangle> left_in_tower_top = initCyl(down_left.X + param_obj.Outside_tower_radius*2 + param_obj.Outside_wall_lenght, down_left.Y + param_obj.Inside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_top_radius, param_obj.Inside_tower_top_height, segments);
            foreach (Triangle t in left_in_tower_top)
            {
                obj.Add(t);
            }
            List<Triangle> left_in_tower_sphere = initSph(down_left.X + param_obj.Outside_tower_radius*2+ param_obj.Outside_wall_lenght, down_left.Y + param_obj.Inside_tower_top_height + param_obj.Inside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_sphere_radius, segments);
            foreach (Triangle t in left_in_tower_sphere)
            {
                obj.Add(t);
            }

            List<Triangle> in_wall = initPar(down_left.X + param_obj.Outside_tower_radius * 2+param_obj.Outside_wall_lenght+param_obj.Inside_tower_radius, down_left.Y, down_left.Z - param_obj.Outside_tower_radius - param_obj.Inside_tower_radius, param_obj.Inside_tower_height / 3*2, param_obj.Inside_wall_lenght, param_obj.Inside_tower_radius * 2);
            foreach (Triangle t in in_wall)
            {
                obj.Add(t);
            }
            float in_box_lenght = param_obj.Inside_wall_lenght;
            float in_box_empty = 10;
            float in_box_widht = -(in_box_empty * box_count - in_box_lenght) / box_count;
            for (int i = 0; i < box_count; i++)
            {
                List<Triangle> left_out_wall_box = initPar(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + (in_box_empty + in_box_widht) * i, down_left.Y + param_obj.Inside_tower_height /3*2, down_left.Z - param_obj.Outside_tower_radius - param_obj.Inside_tower_radius, box_height, in_box_widht, param_obj.Inside_tower_radius * 2);
                foreach (Triangle t in left_out_wall_box)
                {
                    obj.Add(t);
                }
            }
            List<Triangle> right_in_tower = initCyl(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius+param_obj.Inside_wall_lenght+param_obj.Inside_tower_radius, down_left.Y, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_radius, param_obj.Inside_tower_height, segments);
            foreach (Triangle t in right_in_tower)
            {
                obj.Add(t);
            }
            List<Triangle> right_in_tower_top = initCyl(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius, down_left.Y + param_obj.Inside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_top_radius, param_obj.Inside_tower_top_height, segments);
            foreach (Triangle t in right_in_tower_top)
            {
                obj.Add(t);
            }
            List<Triangle> right_in_tower_sphere = initSph(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius, down_left.Y + param_obj.Inside_tower_top_height + param_obj.Inside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Inside_tower_sphere_radius, segments);
            foreach (Triangle t in right_in_tower_sphere)
            {
                obj.Add(t);
            }
            List<Triangle> right_out_wall = initPar(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius, down_left.Y, down_left.Z - param_obj.Outside_tower_radius - param_obj.Inside_tower_radius, param_obj.Inside_tower_height / 2, param_obj.Outside_wall_lenght, param_obj.Inside_tower_radius * 2);
            foreach (Triangle t in right_out_wall)
            {
                obj.Add(t);
            }
            for (int i = 0; i < box_count; i++)
            {
                List<Triangle> left_out_wall_box = initPar(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius*2 + (box_empty + box_widht) * i, down_left.Y + param_obj.Inside_tower_height / 2, down_left.Z - param_obj.Outside_tower_radius - param_obj.Inside_tower_radius, box_height, box_widht, param_obj.Inside_tower_radius * 2);
                foreach (Triangle t in left_out_wall_box)
                {
                    obj.Add(t);
                }
            }
            List<Triangle> right_out_tower = initCyl(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius+param_obj.Outside_wall_lenght+param_obj.Outside_tower_radius, down_left.Y, down_left.Z - param_obj.Outside_tower_radius, param_obj.Outside_tower_radius, param_obj.Outside_tower_height, segments);
            foreach (Triangle t in right_out_tower)
            {
                obj.Add(t);
            }
            List<Triangle> right_out_tower_top = initCyl(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Outside_wall_lenght + param_obj.Outside_tower_radius, down_left.Y + param_obj.Outside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Outside_tower_top_radius, param_obj.Outside_tower_top_height, segments);
            foreach (Triangle t in right_out_tower_top)
            {
                obj.Add(t);
            }
            List<Triangle> right_out_tower_sphere = initSph(down_left.X + param_obj.Outside_tower_radius * 2 + param_obj.Outside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Inside_wall_lenght + param_obj.Inside_tower_radius + param_obj.Outside_wall_lenght + param_obj.Outside_tower_radius, down_left.Y + param_obj.Outside_tower_top_height + param_obj.Outside_tower_height, down_left.Z - param_obj.Outside_tower_radius, param_obj.Outside_tower_sphere_radius, segments);
            foreach (Triangle t in right_out_tower_sphere)
            {
                obj.Add(t);
            }
            return obj;
        }
        
        public WriteableBitmap DrawObject(int Width, int Height)
        {
            
            //Matrix rotate = Matrix.Multiply(Matrix.RotationX((float)(h_dgr * Math.PI / 180)), Matrix.RotationY((float)(v_dgr * Math.PI / 180)));
            //rotate = Matrix.Multiply(rotate, Matrix.RotationZ((float)(d_dgr * Math.PI / 180)));
            bmp = new WriteableBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Bgra32, null);
            Int32Rect rect = new Int32Rect(0, 0, (int)Width, (int)Height);
            pixels = new byte[(int)Width * (int)Height * bmp.Format.BitsPerPixel / 8];
            zBuffer = new double[(int)bmp.Width * (int)bmp.Height];
            for (int q = 0; q < zBuffer.Length; q++)
            {
                zBuffer[q] = Double.NegativeInfinity;
            }           
            Matrix view = Matrix.CreateLookAt(eye, center, new Vec3(0, 1, 0));
            Matrix projection;
            if (projection_centr)
                projection = Matrix.CreatePerspective(-90, (float)Width / Height, 0.1f, far);
            else
                projection = Matrix.Identity;
            foreach (ParamObject obj in objects )
            {
                WriteObjectWithParam(obj.triangles, view, projection, obj.model);
            }
            int stride = (bmp.PixelWidth * bmp.Format.BitsPerPixel) / 8;
            bmp.WritePixels(rect, pixels, stride, 0);
            return bmp;
        }
        public void WriteObjectWithParam(List<Triangle> triangles, Matrix view, Matrix projection, Matrix model)
        {
            foreach (Triangle t in triangles)
            {
                PolygonWrite(t, view, projection, model);
            }
        }
        public void TetraCreate()
        {
            tr.Add(new Triangle(new Vec3(100, 100, 100),
          new Vec3(-100, -100, 100),
          new Vec3(-100, 100, -100),
          Colors.Black));
            tr.Add(new Triangle(new Vec3(100, 100, 100),
                      new Vec3(-100, -100, 100),
                      new Vec3(100, -100, -100),
                      Colors.Red));
            tr.Add(new Triangle(new Vec3(-100, 100, -100),
                      new Vec3(100, -100, -100),
                      new Vec3(100, 100, 100),
                      Colors.Green));
            tr.Add(new Triangle(new Vec3(-100, 100, -100),
                                  new Vec3(100, -100, -100),
                                  new Vec3(-100, -100, 100),
                                  Colors.Blue));
        }

        private List<Triangle> initPar(float xb, float yb, float zb, float length, float width, float height)
        {
            if (length == 0 || height == 0 || width == 0)
            {
                return null;
            }

            List<Triangle> par = new List<Triangle>();

            Vec3 v1 = new Vec3(xb, yb, zb);
            Vec3 v2 = new Vec3(xb + width, yb, zb);
            Vec3 v1h = new Vec3(xb, yb, zb + height);
            Vec3 v2h = new Vec3(xb + width, yb, zb + height);

            Triangle seg = new Triangle(v1, v2, v1h, Colors.LimeGreen);
            par.Add(seg);
            seg = new Triangle(v2h, v2, v1h, Colors.Plum);
            par.Add(seg);

            Vec3 v1l = new Vec3(xb, yb + length, zb);
            Vec3 v2l = new Vec3(xb + width, yb + length, zb);
            Vec3 v1hl = new Vec3(xb, yb + length, zb + height);
            Vec3 v2hl = new Vec3(xb + width, yb + length, zb + height);

            seg = new Triangle(v1l, v2l, v1hl, Colors.Black);
            par.Add(seg);
            seg = new Triangle(v2hl, v2l, v1hl, Colors.Red);
            par.Add(seg);

            seg = new Triangle(v1, v1l, v1hl, Colors.Blue);
            par.Add(seg);
            seg = new Triangle(v1, v1h, v1hl, Colors.Green);
            par.Add(seg);

            seg = new Triangle(v2, v2l, v2hl, Colors.ForestGreen);
            par.Add(seg);
            seg = new Triangle(v2, v2h, v2hl, Colors.Yellow);
            par.Add(seg);

            seg = new Triangle(v1, v1l, v2l, Colors.RoyalBlue);
            par.Add(seg);
            seg = new Triangle(v1, v2, v2l, Colors.Coral);
            par.Add(seg);

            seg = new Triangle(v1h, v1hl, v2hl, Colors.Violet);
            par.Add(seg);
            seg = new Triangle(v1h, v2h, v2hl, Colors.Silver);
            par.Add(seg);


            return par;
        }
        private List<Triangle> initSph(float xb, float yb, float zb, float radius, int segments)
        {
            if (radius <= 0 || segments < 4)
            {
                return null;
            }

            List<Triangle> sph = new List<Triangle>();

            Vec3 center = new Vec3(xb, yb, zb);

            double stepPhi = (1.05 * Math.PI) / segments;
            double stepTeta = Math.PI / segments;
            double newX = 0;
            double newY = 0;
            double newZ = 0;

            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < segments; j++)
                {
                    newX = radius * Math.Sin(j * stepTeta) * Math.Cos(i * stepPhi) + xb;
                    newY = radius * Math.Sin(j * stepTeta) * Math.Sin(i * stepPhi) + yb;
                    newZ = radius * Math.Cos(j * stepTeta) + zb;

                    Vec3 v1 = new Vec3((float) newX,(float) newY,(float) newZ);

                    newX = radius * Math.Sin((j + 1) * stepTeta) * Math.Cos(i * stepPhi) + xb;
                    newY = radius * Math.Sin((j + 1) * stepTeta) * Math.Sin(i * stepPhi) + yb;
                    newZ = radius * Math.Cos((j + 1) * stepTeta) + zb;

                    Vec3 v2 = new Vec3((float) newX, (float) newY, (float) newZ);

                    newX = radius * Math.Sin(j * stepTeta) * Math.Cos((i + 1) * stepPhi) + xb;
                    newY = radius * Math.Sin(j * stepTeta) * Math.Sin((i + 1) * stepPhi) + yb;
                    newZ = radius * Math.Cos(j * stepTeta) + zb;

                    Vec3 v3 = new Vec3((float) newX, (float) newY, (float) newZ);

                    Triangle seg = new Triangle(v1, v2, v3, Colors.Yellow);

                    sph.Add(seg);
                    // в обратную сторону

                    newX = radius * Math.Sin(j * stepTeta) * Math.Cos(i * stepPhi) + xb;
                    newY = radius * Math.Sin(j * stepTeta) * Math.Sin(i * stepPhi) + yb;
                    newZ = radius * Math.Cos(j * stepTeta) + zb;

                    v1 = new Vec3((float) newX, (float) newY, (float) newZ);

                    newX = radius * Math.Sin((j - 1) * stepTeta) * Math.Cos(i * stepPhi) + xb;
                    newY = radius * Math.Sin((j - 1) * stepTeta) * Math.Sin(i * stepPhi) + yb;
                    newZ = radius * Math.Cos((j - 1) * stepTeta) + zb;

                    v2 = new Vec3((float) newX, (float) newY, (float) newZ);

                    newX = radius * Math.Sin(j * stepTeta) * Math.Cos((i - 1) * stepPhi) + xb;
                    newY = radius * Math.Sin(j * stepTeta) * Math.Sin((i - 1) * stepPhi) + yb;
                    newZ = radius * Math.Cos(j * stepTeta) + zb;

                    v3 = new Vec3((float) newX, (float) newY, (float) newZ);

                    seg = new Triangle(v1, v2, v3, Colors.RoyalBlue);

                    sph.Add(seg);

                }

            }

            return sph;
        }
        private List<Triangle> initCyl(float xb, float yb, float zb, float radius, float height, int segments)
        {
            if (radius <= 0 || height == 0 || segments < 4)
            {
                return null;
            }

            List<Triangle> cyl = new List<Triangle>();

            Vec3 center = new Vec3(xb, yb, zb);

            float step = (float) (2 * Math.PI) / segments;
            float newX = 0;
            float newZ = 0;

            for (int i = 0; i < segments; i++)
            {
                newX = (float) (radius * Math.Cos(i * step) + xb);
                newZ= (float) (radius * Math.Sin(i * step) + zb);

                Vec3 v2 = new Vec3(newX, yb, newZ);
                Vec3 v2h = new Vec3(newX, yb+height, newZ);

                newX = (float) (radius * Math.Cos((i + 1) * step) + xb);
                newZ = (float) (radius * Math.Sin((i + 1) * step) + zb);

                Vec3 v3 = new Vec3(newX, yb, newZ);
                Vec3 v3h = new Vec3(newX, yb, newZ);
                Vec3 ch = new Vec3(xb, yb+height, zb);

                Triangle seg;
                if (i % 2 == 0)
                {
                    seg = new Triangle(center, v2, v3, Colors.Red);
                }
                else
                {
                    seg = new Triangle(center, v2, v3, Colors.Pink);
                }
                cyl.Add(seg);


                if (i % 2 == 0)
                {
                    seg = new Triangle(ch, v2h, v3h, Colors.Red);
                }
                else
                {
                    seg = new Triangle(ch, v2h, v3h, Colors.Pink);
                }
                cyl.Add(seg);

                seg = new Triangle(v2, v2h, v3, Colors.Blue);
                cyl.Add(seg);

                seg = new Triangle(v3, v3h, v2h, Colors.Black);
                cyl.Add(seg);
            }

            return cyl;
        }
        public void PolygonWrite(Triangle t, Matrix view, Matrix projection, Matrix model)
        {

            int Width = (int) bmp.Width;
            int Height = (int)bmp.Height;

            Vec4 v1_4 = t.V1.toVec4();
            v1_4 = Vec4.Transform(v1_4, model);
            v1_4 = Vec4.Transform(v1_4, view);
            v1_4 = Vec4.Transform(v1_4, projection);
            
            Vec4 v2_4 = t.V2.toVec4();
            v2_4 = Vec4.Transform(v2_4, model);
            v2_4 = Vec4.Transform(v2_4, view);
            v2_4 = Vec4.Transform(v2_4, projection);
            
            Vec4 v3_4 = t.V3.toVec4();
            v3_4 = Vec4.Transform(v3_4, model);
            v3_4 = Vec4.Transform(v3_4, view);
            v3_4 = Vec4.Transform(v3_4, projection);


            if (!projection.isIdentity())
            {
                v1_4.X *= Width;
                v1_4.Y *= Height;
                v2_4.X *= Width;
                v2_4.Y *= Height;
                v3_4.X *= Width;
                v3_4.Y *= Height;
            }
            if (-v1_4.Z < near || -v2_4.Z < near || -v3_4.Z < near || -v1_4.Z>far || -v1_4.Z>far || -v1_4.Z>far)
                return;

            Color color = t.Color;
            Vec3 v1 = v1_4.toVec3();
            Vec3 v2 = v2_4.toVec3();
            Vec3 v3 = v3_4.toVec3();
            
            v1.X += Width / 2;
            v1.Y += Height / 2;
            v2.X += Width / 2;
            v2.Y += Height / 2;
            v3.X += Width / 2;
            v3.Y += Height / 2;


            int minX = (int)Math.Max(0, Math.Ceiling(Math.Min(v1.X, Math.Min(v2.X, v3.X))));
            int maxX = (int)Math.Min(bmp.Width - 1, Math.Floor(Math.Max(v1.X, Math.Max(v2.X, v3.X))));
            int minY = (int)Math.Max(0, Math.Ceiling(Math.Min(v1.Y, Math.Min(v2.Y, v3.Y))));
            int maxY = (int)Math.Min(bmp.Height - 1, Math.Floor(Math.Max(v1.Y, Math.Max(v2.Y, v3.Y))));
            if(carcas)
            { 
                float max = Math.Abs(Math.Max((v1.X - v2.X), (v1.Y - v2.Y)));
                float xn = v2.X;
                float yn = v2.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v1.X - v2.X) / (max);
                        yn += (v1.Y - v2.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v1.X - v2.X) / (max);
                    yn += (v1.Y - v2.Y) / (max);
                }

                max = Math.Abs(Math.Max((v3.X - v1.X), (v3.Y - v1.Y)));
                xn = v1.X;
                yn = v1.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v3.X - v1.X) / (max);
                        yn += (v3.Y - v1.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v3.X - v1.X) / (max);
                    yn += (v3.Y - v1.Y) / (max);
                }

                max = Math.Abs(Math.Max((v2.X - v3.X), (v2.Y - v3.Y)));
                xn = v3.X;
                yn = v3.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v2.X - v3.X) / (max);
                        yn += (v2.Y - v3.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v2.X - v3.X) / (max);
                    yn += (v2.Y - v3.Y) / (max);
                }
                //again

                max = Math.Abs(Math.Max((v3.X - v2.X), (v3.Y - v2.Y)));
                xn = v2.X;
                yn = v2.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v3.X - v2.X) / (max);
                        yn += (v3.Y - v2.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v3.X - v2.X) / (max);
                    yn += (v3.Y - v2.Y) / (max);
                }

                max = Math.Abs(Math.Max((v1.X - v3.X), (v1.Y - v3.Y)));
                xn = v3.X;
                yn = v3.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v1.X - v3.X) / (max);
                        yn += (v1.Y - v3.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v1.X - v3.X) / (max);
                    yn += (v1.Y - v3.Y) / (max);
                }

                max = Math.Abs(Math.Max((v2.X - v1.X), (v2.Y - v1.Y)));
                xn = v3.X;
                yn = v3.Y;
                for (int i = 1; i <= max + 1; i++)
                {
                    if (xn > maxX || xn < minX || yn > maxY || yn < minY)
                    {
                        xn += (v2.X - v1.X) / (max);
                        yn += (v2.Y - v1.Y) / (max);
                        continue;

                    }
                    int pixelOffset = ((int)xn + (int)yn * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)Colors.Black.B;
                    pixels[pixelOffset + 1] = (byte)Colors.Black.R;
                    pixels[pixelOffset + 2] = (byte)Colors.Black.G;
                    pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                    xn += (v2.X - v1.X) / (max);
                    yn += (v2.Y - v1.Y) / (max);
                }
                return;
            }
            double triangleArea = (v1.Y - v3.Y) * (v2.X - v3.X) + (v2.Y - v3.Y) * (v3.X - v1.X);
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    double b1 = ((y - v3.Y) * (v2.X - v3.X) + (v2.Y - v3.Y) * (v3.X - x)) / triangleArea;
                    double b2 = ((y - v1.Y) * (v3.X - v1.X) + (v3.Y - v1.Y) * (v1.X - x)) / triangleArea;
                    double b3 = ((y - v2.Y) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v2.X - x)) / triangleArea;
                    if (b1 >= 0 && b1 <= 1 && b2 >= 0 && b2 <= 1 && b3 >= 0 && b3 <= 1)
                    {
                        double depth = b1 * v1.Z + b2 * v2.Z + b3 * v3.Z;
                        int zIndex = y * (int)bmp.Width + x;
                        if (zBuffer[zIndex] < depth)
                        { 
                            int pixelOffset = (x + y * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                            pixels[pixelOffset] = (byte)color.B;
                            pixels[pixelOffset + 1] = (byte)color.G;
                            pixels[pixelOffset + 2] = (byte)color.R;
                            pixels[pixelOffset + 3] = (byte)color.A;
                            zBuffer[zIndex] = depth;
                        }
                    }
                }

            }
        }
        public void DrawLine(float x0,float y0,float x1,float y1)
        {
            float deltax = Math.Abs(x1 - x0);
            float deltay = Math.Abs(y1 - y0);
            float error = 0;
            float deltaerr = deltay / deltax;
            float y = y0;
            for (float x = x0; x <= x1; x++)
            {
                int pixelOffset = (int) (x + y * bmp.PixelWidth) * bmp.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)Colors.Black.B;
                pixels[pixelOffset + 1] = (byte)Colors.Black.G;
                pixels[pixelOffset + 2] = (byte)Colors.Black.R;
                pixels[pixelOffset + 3] = (byte)Colors.Black.A;
                error += deltaerr;
                if(error>=0.5)
                {
                    y -= 1;
                    error -= 1;
                }
            }
        }
        public static Color getShade(Color color, double shade)
        {
            Color col = new Color();
            col.R = (byte)(color.R * shade);
            col.G = (byte)(color.G * shade);
            col.B = (byte)(color.B * shade);
            col.A = (byte)(color.A * shade);
            return col;
        }

    }
}
