using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Angles {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		Random rnd = new Random();
		mdSimpleCross mdbase;
		int previdx = -1;
		public MainWindow() {
			InitializeComponent();
			mdbase = new mdSimpleCross(myCanvas, ln1, ln2);
		} // ////////////////////////////////////////////////////////////////////////////
		private void lstInit() {
			string s = lstStepsAngles.SelectionBoxItem.ToString();
			if(s.Length == 0)
				return;

			double stepangl = Convert.ToDouble(s.Replace(',', '.'));

			s = lstMinAngle.SelectionBoxItem.ToString();
			if(s.Length == 0)
				return;
			double minangl = Convert.ToDouble(s.Replace(',', '.'));
			if(minangl == 0)
				minangl = stepangl;

			s = lstMaxAngle.SelectionBoxItem.ToString();
			if(s.Length == 0)
				return;
			double maxangl = Convert.ToDouble(s.Replace(',', '.'));

			if(minangl >= maxangl) {
				var tmp = minangl;
				minangl = maxangl;
				maxangl = tmp;
			}
			int cntangl = Convert.ToInt32((maxangl - minangl) / stepangl);

			lstChoise.Items.Clear();
			for(double k = minangl; k < maxangl; k += stepangl)
				lstChoise.Items.Add(k);
		} // ////////////////////////////////////////////////////////////////////////////
		private void btNext_Click(object sender, RoutedEventArgs e) {
			lstInit();
			string s = lstStepsAngles.SelectionBoxItem.ToString();
			double stepangl = Convert.ToDouble(s.Replace(',', '.'));

			int idx, cntidx = 0;
			do {
				idx = rnd.Next(lstChoise.Items.Count);
			} while(previdx == idx && ++cntidx < 999);
			previdx = idx;
			s = lstChoise.Items[idx].ToString();
			double angle = Convert.ToDouble(s.Replace(',', '.'));
			double minsz = Math.Min(myCanvas.ActualWidth, myCanvas.ActualHeight);
			mdbase.placeLine2(minsz * 0.75, minsz * 0.84, angle, 2.0 / 4, 2.0 / 5);
			lbAngle.Content = Math.Abs(angle).ToString();
			lbAngle.Visibility = Visibility.Hidden;
		} // //////////////////////////////////////////////////////////////////////////////////////////////////////
		private void lstChoise_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if(lstChoise.SelectedItem == null)
				return;
			lbAngle.Visibility = Visibility.Visible;
			string choise = lstChoise.SelectedItem.ToString();
			string real = lbAngle.Content.ToString();
			SoundPlayer player = new SoundPlayer();
			if(real == choise) {
				lbAngle.Foreground = Brushes.Green;
				//SystemSounds.Question.Play();
				player.Stream = Properties.Resources.ding;
				player.Play();
				btNext_Click(null, null);
			} else {
				lbAngle.Foreground = Brushes.Red;
				//SystemSounds.Asterisk.Play();
				player.Stream = Properties.Resources.Speech_Sleep;
				player.Play();
			}
		} // //////////////////////////////////////////////////////////////////////////////////////////////////////
		private void lstStepsAngles_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			lstInit();
		} // //////////////////////////////////////////////////////////////////////////////////////////////////
		private void lstMinAngle_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			lstInit();
		} // /////////////////////////////////////////////////////////////////////////////////////////////////////
		private void lstMaxAngle_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			lstInit();
		} // ////////////////////////////////////////////////////////////////////////////////////////////////////
	} // *****************************************************************************************************
}
