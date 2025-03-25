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
            if (_containerWeightInKg + cargoLiquid.weight > 0.5 * _maxCargoCapacityInKg)
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
            if (_containerWeightInKg + cargoLiquid.weight > 0.9 * _maxCargoCapacityInKg)
            {
                Log(_id, "Tried to add non hazardous liquid that would exceed 90% capacity rule.");
            }
            else
            {
                _cargoWeightInKg += cargoLiquid.weight;
            }
        }
    }

    protected override string initId(string append = "")
    {
        return base.initId("L-" + uid++);
    }
}