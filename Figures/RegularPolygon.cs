using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal class RegularPolygon(float r, uint n) : Figure()
    {
        public float R { get; private set; } = r;
        public uint N { get; private set; } = n;
        public Point[] Points { get; private set; }

        protected override float CalcPerimeter() =>
            2 * R * MathF.Sin(MathF.PI / N) * N;

        protected override float CalcSquare() =>
            0.5f * N * R * R * MathF.Sin(2 * MathF.PI / N);

        private Point[] CalcPoints()
        {
            Point[] points = new Point[N];

            for (int i = 0; i < N; i++)
                points[i] = new Point(
                    R + R * MathF.Cos(2 * MathF.PI * i / N),
                    R + R * MathF.Sin(2 * MathF.PI * i / N));

            return points;
        }

        protected override void CalcSpecific()
        {
            if (R <= 0)
                throw new ArgumentException("Argument R must be positive!");
            else if (N < 3)
                throw new ArgumentException("Argument N must be not less than 3! Polygon with 2 or less points does not exist");

            Points = CalcPoints();
        }
    }
}
