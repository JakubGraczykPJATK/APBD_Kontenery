namespace ContainerConsoleApp;

public class ContainerForGas : Container, IHazardNotifier
{
    public void Log(string id, string message)
    {
        Console.WriteLine("Container: " + id + " " + message);
    }

    public override void LoadCargo(Cargo cargo)
    {
        if (typeof(CargoGas) != cargo.GetType())
        {
            throw new WrongCargoTypeException(_id, typeof(CargoGas), cargo.GetType());
        }

        base.LoadCargo(cargo);
    }

    public override void EmptyCargo()
    {
        _cargoWeightInKg *= 0.05;
    }
}