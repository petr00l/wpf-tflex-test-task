using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal class Ellipse(float radiusBig, float radiusSmall) : Figure()
    {
        public float A { get; private set; } = radiusBig;
        public float B { get; private set; } = radiusSmall;
        
        protected override float CalcPerimeter() =>
            MathF.PI * MathF.Sqrt(
                2 * (
                A * A + B * B));

        protected override void CalcSpecific()
        {
            if (A <= 0 || B <= 0)
                throw new ArgumentException("Arguments A and B must be positive!");
        }

        protected override float CalcSquare() =>
            MathF.PI * A * B;
    }
}
