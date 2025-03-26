namespace ContainerConsoleApp;

public class CargoLiquid : Cargo
{
    public bool isHazardous;

    public CargoLiquid(double weight, bool isHazardous) : base(weight)
    {
        this.isHazardous = isHazardous;
    }
    public override string ToString()
    {
        return base.ToString() + " isHazardous: " + isHazardous;
    }
}