namespace ContainerConsoleApp;

public class ContainerForCooling : Container
{
    private string? _cargoType;
    private float _containerTemp;

    public ContainerForCooling(double containerWeightInKg, double maxCargoCapacityInKg, double heightInCm,
        double depthInCm) : base(containerWeightInKg, maxCargoCapacityInKg, heightInCm, depthInCm)
    {
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

        if (cargoProduct.minTemp > _containerTemp)
        {
            throw new WrongMinimalTemperatureException(_id, cargoProduct.minTemp);
        }

        base.LoadCargo(cargo);
    }

    protected override string initId(string append = "")
    {
        return base.initId("C-" + uid++);
    }

    public override void EmptyCargo()
    {
        _cargoType = null;
        base.EmptyCargo();
    }

    public class WrongProductTypeException(string id, string expected, string received)
        : Exception("Container: " + id + " Expected product " + expected + ", but received: " + received);

    public class WrongMinimalTemperatureException(string id, float productTemp)
        : Exception("Container: " + id + " Container temp is higher than minimal temp for the product");
}