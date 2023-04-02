namespace PlankenCalculator;

public class SpecsCalculator
{
    private readonly double _plankWidth;

    public SpecsCalculator(double plankWidth)
    {
        _plankWidth = plankWidth;
    }
    
    public Specs CalculateSpecs(int length, double price, double diameter, double width, double minHeight, double maxHeight)
    {
        PlankLayout.Length = length;
    
        var calculator = new PlankLengthCalculator(_plankWidth, minHeight, maxHeight);
        var planks = calculator.CalculateLengths(diameter)
            .Concat(calculator.CalculateLengths(diameter + width))
            .Select(x => x + 0.5)
            .OrderByDescending(x => x)
            .ToList();
    
        var layouts = new List<PlankLayout>();
        while (planks.Any())
        {
            var layout = new PlankLayout();
            layout.Planks.Add(planks[0]);
    
            var planksToBeRemoved = new List<double>();
            for (int i = 1; i < planks.Count; i++)
            {
                var plank = planks[i];
                if (plank <= layout.Remainder)
                {
                    layout.Planks.Add(plank);
                    planksToBeRemoved.Add(plank);
                }
            }
    
            foreach (var plank in planksToBeRemoved)
                planks.Remove(plank);
            
            layouts.Add(layout);
            planks.RemoveAt(0);
        }
    
        for (int i = 0; i < layouts.Count; i++)
        {
            var layout = layouts[i];
            //Console.WriteLine($"{i}: {Math.Round(layout.Remainder, 2):f2}\t + {string.Join(", ", layout.Planks)}");
        }
    
        return new Specs(diameter, width, layouts.Count, length, Math.Round(layouts.Count * price, 2), minHeight, maxHeight);
    }
}