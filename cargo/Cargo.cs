namespace ContainerConsoleApp;

public abstract class Cargo
{
    public double weight { get; protected set; }

    protected Cargo(double weight)
    {
        this.weight = weight;
    }

    public override string ToString()
    {
        return "weight: " + weight;
    }
}