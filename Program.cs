using PlankenCalculator;

var lengthRange = new[] { new WoodPrice(180, 4.5), new WoodPrice(240, 5.99), new WoodPrice(300, 7.5)};
var diameterRange = Enumerable.Range(300, 50);
var widthRange = Enumerable.Range(40, 1);

var specs = new List<Specs>();

const double plankWidth = 14.5;
const double minHeight = 70;
const double maxHeight = 120;
var specsCalculator = new SpecsCalculator(plankWidth, minHeight, maxHeight);


foreach (var length in lengthRange)
{
    foreach (var diameter in diameterRange)
    {
        foreach (var width in widthRange)
        {
            var spec = specsCalculator.CalculateSpecs(length.Length, length.Price, diameter, width);
            specs.Add(spec);
        }
    }
}

var fileName = @"C:\Users\micke\Documents\PlankenCalculatorOutput\" + $"{DateTime.Now:yyyy-MM-dd hh-mm-ss}.txt";
File.WriteAllText(fileName, $"Diameter\tLength\t\tWidth\t\tAmount\tPrice\n");

var relevantSpecs = specs
    //.GroupBy(x => x.Diameter)
    //.Select(x => x.OrderBy(y => y.TotalPrice).First())
    .OrderBy(x => x.Diameter);

foreach (var spec in relevantSpecs)
{
    File.AppendAllText(fileName, $"{spec.Diameter}cm\t\t{spec.Length}cm\t\t{spec.Width}cm\t\t{spec.Amount}\t\t{spec.TotalPrice}\n");
}




public record WoodPrice(int Length, double Price);
public record Specs(double Diameter, double Width, int Amount, double Price, double Length, double TotalPrice);

public class PlankLayout
{
    public static int Length { get; set; }
    public List<double> Planks { get; set; } = new List<double>();
    public double Remainder => Length - Planks.Sum();
}

