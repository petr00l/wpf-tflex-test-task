using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal class Square(float width) : Rectangle(width, width)
    {
        protected override void CalcSpecific()
        {
            if (Width <= 0)
                throw new ArgumentException("Argument Width must be positive!");
        }
    };
}
