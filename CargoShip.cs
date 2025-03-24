namespace ContainerConsoleApp;

public class CargoShip
{
    private List<Container> _containers;
    private float _maxSpeedInKnots;
    private int _maxContainerNumber;
    private double _maxWeightInTons;

    public void loadContainer(Container container)
    {
        if (_containers.Count + 1 > _maxContainerNumber)
        {
            //TODO rzuc bledem o braku miejsce
        }

        double totalWeight = 0d;
        foreach (var con in _containers)
        {
            totalWeight += con.getTotalWeight();
        }

        if (totalWeight + container.getTotalWeight() > _maxWeightInTons)
        {
            //TODO rzuc bledem o braku miejsca z uwagi na wage
        }

        _containers.Add(container);
    }

    public void loadContainers(List<Container> containers)
    {
        if (_containers.Count + containers.Count > _maxContainerNumber)
        {
            //TODO rzuc bledem o braku miejsce
        }

        double totalWeight = 0d;
        foreach (var con in _containers)
        {
            totalWeight += con.getTotalWeight();
        }

        foreach (var con in containers)
        {
            totalWeight += con.getTotalWeight();
        }

        if (totalWeight > _maxWeightInTons)
        {
            //TODO rzuc bledem o braku miejsca z uwagi na wage
        }

        _containers.AddRange(containers);
    }

    public void removeContainer(String id)
    {
        
    }
}