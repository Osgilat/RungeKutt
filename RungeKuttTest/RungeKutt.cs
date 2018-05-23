using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extreme.Numerics.QuickStart.CSharp
{
    using Extreme.Mathematics;
    using Extreme.Mathematics.Calculus.OrdinaryDifferentialEquations;

    class RungeKutt
    {
        static void Main(string[] args)
        {
            
            // The ClassicRungeKuttaIntegrator class implements the
            // well-known 4th order fixed step Runge-Kutta method.
            ClassicRungeKuttaIntegrator rk4 = new ClassicRungeKuttaIntegrator();

            // The differential equation is expressed in terms of a 
            // DifferentialFunction delegate. This is a function that
            // takes a double (time value) and two Vectors (y value and
            // return value)  as arguments.
            //
            // The Lorentz function below defines the differential function
            // for the Lorentz attractor.
            rk4.DifferentialFunction = Lorentz;

            // To perform the computations, we need to set the initial time...
            rk4.InitialTime = 0.0;
            // and the initial value.
            rk4.InitialValue = Vector.Create(1.0, 0.0, 0.0);
            // The Runge-Kutta integrator also requires a step size:
            rk4.InitialStepsize = 0.1;

            Console.WriteLine("Classic 4th order Runge-Kutta");
            for (int i = 0; i <= 5; i++)
            {
                double t = 0.2 * i;
                // The Integrate method performs the integration.
                // It takes as many steps as necessary up to
                // the specified time and returns the result:
                var y = rk4.Integrate(t);
                // The IterationsNeeded always shows the number of steps
                // needed to arrive at the final time.
                Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, rk4.IterationsNeeded);
            }

            Console.Write("Press Enter key to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Represents the differential function for the Lorentz attractor.
        /// </summary>
        /// <param name="t">The time value.</param>
        /// <param name="y">The current value.</param>
        /// <param name="dy">On output, the first derivatives.</param>
        /// <returns>A reference to <paramref name="dy"/>.</returns>
        /// <remarks><paramref name="dy"/> may be <see langword="null"/>
        /// on input.</remarks>
        private static Vector<double> Lorentz(double t, Vector<double> y, Vector<double> dy)
        {
            if (dy == null)
                dy = Vector.Create<double>(3);

            double sigma = 10.0;
            double beta = 8.0 / 3.0;
            double rho = 28.0;

            dy[0] = sigma * (y[1] - y[0]);
            dy[1] = y[0] * (rho - y[2]) - y[1];
            dy[2] = y[0] * y[1] - beta * y[2];

            return dy;
        }
    }

   
}

