using System.Text;

namespace ContainerConsoleApp;

public abstract class Container
{
    protected Container(double containerWeightInKg, double maxCargoCapacityInKg, double heightInCm, double depthInCm)
    {
        _containerWeightInKg = containerWeightInKg;
        _maxCargoCapacityInKg = maxCargoCapacityInKg;
        _heightInCm = heightInCm;
        _depthInCm = depthInCm;
        _id = initId();
        _cargoWeightInKg = 0d;
    }

    protected static int uid;
    protected string _id;
    protected double _containerWeightInKg;
    protected double _cargoWeightInKg;
    protected double _maxCargoCapacityInKg;
    protected double _heightInCm;
    protected double _depthInCm;

    public virtual void EmptyCargo()
    {
        _cargoWeightInKg = 0;
    }

    public virtual void LoadCargo(Cargo cargo)
    {
        if (_cargoWeightInKg + cargo.weight > _maxCargoCapacityInKg)
        {
            throw new OverfillException(_id, cargo.weight);
        }

        _cargoWeightInKg += cargo.weight;
    }

    public double getTotalWeight()
    {
        return _containerWeightInKg + _cargoWeightInKg;
    }

    public double getCargoWeight()
    {
        return _cargoWeightInKg;
    }

    public string getId()
    {
        return _id;
    }

    protected virtual string initId(String append = "")
    {
        return "KON-" + append;
    }

    public override string? ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_id);
        stringBuilder.Append("\t{container weight:" + _containerWeightInKg + ",");
        stringBuilder.Append(" cargo weight:" + _cargoWeightInKg + ",");
        stringBuilder.Append(" max cargo capacity:" + _maxCargoCapacityInKg + ",");
        stringBuilder.Append(" container height:" + _heightInCm + ",");
        stringBuilder.Append(" container depth:" + _depthInCm + "}");

        return stringBuilder.ToString();
    }
}