using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _3dEngineWPF
{
    /// <summary>
    /// Логика взаимодействия для ObjectChange.xaml
    /// </summary>
    public partial class ObjectChange : Window
    {
        public ParamObject result;
        public ObjectChange(ParamObject obj)
        {
            InitializeComponent();
            outside_tower_height.Text = obj.Outside_tower_height.ToString();
            outside_tower_radius.Text = obj.Outside_tower_radius.ToString();
            outside_tower_top_height.Text = obj.Outside_tower_top_height.ToString();
            outside_tower_top_radius.Text = obj.Outside_tower_top_radius.ToString();
            outside_sphere_radius.Text = obj.Outside_tower_sphere_radius.ToString();
            inside_tower_height.Text = obj.Inside_tower_height.ToString();
            inside_tower_radius.Text = obj.Inside_tower_radius.ToString();
            inside_tower_top_height.Text = obj.Inside_tower_top_height.ToString();
            inside_tower_top_radius.Text = obj.Inside_tower_top_radius.ToString();
            inside_sphere_radius.Text = obj.Inside_tower_sphere_radius.ToString();
            outside_wall_lenght.Text = obj.Outside_wall_lenght.ToString();
            inside_wall_lenght.Text = obj.Inside_wall_lenght.ToString();
            outside_box_count.Text = obj.Outside_box_count.ToString();
            result = obj;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((float)Convert.ToDouble(outside_tower_height.Text) <= 0)
                {
                    MessageBox.Show("Высота башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Outside_tower_height = (float)Convert.ToDouble(outside_tower_height.Text);

                if ((float)Convert.ToDouble(outside_tower_radius.Text) <= 0)
                {
                    MessageBox.Show("Радиус башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Outside_tower_radius = (float)Convert.ToDouble(outside_tower_radius.Text);

                if ((float)Convert.ToDouble(outside_tower_top_height.Text) <= 0)
                {
                    MessageBox.Show("Высота верхушки башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Outside_tower_top_height = (float)Convert.ToDouble(outside_tower_top_height.Text);

                if ((float)Convert.ToDouble(outside_tower_top_radius.Text) <= result.Outside_tower_radius || (float)Convert.ToDouble(outside_tower_top_radius.Text) <= 0)
                {
                    MessageBox.Show("Верхушка башни должна иметь радиус больший радиуса самой башни");
                    return;
                }
                else
                    result.Outside_tower_top_radius = (float)Convert.ToDouble(outside_tower_top_radius.Text);

                if ((float)Convert.ToDouble(outside_sphere_radius.Text) >= result.Outside_tower_top_radius || (float)Convert.ToDouble(outside_sphere_radius.Text) <= 0)
                {
                    MessageBox.Show("Сфера верхушки башни должна имерь радиус меньший радиуса самой верхушки");
                    return;
                }
                else
                    result.Outside_tower_sphere_radius = (float)Convert.ToDouble(outside_sphere_radius.Text);

                if ((float)Convert.ToDouble(inside_tower_height.Text) <= 0)
                {
                    MessageBox.Show("Высота внутренней башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Inside_tower_height = (float)Convert.ToDouble(inside_tower_height.Text);

                if ((float)Convert.ToDouble(inside_tower_radius.Text) <= 0)
                {
                    MessageBox.Show("Радиус внутренней башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Inside_tower_radius = (float)Convert.ToDouble(inside_tower_radius.Text);

                if ((float)Convert.ToDouble(inside_tower_top_height.Text) <= 0)
                {
                    MessageBox.Show("Высота верхушки внутренней башни должна задаваться положительным числом");
                    return;
                }
                else
                    result.Inside_tower_top_height = (float)Convert.ToDouble(inside_tower_top_height.Text);

                if ((float)Convert.ToDouble(inside_tower_top_radius.Text) <= result.Inside_tower_radius || (float)Convert.ToDouble(inside_tower_top_radius.Text) <= 0)
                {
                    MessageBox.Show("Верхушка башни должна иметь радиус больший радиуса самой башни");
                    return;
                }
                else
                    result.Inside_tower_top_radius = (float)Convert.ToDouble(inside_tower_top_radius.Text);

                if ((float)Convert.ToDouble(inside_sphere_radius.Text) >= result.Inside_tower_top_radius || (float)Convert.ToDouble(inside_sphere_radius.Text) <= 0)
                {
                    MessageBox.Show("Сфера верхушки башни должна имерь радиус меньший радиуса самой верхушки");
                    return;
                }
                else
                    result.Inside_tower_sphere_radius = (float)Convert.ToDouble(inside_sphere_radius.Text);

                if ((float)Convert.ToDouble(outside_wall_lenght.Text) < result.Outside_tower_top_radius + result.Inside_tower_top_radius || (float)Convert.ToDouble(outside_wall_lenght.Text) < 0)
                {
                    MessageBox.Show("Внешняя стена имеет слишком маленькую длину, башни расположены слишком близко");
                    return;
                }
                else
                    result.Outside_wall_lenght = (float)Convert.ToDouble(outside_wall_lenght.Text);

                if ((float)Convert.ToDouble(inside_wall_lenght.Text) < result.Inside_tower_top_radius + result.Inside_tower_top_radius || (float)Convert.ToDouble(inside_wall_lenght.Text) < 0)
                {
                    MessageBox.Show("Внутренняя стена имеет слишком маленькую длину, башни расположены слишком близко");
                    return;
                }
                else
                    result.Inside_wall_lenght = (float)Convert.ToDouble(inside_wall_lenght.Text);

                if ((float)Convert.ToDouble(outside_box_count.Text) < 0)
                {
                    MessageBox.Show("Количество блоков на стенах должно быть неотрицательным");
                    return;
                }
                else
                    result.Outside_box_count = (float)Convert.ToDouble(outside_box_count.Text);

                this.Close();
            }
            catch { MessageBox.Show("Неверный формат параметров"); return; }
        }
    }
}
