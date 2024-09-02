using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal class Rectangle(float width, float height) : Figure()
    {
        public float Width { get; private set; } = width;
        public float Height { get; private set; } = height;

        protected override float CalcPerimeter() =>
            (Width + Height) * 2;

        protected override void CalcSpecific()
        {
            if (Width <= 0 || Height <= 0)
                throw new ArgumentException("Arguments Width and Height must be positive!");
        }

        protected override float CalcSquare() =>
            Width * Height;
        
    }
}
