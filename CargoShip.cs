using System.Text;

namespace ContainerConsoleApp;

public class CargoShip
{
    public CargoShip(float maxSpeedInKnots, int maxContainerNumber, double maxWeightInTons)
    {
        _maxSpeedInKnots = maxSpeedInKnots;
        _maxContainerNumber = maxContainerNumber;
        _maxWeightInTons = maxWeightInTons;
        _name = "Ship_" + uid++;
        _containers = new Dictionary<string, Container>();
    }

    private static int uid;
    private Dictionary<string, Container> _containers;
    private string _name;
    private float _maxSpeedInKnots;
    private int _maxContainerNumber;
    private double _maxWeightInTons;

    public void loadContainer(Container container)
    {
        if (_containers.Count + 1 > _maxContainerNumber)
        {
            //TODO rzuc bledem o braku miejsce
        }

        double totalWeightInKg = 0d;
        foreach (var con in _containers.Values)
        {
            totalWeightInKg += con.getTotalWeight();
        }

        if (totalWeightInKg + container.getTotalWeight() > _maxWeightInTons * 1000)
        {
            //TODO rzuc bledem o braku miejsca z uwagi na wage
        }

        _containers.Add(container.getId(), container);
    }

    public void loadContainers(List<Container> containers)
    {
        if (_containers.Count + containers.Count > _maxContainerNumber)
        {
            //TODO rzuc bledem o braku miejsce
        }

        double totalWeightInKg = 0d;
        foreach (var con in _containers.Values)
        {
            totalWeightInKg += con.getTotalWeight();
        }

        foreach (var con in containers)
        {
            totalWeightInKg += con.getTotalWeight();
        }

        if (totalWeightInKg > _maxWeightInTons * 1000)
        {
            //TODO rzuc bledem o braku miejsca z uwagi na wage
        }

        foreach (var con in _containers.Values)
        {
            _containers.Add(con.getId(), con);
        }
    }

    public void removeContainer(string id)
    {
        _containers.Remove(id);
    }

    public void emptyContainer(string id)
    {
        _containers[id].EmptyCargo();
    }

    public void replaceWithContainer(string id, Container con)
    {
        _containers.Remove(id);
        _containers.Add(con.getId(), con);
    }

    public void moveContainer(string id, CargoShip targetCon)
    {
        Container containerToMove = _containers[id];
        _containers.Remove(id);
        targetCon.loadContainer(containerToMove);
    }

    public string? containerInfo(string id)
    {
        return _containers[id].ToString();
    }

    public List<Container> getContainers()
    {
        return _containers.Values.ToList();
    }

    public string getName()
    {
        return _name;
    }

    public override string? ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_name);
        stringBuilder.Append("\t{speed:" + _maxSpeedInKnots + ",");
        stringBuilder.Append(" maxContainer:" + _maxContainerNumber + ",");
        stringBuilder.Append(" maxWeight:" + _maxWeightInTons + "}");
        if (_containers.Count != 0)
        {
            stringBuilder.Append(_containers);
        }

        return stringBuilder.ToString();
    }
}