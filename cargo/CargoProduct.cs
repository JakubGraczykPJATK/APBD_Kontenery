namespace ContainerConsoleApp;

public class CargoProduct : Cargo
{
    public string name;
    public float minTemp;

    public CargoProduct(double weight, string name, float minTemp) : base(weight)
    {
        this.name = name;
        this.minTemp = minTemp;
    }

    public override string ToString()
    {
        return base.ToString() + " minTemp: " + minTemp + " product type: " + name;
    }
}