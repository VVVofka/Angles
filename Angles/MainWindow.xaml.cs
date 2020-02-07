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

            s = lstMinAngle.SelectionBoxItem.ToString();
            double minangl = Convert.ToDouble(s.Replace(',', '.'));
            if (minangl == 0)
                minangl = stepangl;

            s = lstMaxAngle.SelectionBoxItem.ToString();
            double maxangl = Convert.ToDouble(s.Replace(',', '.'));

            if (minangl >= maxangl)
            {
                var tmp = minangl;
                minangl = maxangl;
                maxangl = tmp;
            }
            int cntangl = Convert.ToInt32((maxangl - minangl) / stepangl);

            lstChoise.Items.Clear();
            for (double k = minangl; k < maxangl; k += stepangl)
            {
                lstChoise.Items.Add(k);
            }
            int idx = rnd.Next(cntangl);
            s = lstChoise.Items[idx].ToString();
            double angle = Convert.ToDouble(s.Replace(',', '.'));

            double szline = 100;
            double posx = szline + rnd.NextDouble() * (myGrid.ColumnDefinitions[0].ActualWidth - 2 * szline);
            double posy = szline + rnd.NextDouble() * (myGrid.RowDefinitions[0].ActualHeight - 2 * szline);

            ln1.X1 = posx - szline;
            ln1.X2 = posx + szline;
            ln1.Y1 = posy - szline;
            ln1.Y2 = posy + szline;

            ln2.X1 = posx - szline;
            ln2.X2 = posx + szline;
            ln2.Y1 = posy - szline;
            ln2.Y2 = posy + szline;

            double direct = rnd.NextDouble() * 360;
            RotateTransform myRotateTransform1 = new RotateTransform(direct, posx, posy);
            ln1.RenderTransform = myRotateTransform1;
            double zn = -rnd.Next();
            RotateTransform myRotateTransform2 = new RotateTransform(direct + zn * angle, posx, posy);
            ln2.RenderTransform = myRotateTransform2;
            lbAngle.Content = Math.Abs(angle).ToString();
            lbAngle.Visibility = Visibility.Hidden;
            Console.WriteLine("{0}x{1}  {2}x{3} {4}x{5}   {6}x{7} {8}x{9}",
                Convert.ToInt32(posx), Convert.ToInt32(posy),
                Convert.ToInt32(ln1.X1), Convert.ToInt32(ln1.Y1),
                Convert.ToInt32(ln1.X2), Convert.ToInt32(ln1.Y2),
                Convert.ToInt32(ln2.X1), Convert.ToInt32(ln2.Y1),
                Convert.ToInt32(ln2.X2), Convert.ToInt32(ln2.Y2)
                );
        } // //////////////////////////////////////////////////////////////////////////////////////////////////////
        private void lstChoise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstChoise.SelectedItem == null) return;
            lbAngle.Visibility = Visibility.Visible;
            string choise = lstChoise.SelectedItem.ToString();
            string real = lbAngle.Content.ToString();
            if (real == choise)
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
