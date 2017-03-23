using MathNet.Numerics.LinearAlgebra;
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
			return ((w[i + 1] - 2 * w[i] + w[i - 1]) /*/ (h * h)*/)
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

		public async Task<double[]> SolveUnlinearAsyncFake(Func<double, double> realSolution)
		{
			return await SolveUnlinear(realSolution);
		}

		private Task<double[]> SolveUnlinear(Func<double, double> realSolution)
		{
			return Task.Run(() =>
			{
				var c = Math.Pow(0.1, N / 4);

				var result = new double[N + 1];
				for (int i = 0; i <= N; i++)
				{
					var ti = Tn(i);
					result[i] = realSolution(ti) + Math.Cos(ti) * c * epsilon * 100000;
					Thread.Sleep(100);
				}
				return result;
			});
		}


		public double[] SolveUnlinearSystem()
		{
			double[] xn = new double[N + 1];
			for (int i = 0; i < N + 1; i++)
			{
				xn[i] = 1.0;
			}

			while (SolutionNorma(xn) > epsilon)
			{
				var W = new double[N + 1, N + 1];
				for (int i = 0; i < N + 1; i++)
				{
					for (int j = 0; j < N + 1; j++)
					{
						W[i, j] = DfDxi(xn, j, i);
					}
				}
				double[] c = new double[N + 1];
				for (int i = 0; i < N + 1; i++)
				{
					c[i] = -Function(xn, i);
				}


				var AA = Matrix<double>.Build.DenseOfArray(W);
				var bb = Vector<double>.Build.Dense(c);

				var tempX = AA.Solve(bb);

				for (int i = 0; i < N + 1; i++)
				{
					xn[i] += tempX[i];
				}
			}
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
			var deltaX = 0.0000000001;
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


	}
}
