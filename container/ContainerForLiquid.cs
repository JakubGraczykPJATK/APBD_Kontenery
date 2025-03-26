using System.Text;

namespace ContainerConsoleApp;

public class ContainerForLiquid : Container, IHazardNotifier
{
    private bool _isCargoHazardous;

    public ContainerForLiquid(double containerWeightInKg, double maxCargoCapacityInKg, double heightInCm,
        double depthInCm) : base(containerWeightInKg, maxCargoCapacityInKg, heightInCm, depthInCm)
    {
    }

    public void Log(string id, string message)
    {
        Console.WriteLine("Container: " + id + " " + message);
        Console.WriteLine("Enter anything to continue.");
        Console.ReadKey();
    }

    public override void LoadCargo(Cargo cargo)
    {
        if (typeof(CargoLiquid) != cargo.GetType())
        {
            throw new WrongCargoTypeException(_id, typeof(CargoLiquid), cargo.GetType());
        }

        CargoLiquid cargoLiquid = (CargoLiquid)cargo;
        if (_cargoWeightInKg == 0)
        {
            _isCargoHazardous = cargoLiquid.isHazardous;
        }

        if (_isCargoHazardous)
        {
            if (_cargoWeightInKg + cargoLiquid.weight > 0.5 * _maxCargoCapacityInKg)
            {
                Log(_id, "Tried to add hazardous liquid that would exceed 50% capacity rule.");
            }
            else
            {
                _cargoWeightInKg += cargoLiquid.weight;
            }
        }
        else
        {
            if (_cargoWeightInKg + cargoLiquid.weight > 0.9 * _maxCargoCapacityInKg)
            {
                Log(_id, "Tried to add non hazardous liquid that would exceed 90% capacity rule.");
            }
            else
            {
                _cargoWeightInKg += cargoLiquid.weight;
            }
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(base.ToString().Replace("}", ","));
        stringBuilder.Append(" is liquid hazardous: " + _isCargoHazardous + "}");

        return stringBuilder.ToString();
    }

    protected override string initId(string append = "")
    {
        return base.initId("L-" + uid++);
    }
}