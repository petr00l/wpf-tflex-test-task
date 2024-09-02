using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal class Circle(float radius) : Ellipse(radius, radius)
    {
        protected override void CalcSpecific()
        {
            if (A <= 0)
                throw new ArgumentException("Argument Radius must be positive!");
        }
    }
}
