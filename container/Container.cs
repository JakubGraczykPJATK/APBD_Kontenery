namespace ContainerConsoleApp;

public abstract class Container
{
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

    protected virtual void initId(String append = "")
    {
        _id = "KON-" + append;
    }
}