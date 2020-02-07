using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Angles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        } // ////////////////////////////////////////////////////////////////////////////

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string s = lstStepsAngles.SelectionBoxItem.ToString();
            double stepangl = Convert.ToDouble(s.Replace(',', '.'));
            int stepangl10 = Convert.ToInt32(stepangl * 10);

            s = lstMinAngle.SelectionBoxItem.ToString();
            double minangl = Convert.ToDouble(s.Replace(',', '.'));
            int minangl10 = Convert.ToInt32(minangl * 10);
            if (minangl10 == 0)
                minangl10 = stepangl10;

            s = lstMaxAngle.SelectionBoxItem.ToString();
            double maxangl = Convert.ToDouble(s.Replace(',', '.'));
            int maxangl10 = Convert.ToInt32(maxangl * 10);

            if (minangl10 >= maxangl10)
            {
                var tmp = minangl10;
                minangl10 = maxangl10;
                maxangl10 = tmp;
            }
            int cntangl = (maxangl10 - minangl10) / stepangl10;

            lstChoise.Items.Clear();
            for (int k = minangl10; k < maxangl10; k += stepangl10)
            {
                lstChoise.Items.Add(Convert.ToDouble(k) * 0.1);
            }
            int idx = rnd.Next(cntangl);
            s = lstChoise.Items[idx].ToString();
            double angle = Convert.ToDouble(s.Replace(',', '.'));

            double posx = rnd.NextDouble() * myGrid.ActualWidth;
            double posy = rnd.NextDouble() * myGrid.ActualHeight;
            ln1.X1 = posx - 100;
            ln1.X2 = posx + 100;
            ln1.Y1 = posy - 100;
            ln1.Y2 = posy + 100;
            ln2.X1 = posx - 100;
            ln2.X2 = posx + 100;
            ln2.Y1 = posy - 100;
            ln2.Y2 = posy + 100;

            double direct = rnd.NextDouble() * 360;
            RotateTransform myRotateTransform1 = new RotateTransform(direct, posx, posy);
            ln1.RenderTransform = myRotateTransform1;
            RotateTransform myRotateTransform2 = new RotateTransform(direct + angle, posx, posy);
            ln2.RenderTransform = myRotateTransform2;
            lbAngle.Content = angle.ToString();
            lbAngle.Visibility = Visibility.Hidden;
        } // //////////////////////////////////////////////////////////////////////////////////////////////////////
        private void lstChoise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstChoise.SelectedItem == null) return;
            lbAngle.Visibility = Visibility.Visible;
            string choise = lstChoise.SelectedItem.ToString();
            string real = lbAngle.Content.ToString();
            if(real == choise)
            {
                lbAngle.Foreground = Brushes.Green;
            }
            else
            {
                lbAngle.Foreground = Brushes.Red;
            }
        } // //////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
