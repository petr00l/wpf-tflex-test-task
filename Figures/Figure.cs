using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_TestTask_TFLEX.Figures
{
    internal abstract class Figure
    {
        public float Square { get; private set; }
        public float Perimeter { get; private set; }
        public string FigureType { get; private set; }

        public Figure()
        {
            Square = CalcSquare();
            Perimeter = CalcPerimeter();
            FigureType = GetType().Name;
            CalcSpecific();
        }

        protected abstract float CalcSquare();
        protected abstract float CalcPerimeter();
        protected abstract void CalcSpecific();
    }
}
