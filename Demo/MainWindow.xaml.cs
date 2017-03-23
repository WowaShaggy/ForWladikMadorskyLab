using EquestionSolver;
using SimpleExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Demo
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

		double[] SolutionLol;
		List<double> dxdx = new List<double>();
		string log = "";
		bool IsContinueSolving = true;
		Stopwatch sw = new Stopwatch();
		double[] solution;
		int n;
		double h;
		Equestion eq;
		double[] realSol;

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			abortBtn.IsEnabled = true;
			IsContinueSolving = true;
			try
			{
				sw.Start();
				eq = new Equestion(
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

				solution = await eq.SolveUnlinearAsync();

				n = Convert.ToInt32(N.Text);
				h = (Convert.ToDouble(b.Text) - Convert.ToDouble(a.Text)) / n;
				ExpressionEvaluator evaluator = new ExpressionEvaluator();

				realSol = null;
				if (!string.IsNullOrEmpty(xReal.Text))
				{
					realSol = Enumerable.Range(0, Convert.ToInt32(N.Text) + 1).Select(w => w * h).Select(w => (double)evaluator.Evaluate(xReal.Text, new { t = w })).ToArray();
				}

				// Ищем нужное N
				if (!string.IsNullOrEmpty(epsilon2.Text))
				{
					currentN.Visibility = Visibility.Visible;
					var firstInt = eq.N;
					bool isContinue = true;
					while (isContinue)
					{
						isContinue = false;
						currentNValue.Text = eq.N.ToString();
						var eps2 = Convert.ToDouble(epsilon2.Text);
						eq.N = eq.N * 2;
						eq.h = eq.h / 2;

						var k = eq.N / firstInt;
						var newSol = (await eq.SolveUnlinearAsync()).Where((w, i) => i % k == 0).ToArray();
						if (!IsContinueSolving)
						{
							return;
						}
						var summ = 0.0;
						for (int i = 0; i < solution.Length; i++)
						{
							summ += Math.Pow(newSol[i] - solution[i], 2.0);
						}
						summ = Math.Sqrt(summ);
						if (summ > eps2)
						{
							isContinue = true;
						}
						solution = newSol;
					}
					eq.N = eq.N / 2;
					closer.Visibility = Visibility.Hidden;
				}

				SomeWork();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Проверьте корректность введенных данных\n" + ex.Message);
			}
		}

		private void SomeWork()
		{
			resultView.Items.Clear();
			resultView.Items.Add(GenerateItem("t", "Численное решение", "Точное решение", "Норма"));
			dxdx = new List<double>();
			sw.Stop();

			SolutionLol = solution.ToArray();
			for (int i = 0; i < n + 1; i++)
			{
				dxdx.Add(i * h);
				resultView.Items.Add(GenerateItem((h * i).ToString(), solution[i].ToString(), realSol == null ? "(none)" : realSol[i].ToString(), realSol == null ? "(none)" : (Math.Abs(realSol[i] - solution[i])).ToString()));
			}

			log = eq.log;
			// рисуем график
			List<double> DfDxi = new List<double>();
			List<double> DfDxi2 = new List<double>();

			for (int i = 0; i < n + 1; i++)
			{
				DfDxi.Add(DfDx(i * h, dxdx.ToArray(), solution));
				DfDxi2.Add(Df2Dx2(i * h, dxdx.ToArray(), solution));
			}

			chartResult.DrawChart(dxdx.ToArray(), DfDxi.ToArray());
			chartResultDx.DrawChart(dxdx.ToArray(), DfDxi2.ToArray(), 10, 120, 220);
			solutionChart.DrawChart(dxdx.ToArray(), solution, 130, 160, 42);

			callDown.Text = $"Потрачено {sw.ElapsedMilliseconds} мс";

			HiderGrid.Visibility = Visibility.Hidden;
			MainGridarya.Visibility = Visibility.Visible;
			closer.Visibility = Visibility.Hidden;
			abortBtn.IsEnabled = false;
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

		double P(double x, double[] X, double[] Y)
		{
			double sum = 0;
			for (int i = 0; i < X.Length; i++)
			{
				sum += Y[i] * Li(x, i, X, Y);
			}
			return sum;
		}

		double Li(double x, int i, double[] X, double[] Y)
		{
			double a1 = 1;
			for (int j = 0; j < X.Length; j++)
			{
				if (i != j)
				{
					a1 *= ((x - X[j]) / (X[i] - X[j]));
				}
			}
			return a1;
		}

		double DfDx(double x, double[] X, double[] Y)
		{
			var deltaX = 0.00001;
			return (P(x + deltaX, X, Y) - P(x, X, Y)) / (deltaX);
		}

		double Df2Dx2(double x, double[] X, double[] Y)
		{
			var deltaX = 0.00001;
			return (DfDx(x + deltaX, X, Y) - DfDx(x, X, Y)) / (deltaX);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				var a = Convert.ToDouble(interpolT.Text);
				interpolX.Text = P(a, dxdx.ToArray(), SolutionLol).ToString();
			}
			catch (Exception ee)
			{
				MessageBox.Show("Неккоректно введены данные");
			}
		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("Данная программа предназначена для решения дифференциальных уравнений специального вида и исследования полученого решения. \n\nЗа любыми вопросами обращайтесь к авторам проекта. Программа не предназначена для корпоративного использования, использовать ее можно только в обучающих целях.");
		}

		public double FF(double t)
		{
			return 10 + 4 * t + 4 * t * t + 3 * (2 * t * t + 3) * (2 * t * t + 3) * (2 * t * t + 3);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			var t = Convert.ToDouble(someT.Text);

			var p = P(t, dxdx.ToArray(), SolutionLol);
			var dp = DfDx(t, dxdx.ToArray(), SolutionLol);
			var dpdp = Df2Dx2(t, dxdx.ToArray(), SolutionLol);

			var leftPart = dpdp + Convert.ToDouble(alpha.Text) * dp + Convert.ToDouble(beta.Text) * p + Convert.ToDouble(gamma.Text) * Math.Pow(p, Convert.ToInt32(r.Text));
			checkResult.Text = $"x''(t) + αx'(t) + βx(t) + γ = F(t)\nx''({t}) + αx'({t}) + βx({t}) + γ = F({t})\n{leftPart} = F(t)\n{leftPart} = {FF(t)}\n\nТочность равна {Math.Abs(FF(t) - leftPart)}";
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			ExportToNotepad(log.Replace("\n", "\r\n"));
		}

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

		static void ExportToNotepad(string text)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo("notepad");
			startInfo.UseShellExecute = false;
			Process notepad = Process.Start(startInfo);
			notepad.WaitForInputIdle();
			IntPtr child = FindWindowEx(notepad.MainWindowHandle, new IntPtr(0), null, null);
			SendMessage(child, 0x000c, 0, text);
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
			IsContinueSolving = false;
			((Button)sender).IsEnabled = false;
			SomeWork();
			log += "\r\n\r\n=====================!!!!!!!!!!!Вычисление прервано!!!!!!!!!!======================";
		}
	}
}
