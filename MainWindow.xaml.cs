using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_TestTask_TFLEX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeFactory _shapeFactory;
        Point _lastFigureMargin = new ();
        Grid _currentParametersGrid;
        float _prevMaxRowHeight;

        public MainWindow()
        {
            _shapeFactory = new ();
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            comboBox.ItemsSource = new[]
            {
                nameof(Figures.Circle),
                nameof(Figures.Ellipse),
                nameof(Figures.Rectangle),
                nameof(Figures.RegularPolygon),
                nameof(Figures.RegularTriangle),
                nameof(Figures.Square),
            };
        }

        private void DrawButtonClick(object sender, RoutedEventArgs e)
        {
            Figures.Figure? targetFigure = null;

            try
            {
                targetFigure = comboBox.SelectedItem switch
                {
                    nameof(Figures.Circle) => new Figures.Circle(
                        float.Parse(textBoxCircleRadius.Text)),
                    nameof(Figures.Ellipse) => new Figures.Ellipse(
                        float.Parse(textBoxEllipseA.Text),
                        float.Parse(textBoxEllipseB.Text)),
                    nameof(Figures.Rectangle) => new Figures.Rectangle(
                        float.Parse(textBoxRectangleWidth.Text),
                        float.Parse(textBoxRectangleHeight.Text)),
                    nameof(Figures.RegularPolygon) => new Figures.RegularPolygon(
                        float.Parse(textBoxPolygonR.Text),
                        uint.Parse(textBoxPolygonN.Text)),
                    nameof(Figures.RegularTriangle) => new Figures.RegularTriangle(
                        float.Parse(textBoxTriangleR.Text)),
                    nameof(Figures.Square) => new Figures.Square(
                        float.Parse(textBoxSquareA.Text)),
                    _ => null
                };
            }
            catch(FormatException)
            {
                MessageBox.Show("Invalid input!");
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception [{ex.GetType().FullName}]: {ex.Message}");
                return;
            }

            if (targetFigure is null)
            {
                MessageBox.Show("Invalid combobox selection!");
                return;
            }

            (Shape s, Label l) = _shapeFactory.CreateShape(targetFigure);

            l.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            float maxWidth = MathF.Max((float)s.Width, (float)l.DesiredSize.Width);

            double currentHeight = s.Height + l.DesiredSize.Height;

            if (_lastFigureMargin.X + maxWidth > canvas.ActualWidth)
            {
                _lastFigureMargin.X = 0;
                _lastFigureMargin.Y += _prevMaxRowHeight;

                _prevMaxRowHeight = (float)currentHeight;

                double requiredHeight = currentHeight + _lastFigureMargin.Y;

                if (requiredHeight > canvas.ActualHeight)
                {
                    canvas.Height = requiredHeight;
                    scrollViewer.ScrollToEnd();
                }
            }
            else
            {
                if (currentHeight > _prevMaxRowHeight)
                    _prevMaxRowHeight = (float)currentHeight;
            }

            s.Margin = new Thickness(_lastFigureMargin.X, _lastFigureMargin.Y, 0, 0);
            l.Margin = new Thickness(_lastFigureMargin.X, _lastFigureMargin.Y + s.Height, 0, 0);

            _lastFigureMargin.X += maxWidth;

            canvas.Children.Add(s);
            canvas.Children.Add(l);
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var targetGrid = comboBox.SelectedItem switch
            {
                nameof(Figures.Circle) => circleParametersGrid,
                nameof(Figures.Ellipse) => ellipseParametersGrid,
                nameof(Figures.Rectangle) => rectangleParametersGrid,
                nameof(Figures.RegularPolygon) => polygonParametersGrid,
                nameof(Figures.RegularTriangle) => triangleParametersGrid,
                nameof(Figures.Square) => squareParametersGrid,
                _ => throw new Exception("Invalid combobox selected item")
            };

            targetGrid.Visibility = Visibility.Visible;

            if (_currentParametersGrid is { })
                _currentParametersGrid.Visibility = Visibility.Hidden;   

            _currentParametersGrid = targetGrid;
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }
    }
}