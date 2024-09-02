using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_TestTask_TFLEX
{
    internal class ShapeFactory
    {
        public (System.Windows.Shapes.Shape, Label) CreateShape(Figures.Figure figure)
        {
            System.Windows.Shapes.Shape shape = figure.FigureType switch
            {
                nameof(Figures.Rectangle) or
                nameof(Figures.Square) => new System.Windows.Shapes.Rectangle()
                {
                    Width = (figure as Figures.Rectangle).Width,
                    Height = (figure as Figures.Rectangle).Height,
                },
                nameof(Figures.Ellipse) or
                nameof(Figures.Circle) => new System.Windows.Shapes.Ellipse()
                {
                    Width = 2 * (figure as Figures.Ellipse).A,
                    Height = 2 * (figure as Figures.Ellipse).B
                },
                nameof(Figures.RegularPolygon) or
                nameof(Figures.RegularTriangle) => new System.Windows.Shapes.Polygon()
                {
                    Width = 2 * (figure as Figures.RegularPolygon).R,
                    Height = 2 * (figure as Figures.RegularPolygon).R,
                    Points = new (
                        (figure as Figures.RegularPolygon).Points
                            .Select(p => new Point(p.x, p.y)))
                },
                _ => throw new NotImplementedException($"ShapeFactory.CreateChape: Passed figure type [{figure.FigureType}] not implemented")
            };

            shape.Stroke = Brushes.Black;
            shape.StrokeThickness = 2;

            Label label = new()
            {
                Content = $"Fig: {figure.FigureType}\nP = {figure.Perimeter}\nS = {figure.Square}"
            };

            return (shape, label);
        }
    }
}
