using EquestionSolver;
using SimpleExpressionEvaluator;
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

namespace MadorskyLab1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var eq = new Equestion(
					Convert.ToDouble(alpha.Text),
					Convert.ToDouble(beta.Text),
					Convert.ToDouble(gamma.Text),
					F.Text,
					Convert.ToDouble(a.Text),
					Convert.ToDouble(b.Text),
					Convert.ToDouble(A.Text),
					Convert.ToDouble(B.Text),
					Convert.ToInt32(r.Text),
					Convert.ToInt32(N.Text),
					Convert.ToDouble(epsilon.Text));
				closer.Visibility = Visibility.Visible;

				var solution = await eq.SolveUnlinearAsyncFake(w => 2*w*w + 3);

				closer.Visibility = Visibility.Hidden;
				var n = Convert.ToInt32(N.Text);
				var h = (Convert.ToDouble(b.Text) - Convert.ToDouble(a.Text)) / n;
				ExpressionEvaluator evaluator = new ExpressionEvaluator();

				double[] realSol = null;
				if (!string.IsNullOrEmpty(xReal.Text))
				{
					realSol = Enumerable.Range(0, Convert.ToInt32(N.Text) + 1).Select(w => w * h).Select(w => (double)evaluator.Evaluate(xReal.Text, new { t = w })).ToArray();
				}

				resultView.Items.Clear();
				resultView.Items.Add(GenerateItem("t", "Численное решение", "Точное решение", "Норма"));
				for (int i = 0; i < n + 1; i++)
				{
					resultView.Items.Add(GenerateItem((h * i).ToString(), solution[i].ToString(), realSol == null ? "(none)" : realSol[i].ToString(), realSol == null ? "(none)" : (Math.Abs(realSol[i] - solution[i])).ToString()));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Проверьте корректность введенных данных\n" + ex.Message);
			}
		}

		private Grid GenerateItem(string t, string x1, string x2, string diff)
		{
			var grid = new Grid()
			{
				HorizontalAlignment = HorizontalAlignment.Stretch
			};

			grid.ColumnDefinitions.Add(new ColumnDefinition()
			{
				Width = new GridLength(1, GridUnitType.Star)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition()
			{
				Width = new GridLength(1, GridUnitType.Star)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition()
			{
				Width = new GridLength(1, GridUnitType.Star)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition()
			{
				Width = new GridLength(1, GridUnitType.Star)
			});

			var text1 = new TextBlock()
			{
				Text = string.Format("x({0}) = ", t),
				Foreground = Brushes.Gray,
				HorizontalAlignment = HorizontalAlignment.Stretch,
				TextAlignment = TextAlignment.Center
			};
			Grid.SetColumn(text1, 0);
			grid.Children.Add(text1);


			var text2 = new TextBlock()
			{
				Text = string.Format("{0}", x1),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				TextAlignment = TextAlignment.Center
			};
			Grid.SetColumn(text2, 1);
			grid.Children.Add(text2);

			var text3 = new TextBlock()
			{
				Text = string.Format("{0}", x2),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				TextAlignment = TextAlignment.Center
			};
			Grid.SetColumn(text3, 2);			
			grid.Children.Add(text3);

			var text4 = new TextBlock()
			{
				Text = string.Format("{0}", diff),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				TextAlignment = TextAlignment.Center
			};
			Grid.SetColumn(text4, 3);
			grid.Children.Add(text4);

			return grid;


		}
	}
}
