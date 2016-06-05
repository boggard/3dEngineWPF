using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3dEngineWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SceneWork scene=new SceneWork();
        string fname = "scene.txt";
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //image.Source = obj.DrawObject(0,h_Scroll.Value, v_Scroll.Value, d_Scroll.Value, (int)image.Width, (int)image.Height, "");
        }
        private void v_Scroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //image.Source = obj.DrawObject(0,h_Scroll.Value, v_Scroll.Value, d_Scroll.Value, (int)image.Width, (int)image.Height, "");
        }

        private void h_Scroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //image.Source = obj.DrawObject(0,h_Scroll.Value, v_Scroll.Value, d_Scroll.Value, (int)image.Width, (int)image.Height, "");
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (radio_sphere.IsChecked == true)
            {
                if (e.Key == Key.NumPad6)
                {
                    scene.RotateCamera("right");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad4)
                {
                    scene.RotateCamera("left");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad8)
                {
                    scene.RotateCamera("up");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad2)
                {
                    scene.RotateCamera("down");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad5)
                {
                    scene.RotateCamera("front");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad0)
                {
                    scene.RotateCamera("back");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
            }
            if(radio_axis.IsChecked==true)
            {
                if (e.Key == Key.NumPad6)
                {
                    scene.RotateCenter("right");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad4)
                {
                    scene.RotateCenter("left");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad8)
                {
                    scene.RotateCenter("up");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad2)
                {
                    scene.RotateCenter("down");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad5)
                {
                    scene.RotateCenter("front");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (e.Key == Key.NumPad0)
                {
                    scene.RotateCenter("back");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
            }
            if (e.Key == Key.Add)
            {
                if (radio_X.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate((float)Convert.ToDouble(dgr.Text),"x");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (radio_Y.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate((float)Convert.ToDouble(dgr.Text), "y");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (radio_Z.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate((float)Convert.ToDouble(dgr.Text), "z");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
            }
            if (e.Key == Key.Subtract)
            {
                if (radio_X.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate(-1*(float)Convert.ToDouble(dgr.Text), "x");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (radio_Y.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate(-1*(float)Convert.ToDouble(dgr.Text), "y");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
                if (radio_Z.IsChecked == true)
                {
                    scene.objects[list_objects.SelectedIndex].Rotate(-1*(float)Convert.ToDouble(dgr.Text), "z");
                    image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
                }
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            scene.objects.Add(new ParamObject());
            list_objects.Items.Add("Объект "+(scene.objects.Count));
            scene.objects[scene.objects.Count - 1].Outside_tower_height = 500;
            scene.objects[scene.objects.Count - 1].Outside_tower_radius = 100;
            scene.objects[scene.objects.Count - 1].Outside_tower_top_height = 100;
            scene.objects[scene.objects.Count - 1].Outside_tower_top_radius = 150;
            scene.objects[scene.objects.Count - 1].Inside_tower_height = 400;
            scene.objects[scene.objects.Count - 1].Inside_tower_radius = 50;
            scene.objects[scene.objects.Count - 1].Inside_tower_top_height = 50;
            scene.objects[scene.objects.Count - 1].Inside_tower_top_radius = 100;
            scene.objects[scene.objects.Count - 1].Outside_wall_lenght = 500;
            scene.objects[scene.objects.Count - 1].Inside_wall_lenght = 200;
            scene.objects[scene.objects.Count - 1].Outside_tower_sphere_radius = 50;
            scene.objects[scene.objects.Count - 1].Inside_tower_sphere_radius = 25;
            scene.objects[scene.objects.Count - 1].Outside_box_count = 5;
            /*др параметры*/
            ObjectChange form = new ObjectChange(scene.objects[scene.objects.Count - 1]);
            form.Show();
            form.Closed += Form_Closed;
            list_objects.SelectedIndex = scene.objects.Count-1;
            /*scene.objects[scene.objects.Count - 1].triangles = scene.ConstructionObject(new Vec3(-800,-300,0), scene.objects[scene.objects.Count - 1]);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height, "");*/
        }

        private void list_objects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObjectChange form = new ObjectChange(scene.objects[list_objects.SelectedIndex]);
            form.Show();
            form.Closed += Form_Closed;
        }

        private void Form_Closed(object sender, EventArgs e)
        {
            ObjectChange form = sender as ObjectChange;
            scene.objects[list_objects.SelectedIndex] = form.result;
            scene.objects[list_objects.SelectedIndex].triangles = scene.ConstructionObject(new Vec3(0, 0, 0), scene.objects[list_objects.SelectedIndex]);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
                scene.objects[list_objects.SelectedIndex].Translate((float)Convert.ToDouble(pos_x.Text), (float)Convert.ToDouble(pos_y.Text), (float)Convert.ToDouble(pos_z.Text));
                image.Source = scene.DrawObject((int)image.Width, (int)image.Height);           
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            scene.objects.RemoveAt(list_objects.SelectedIndex);
            list_objects.Items.RemoveAt(list_objects.SelectedIndex);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }        
        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            scene.objects[list_objects.SelectedIndex].Scale((float)Convert.ToDouble(scale_x.Text), (float)Convert.ToDouble(scale_y.Text), (float)Convert.ToDouble(scale_z.Text));
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter file = new StreamWriter(fname);
            file.WriteLine(scene.objects.Count());
            file.WriteLine("Положение камеры по X:"+scene.Eye.X);
            file.WriteLine("Положение камеры по Y:" + scene.Eye.Y);
            file.WriteLine("Положение камеры по Z:" + scene.Eye.Z);
            file.WriteLine("Угол по сфере X:" + scene.pos_cam_x);
            file.WriteLine("Угол по сфере Y:" + scene.pos_cam_y);
            file.WriteLine("Положение центра по X:" + scene.Center.X);
            file.WriteLine("Положение центра по Y:" + scene.Center.Y);
            file.WriteLine("Положение центра по Z:" + scene.Center.Z);
            file.WriteLine("Угол по сфере X:" + scene.pos_center_x);
            file.WriteLine("Угол по сфере Y:" + scene.pos_center_y);
            file.WriteLine();
            file.Close();
            foreach (ParamObject obj in scene.objects)
                obj.SaveInFile(fname);
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            StreamReader file = new StreamReader(fname);
            int count = Convert.ToInt32(file.ReadLine());
            scene.Eye.X = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.Eye.Y = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.Eye.Z = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.pos_cam_x = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.pos_cam_y = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.Center.X = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.Center.Y = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.Center.Z = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.pos_center_x = Convert.ToSingle(file.ReadLine().Split(':')[1]);
            scene.pos_center_y= Convert.ToSingle(file.ReadLine().Split(':')[1]);
            file.ReadLine();
            file.Close();
            scene.objects.Clear();
            list_objects.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                scene.objects.Add(ParamObject.UploadFromFile(fname, i));
                scene.objects[i].triangles = scene.ConstructionObject(new Vec3(0, 0, 0), scene.objects[i]);
                list_objects.Items.Add("Объект " + (scene.objects.Count));
            }
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            bool need = scene.Projection_centr;
            scene.Projection_centr = true;
            if(!need)
                image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            bool need = scene.Projection_centr;
            scene.Projection_centr = false;
            if (need)
                image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

       

        private void list_objects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list_objects.SelectedIndex != -1)
            {
                pos_x.Text = scene.objects[list_objects.SelectedIndex].x_pos.ToString();
                pos_y.Text = scene.objects[list_objects.SelectedIndex].y_pos.ToString();
                pos_z.Text = scene.objects[list_objects.SelectedIndex].z_pos.ToString();
                scale_x.Text = scene.objects[list_objects.SelectedIndex].scale_x.ToString();
                scale_y.Text = scene.objects[list_objects.SelectedIndex].scale_y.ToString();
                scale_z.Text = scene.objects[list_objects.SelectedIndex].scale_z.ToString();
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Eye.X = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Eye.Y = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void TextBox_LostFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Eye.Z = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void TextBox_LostFocus_center_1(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Center.X = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void TextBox_LostFocus_center_2(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Center.Y = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void TextBox_LostFocus_center_3(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            scene.Center.Z = Convert.ToSingle(box.Text);
            image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            bool need = scene.Carcas;
            scene.Carcas = false;
            if (need)
                image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            bool need = scene.Carcas;
            scene.Carcas = true;
            if (!need)
                image.Source = scene.DrawObject((int)image.Width, (int)image.Height);
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
