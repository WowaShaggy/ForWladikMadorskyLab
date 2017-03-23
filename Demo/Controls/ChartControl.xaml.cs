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

namespace Demo.Controls
{
	/// <summary>
	/// Interaction logic for ChartControl.xaml
	/// </summary>
	public partial class ChartControl : UserControl
	{
		public ChartControl()
		{
			InitializeComponent();
		}

		private double[] lastX;
		private double[] lastY;
		private bool isTextVisible = true;

		public void DrawChart(double[] x, double[] y, double colX = -10, double colY = -10, double colZ = -10)
		{
			lastX = x;
			lastY = y;

			if (colX > 0)
			{
				colorX.Value = colX;
				colorY.Value = colY;
				colorZ.Value = colZ;
			}

			var maxX = x.Max();
			var minX = Math.Min(x.Min(), 0);
			var maxY = y.Max() + 3;
			var minY = Math.Min(y.Min(), 0) - 3;

			var stepY = (content.ActualHeight / (maxY - minY));
			var stepX = (content.ActualWidth / (maxX - minX));

			var posXofY = -minX * stepX + 15;
			ordinata.X1 = ordinata.X2 = posXofY;
			ordinata.Y1 = 0;
			ordinata.Y2 = content.ActualHeight;

			yy.Margin = new Thickness(posXofY + 15, 0, 0, 0);
			yy.Padding = new Thickness(4);
			yy.Text = "y";

			topArrow.Visibility = Visibility.Visible;
			topArrow.Margin = new Thickness(posXofY - 3.5, 15, 0, 0);



			var posYofX = content.ActualHeight - (-minY * stepY);
			abscissa.Y1 = abscissa.Y2 = posYofX + 15;
			abscissa.X1 = 0;
			abscissa.X2 = content.ActualWidth;

			xx.Margin = new Thickness(0, posYofX + 10, 0, 0);
			xx.Padding = new Thickness(4);
			xx.Text = "x";

			leftArrow.Visibility = Visibility.Visible;
			leftArrow.Margin = new Thickness(0, posYofX + 15 - 3.5, 15, 0);


			content.Children.Clear();
			for (int i = 0; i < x.Length - 1; i++)
			{
				var line = new Line()
				{
					X1 = (x[i] - minX) * stepX,
					Y1 = content.ActualHeight - ((y[i] - minY) * stepY),
					X2 = (x[i + 1] - minX) * stepX,
					Y2 = content.ActualHeight - ((y[i + 1] - minY) * stepY),
					Stroke = new SolidColorBrush(Color.FromArgb(255, (byte)colorX.Value, (byte)colorY.Value, (byte)colorZ.Value)),
					StrokeThickness = tchcknes.Value
				};
				if (isTextVisible)
				{
					var text = new TextBlock()
					{
						Text = y[i].ToString(),
						FontSize = txtSize.Value,
						Margin = new Thickness((x[i] - minX) * stepX + 10, content.ActualHeight - ((y[i] - minY) * stepY) - 15, 0, 0)
					};
					content.Children.Add(text);
				}
				content.Children.Add(line);
			}
			if (isTextVisible)
			{
				var text2 = new TextBlock()
				{
					Text = y[y.Length - 1].ToString(),
					FontSize = txtSize.Value,
					Margin = new Thickness((x[x.Length - 1] - minX) * stepX, content.ActualHeight - ((y[y.Length - 1] - minY) * stepY), 0, 0)
				};
				content.Children.Add(text2);
			}
		}

		private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (lastX != null && lastY != null)
			{
				DrawChart(lastX, lastY);
			}
		}

		private void colorX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (lastX != null && lastY != null)
			{
				DrawChart(lastX, lastY);
			}
		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			isTextVisible = !isTextVisible;
			if (lastX != null && lastY != null)
			{
				DrawChart(lastX, lastY);
			}
		}
	}
}
