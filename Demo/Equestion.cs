using SimpleExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquestionSolver
{
	public class Equestion
	{
		public double alpha;
		public double beta;
		public double gamma;
		public double a;
		public double b;
		public Func<object, decimal> F;
		public double xA;
		public double xB;
		public int r;
		public int N;
		public double h;
		public double epsilon;
		ExpressionEvaluator evaluator = new ExpressionEvaluator();
		public string log = "";


		public Equestion(double alpha, double beta, double gamma, string f, double a, double b, double xA, double xB, int r, int n, double epsilon)
		{
			this.alpha = alpha;
			this.beta = beta;
			this.gamma = gamma;
			this.a = a;
			this.b = b;
			this.xA = xA;
			this.xB = xB;
			this.r = r;
			this.epsilon = epsilon;
			F = evaluator.Compile(f);
			N = n;
			h = (b - a) / N;
		}

		private double Function(double[] w, int i)
		{
			if (i == 0)
			{
				return w[0] - xA;
			}
			if (i == N)
			{
				return w[N] - xB;
			}
			return ((w[i + 1] - 2 * w[i] + w[i - 1]) / (h * h))
				+ alpha * (w[i + 1] - w[i - 1]) / (2 * h)
				+ beta * w[i]
				+ gamma * Math.Pow(w[i], r)
				- (double)F(new { t = Tn(i) });
		}

		private double Tn(int n)
		{
			return a + n * h;
		}

		public Task<double[]> SolveUnlinear()
		{
			return Task.Run(new Func<double[]>(SolveUnlinearSystem));
		}

		public async Task<double[]> SolveUnlinearAsync()
		{
			return await SolveUnlinear();
		}

		public double[] SolveUnlinearSystem()
		{
			log += "Начинаем решение \n\n";

			double[] xn = new double[N + 1];
			for (int i = 0; i < N + 1; i++)
			{
				xn[i] = 1.0 * i;
			}

			log += $"получис все нужные функции выберем начальное приближение: x0 = ({VectorToString(xn)})\n\nДальше вычисляем последующие приближения\n\n\n";


			while (SolutionNorma(xn) > epsilon)
			{
				log += "\n\n====== шаг итерации ======\n\n";
				log += $"норма на данном шаге равна {SolutionNorma(xn)} > {epsilon}";
				var W = new double[N + 1, N + 1];
				for (int i = 0; i < N + 1; i++)
				{
					for (int j = 0; j < N + 1; j++)
					{
						W[i, j] = DfDxi(xn, j, i);
					}
				}

				log += $"W = \n{MatrixToString(W)}\n\n";
				double[] b = new double[N + 1];
				for (int i = 0; i < N + 1; i++)
				{
					b[i] = -Function(xn, i);
				}
				log += $"b = {VectorToString(b)}\n\n";
				////////////// теперь прогонка

				
				var ci = new double[N + 1];
				var di = new double[N + 1];
				var ei = new double[N + 1];

				var alpha = new double[N + 1];
				var beta = new double[N + 1];

				for (int i = 0; i <= N; i++)
				{
					if (i > 0 && i < N)
					{
						ci[i] = W[i, i - 1];
						di[i] = W[i, i];
						ei[i] = W[i, i + 1];
					}
					if (i == 0)
					{
						di[i] = W[i, i];
						ei[i] = W[i, i + 1];
					}
					if (i == N)
					{
						di[i] = W[i, i];
						ci[i] = W[i, i - 1];
					}
				}

				log += $"А именно \n\n c = {VectorToString(ci)}\n d = {VectorToString(di)}\n e = {VectorToString(ei)}\n\n";

				alpha[1] = -ei[0] / di[0];
				beta[1] = b[0] / di[0];

				for (int i = 1; i < N; i++)
				{
					alpha[i + 1] = -ei[i] / (di[i] + ci[i] * alpha[i]);
				}
				for (int i = 1; i < N; i++)
				{
					beta[i + 1] = (-ci[i] * beta[i] + b[i]) / (di[i] + ci[i] * alpha[i]);
				}

				var xx = new double[N + 1];
				xx[N] = (ci[N] * beta[N] + b[N]) / (di[N] + ci[N] * alpha[N]);
				for (int i = N - 1; i >= 0; i--)
				{
					xx[i] = alpha[i + 1] * xx[i + 1] + beta[i + 1];
				}

				log += $"Используя прогонку находим tempX = {VectorToString(xx)}\n\n";

				var summ = -b[N];
				for (int i = 0; i < N + 1; i++)
				{
					summ += W[N - 1, i] * xx[i];
				}

				for (int i = 0; i < N + 1; i++)
				{
					xn[i] += xx[i];
				}

				log += $"Находим x(i+1) = {VectorToString(xn)}\n\n";
			}

			log += $"Найдено решение системы x = {VectorToString(xn)}\n\n";
			return xn;
		}

		private double[] SolveLinearSystem(double[,] w, double[] deltas)
		{
			double[] betas = new double[N + 1];
			double[] alphas = new double[N + 1];
			double[] gammas = new double[N + 1];

			for (int i = 0; i < N + 1; i++)
			{
				betas[i] = -w[i, i];
				if (i > 0)
				{
					alphas[i] = w[i, i - 1];
				}
				if (i < N)
				{
					gammas[i] = w[i, i + 1];
				}
			}

			double[] P = new double[N + 1];
			double[] Q = new double[N + 1];
			P[0] = gammas[0] / betas[0];
			Q[0] = -deltas[0] / betas[0];

			for (int i = 1; i < N + 1; i++)
			{
				P[i] = gammas[i] / (betas[i] - alphas[i] * P[i - 1]);
				Q[i] = (alphas[i] * Q[i - 1] - deltas[i]) / (betas[i] - alphas[i] * P[i - 1]);
			}

			double[] newX = new double[N + 1];
			newX[N] = (alphas[N] * Q[N - 1] - deltas[N]) / (betas[N] - alphas[N] * P[N - 1]);

			for (int i = N - 1; i >= 0; i--)
			{
				newX[i] = P[i] * newX[i + 1] + Q[i];
			}
			return newX;
		}

		private double SolutionNorma(double[] xn)
		{
			double summa = 0;
			for (int i = 0; i < N + 1; i++)
			{
				summa += Math.Pow(Function(xn, i), 2);
			}
			return Math.Sqrt(summa);
		}

		private double DfDxi(double[] x, int i, int funcNum)
		{
			var deltaX = 0.0000001;
			var newX = new double[N + 1];
			for (int j = 0; j < x.Length; j++)
			{
				newX[j] = x[j];
			}
			newX[i] = x[i] + deltaX;
			var aaa = Function(newX, funcNum);
			var bbb = Function(x, funcNum);
			var result = (aaa - bbb) / deltaX;
			return result;
		}

		private string MatrixToString(double[,] matrix)
		{
			string result = "";
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					result += matrix[i, j] + "\t";
				}
				result += "\n";
			}
			return result;
		}

		private string VectorToString(double[] vector)
		{
			string result = "";
			for (int i = 0; i < vector.Length; i++)
			{
				result += vector[i] + "\t";
			}
			return result;
		}


	}
}
