using PlankenCalculator;

//var lengthRange = new[] { new WoodPrice(180, 4.5), new WoodPrice(240, 5.99), new WoodPrice(300, 7.5)};
var lengthRange = new[] { new WoodPrice(180, 7.09), new WoodPrice(240, 8.38), new WoodPrice(300, 11.56)};
var diameterRange = new []{350};
var widthRange = new[] { 35 };
var minRange = new []{60};
var maxRange = new []{100};

var specs = new List<Specs>();

const double plankWidth = 14.5;
var specsCalculator = new SpecsCalculator(plankWidth);

foreach (var min in minRange)
{
    foreach (var max in maxRange)
    {
        foreach (var length in lengthRange)
        {
            foreach (var diameter in diameterRange)
            {
                foreach (var width in widthRange)
                {
                    var spec = specsCalculator.CalculateSpecs(length.Length, length.Price, diameter, width, min, max);
                    specs.Add(spec);
                }
            }
        }
    }
}


var fileName = @"C:\Users\micke\Documents\PlankenCalculatorOutput\" + $"{DateTime.Now:yyyy-MM-dd hh-mm-ss}.txt";
File.WriteAllText(fileName, $"Diameter\tWidth\t\tRange\t\t\tAmount\tLength\t\tPrice\n");

var relevantSpecs = specs
    // .GroupBy(x => x.Diameter)
    // .Select(x => x.OrderBy(y => y.TotalPrice).First())
    .OrderBy(x => x.TotalPrice);

foreach (var spec in relevantSpecs)
{
    File.AppendAllText(fileName, $"{spec.Diameter}cm\t\t{spec.Width}cm\t\t{spec.MinHeight} => {spec.MaxHeight}cm\t\t{spec.Amount}\t\t{spec.Length}cm\t\t{spec.TotalPrice}\n");
}




public record WoodPrice(int Length, double Price);
public record Specs(double Diameter, double Width, int Amount, double Length, double TotalPrice, double MinHeight, double MaxHeight);

public class PlankLayout
{
    public static int Length { get; set; }
    public List<double> Planks { get; set; } = new List<double>();
    public double Remainder => Length - Planks.Sum();
}

