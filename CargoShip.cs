using System.Text;

namespace ContainerConsoleApp;

public class CargoShip
{
    public CargoShip(float maxSpeedInKnots, int maxContainerNumber, double maxWeightInTons)
    {
        _maxSpeedInKnots = maxSpeedInKnots;
        _maxContainerNumber = maxContainerNumber;
        _maxWeightInTons = maxWeightInTons;
        _name = "SHIP-" + _uid++;
        _containers = new Dictionary<string, Container>();
    }

    private static int _uid;
    private readonly Dictionary<string, Container> _containers;
    private readonly string _name;
    private readonly float _maxSpeedInKnots;
    private readonly int _maxContainerNumber;
    private readonly double _maxWeightInTons;

    public void LoadContainer(Container container)
    {
        if (_containers.Count + 1 > _maxContainerNumber)
        {
            throw new ShipOverfillException(_name, _maxContainerNumber);
        }

        double totalWeightInKg = 0d;
        foreach (var con in _containers.Values)
        {
            totalWeightInKg += con.getTotalWeight();
        }

        if (totalWeightInKg + container.getTotalWeight() > _maxWeightInTons * 1000)
        {
            throw new ShipCapacityException(_name, _maxWeightInTons);
        }

        _containers.Add(container.getId(), container);
    }

    public void LoadContainers(List<Container> containers)
    {
        if (_containers.Count + containers.Count > _maxContainerNumber)
        {
            throw new ShipOverfillException(_name, _maxContainerNumber);
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
            throw new ShipCapacityException(_name, _maxWeightInTons);
        }

        foreach (var con in containers)
        {
            _containers.Add(con.getId(), con);
        }
    }

    public Container RemoveContainer(string id)
    {
        Container temp = _containers[id];
        _containers.Remove(id);
        return temp;
    }

    public void EmptyContainer(string id)
    {
        _containers[id].EmptyCargo();
    }

    public Container ReplaceWithContainer(string swapOut, Container swapIn)
    {
        Container con = _containers[swapOut.ToUpper()];
        _containers.Remove(swapOut.ToUpper());
        _containers.Remove(swapOut.ToUpper());
        _containers.Add(con.getId(), swapIn);

        return con;
    }

    public void MoveContainer(string id, CargoShip targetCon)
    {
        Container containerToMove = _containers[id.ToUpper()];
        targetCon.LoadContainer(containerToMove);
        _containers.Remove(id.ToUpper());
    }

    public string? ContainerInfo(string id)
    {
        return _containers[id].ToString();
    }

    public List<Container> GetContainers()
    {
        return _containers.Values.ToList();
    }

    public string GetName()
    {
        return _name;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_name);
        stringBuilder.Append("\t{speed:" + _maxSpeedInKnots + ",");
        stringBuilder.Append(" maxContainer:" + _maxContainerNumber + ",");
        stringBuilder.Append(" maxWeight:" + _maxWeightInTons + "}");
        foreach (var con in _containers.Values)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append("\t" + con);
        }


        return stringBuilder.ToString();
    }
}