using System.Text;

namespace ContainerConsoleApp;

public class ContainerForCooling : Container
{
    private string? _cargoType;
    private float _containerTemp;

    public ContainerForCooling(double containerWeightInKg, double maxCargoCapacityInKg, double heightInCm,
        double depthInCm, float minTemp) : base(containerWeightInKg, maxCargoCapacityInKg, heightInCm, depthInCm)
    {
        _containerTemp = minTemp;
    }

    public override void LoadCargo(Cargo cargo)
    {
        if (typeof(CargoProduct) != cargo.GetType())
        {
            throw new WrongCargoTypeException(_id, typeof(CargoProduct), cargo.GetType());
        }

        CargoProduct cargoProduct = (CargoProduct)cargo;
        if (_cargoType != null && cargoProduct.name != _cargoType)
        {
            throw new WrongProductTypeException(_id, _cargoType, cargoProduct.name);
        }

        if (cargoProduct.minTemp < _containerTemp)
        {
            throw new WrongMinimalTemperatureException(_id, cargoProduct.minTemp);
        }

        _cargoType = cargoProduct.name;
        base.LoadCargo(cargo);
    }

    protected override string initId(string append = "")
    {
        return base.initId("C-" + uid++);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(base.ToString().Replace("}", ","));
        stringBuilder.Append(" product type: " + _cargoType + ",");
        stringBuilder.Append(" minimal temp: " + _containerTemp + "}");

        return stringBuilder.ToString();
    }

    public override void EmptyCargo()
    {
        _cargoType = null;
        base.EmptyCargo();
    }
}