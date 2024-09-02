# wpf-tflex-test-task
Выполнение тестового задания на вакансию "Fullstack C#/.NET разработчик" компании ЗАО Топ Системы
# Общее описание
Программа демонстрирует навыки ООП, соблюдение принципов SOLID, паттерна проектирования MVVM, а также паттернов Matching и Factory.

Конечно, можно было бы использовать встроенные типы System.Windows.Shapes.Shape, однако для демонстрации навыков ООП классы фигур были реализованы заново. Кроме того, классы содержат и другую информацию, которую не получится хранить в System.Windows.Shapes.Shape.  
# Описание геометрических фигур
Классы пространства имён Figures были реализованы так, чтобы их можно было использовать в отрыве от фреймворка WPF. Данные классы содержат в себе исключительно математическое описание данных фигур.
## Структура классов Figures
Структура классов выглядит следующим образом:

* Point - точка - пара координат X, Y
* Figure - абстрактный класс описания фигур
  * Ellipse - эллипс
    * Circle - круг - частный случай эллипса
  * Rectangle - прямоугольник
    * Square - квадрат - частный случай прямоугольника
  * RegularPolygon - правильный многоугольник
    * RegularTriangle - правильный треугольник - частный случай правильного многоугольника

Классы были спроектированы с использованием принципов ООП:
* Наследование - трехуровневая иерархия наследования классов
* Инкапсуляция - требуемые реализации абстрактного класса Figure сокрыты, а параметры фигур доступны только для чтения
* Абстракция - класс Figure
* Полиморфизм - методы, вызываемые в конструкторе абстрактного классе Figure, переопределяются потомками. В дальнейшем работа идёт с абстрактным классом Figure, например в фабричном классе ShapeFactory. Также это демонстрирует соблюдение одного из принципов SOLID - Liskov substitution.

## Описание класса Figure
Абстрактный класс описывает любую фигуру, которая должна иметь:
* Периметр
* Площадь
* Тип фигуры

Эти методы являются абстрактными, и классы-потомки реализуют их самостоятельно.

```csharp
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
```
Также присутствует метод CalcSpecific. Он нужен для дополнительных расчетов, которые могут понадобиться особым геометрическим фигурам. Также он может использоваться для валидации параметров фигуры. Например, класс RegularPolygon требует рассчёта координат вершин многоугольника:
```csharp
protected override void CalcSpecific()
{
    if (R <= 0)
        throw new ArgumentException("Argument R must be positive!");
    else if (N < 3)
        throw new ArgumentException("Argument N must be not less than 3! Polygon with 2 or less points does not exist");

    Points = CalcPoints();
}

private Point[] CalcPoints()
{
    Point[] points = new Point[N];

    for (int i = 0; i < N; i++)
        points[i] = new Point(
            R + R * MathF.Cos(2 * MathF.PI * i / N),
            R + R * MathF.Sin(2 * MathF.PI * i / N));

    return points;
}
```
# Интеграция с WPF
Для интеграции с WPF был написан фабричный класс ShapeFactory. Он преобразует классы пространства имён Figures в элементы System.Windows.Shapes.Shape для их дальнейшего отображения на экране
## ShapeFactory
Класс определяется следующим образом:
```csharp
public (Shape, Label) CreateShape(Figure figure) { ... }
```
Он возвращает пару элементов Shape и Label. Shape - фигура для отображения, Label - информация о фигуре (периметр, площадь, тип фигуры).

Также демонстрируется паттерн Matching для соотношения классов Figures и System.Windows.Shapes.Shape:
```csharp
Shape shape = figure.FigureType switch
{
    nameof(Figures.Rectangle) or
    nameof(Figures.Square) => new System.Windows.Shapes.Rectangle()
    {...},
    nameof(Figures.Ellipse) or
    nameof(Figures.Circle) => new System.Windows.Shapes.Ellipse()
    {...},
    nameof(Figures.RegularPolygon) or
    nameof(Figures.RegularTriangle) => new System.Windows.Shapes.Polygon()
    {...},
    _ => throw new NotImplementedException($"ShapeFactory.CreateChape: Passed figure type [{figure.FigureType}] not implemented")
};
```

Наличие этого класса демонстрирует другой принцип SOLID - single responsibility.
# Поведение программы
При запуске окно программы выглядит так:

![MainWindow](_images/Screenshot%202024-09-02%20183719.png)

Программа позволяет рисовать фигуры на холсте. Возможно выбрать тип фигуры, ее размеры, и другие параметры. Также в программе реализован ангоритм переноса фигур на следующую строку, чтобы можно было добвить сколько угодно фигур на холст:

![MainWindow](_images/Screenshot%202024-09-02%20183714.png)

Также проверяется валидация введенных данных. Например, если была введена строка, которую невозможно интерпретировать как число, выскакивает предупреждение, и фигура не добавляется на холст

![MainWindow](_images/Screenshot%202024-09-02%20184344.png)

При нажатии кнопки Clear холст очищается от нарисованных ранее фигур.

