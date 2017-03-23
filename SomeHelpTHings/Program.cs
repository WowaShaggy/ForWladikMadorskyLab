using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeHelpTHings
{





	class Program
	{
		static double[] X = new[] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 };
		static double[] Y = new[] { 3.0, 3.02, 3.08, 3.18, 3.32, 3.5, 3.72, 3.98, 4.28, 4.62, 5.0 };


		static double P(double x)
		{
			double sum = 0;
			for (int i = 0; i < X.Length; i++)
			{
				sum += Y[i] * Li(x, i);
			}
			return sum;
		}

		static double Li(double x, int i)
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

		static double DfDx(double x)
		{
			var deltaX = 0.0000001;
			return (P(x + deltaX) - P(x)) / (deltaX);
		}

		static double Df2Dx2(double x)
		{
			var deltaX = 0.0000001;
			return (DfDx(x + deltaX) - DfDx(x)) / (deltaX);
		}


		static void Main(string[] args)
		{

			var m = DfDx(0.2);
			var m2 = Df2Dx2(0.2);

			var t = 0.2;
			var a = 10 + 4 * t + 4 * t * t + 3 * (2 * t * t + 3) * (2 * t * t + 3) * (2 * t * t + 3);
		}
	}
}
