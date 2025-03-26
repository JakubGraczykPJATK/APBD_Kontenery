using System.Text;

namespace ContainerConsoleApp;

public class ConsoleInterface
{
    public string printCargoShipList(List<CargoShip> ships)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("List of Cargo ships:");
        foreach (var cargoShip in ships)
        {
            stringBuilder.AppendLine(cargoShip.ToString());
        }

        return stringBuilder.ToString();
    }

    public string printContainerList(List<Container> containers)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("List of Containers:");
        foreach (var container in containers)
        {
            stringBuilder.AppendLine(container.ToString());
        }

        return stringBuilder.ToString();
    }

    public string printMenu(List<CargoShip> ships, List<Container> containers, ref HashSet<int> allowedCommands)
    {
        allowedCommands.Clear();
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Possible actions:");
        stringBuilder.AppendLine("1. Create cargo ship");
        allowedCommands.Add(1);
        if (ships.Count != 0)
        {
            stringBuilder.AppendLine("2. Remove cargo ship");
            allowedCommands.Add(2);
        }

        stringBuilder.AppendLine("3. Create Container");
        allowedCommands.Add(3);
        if (containers.Count != 0)
        {
            stringBuilder.AppendLine("4. Load Container with cargo");
            allowedCommands.Add(4);
        }

        if (ships.Count != 0 && containers.Count != 0)
        {
            stringBuilder.AppendLine("5. Load container into cargo ship");
            allowedCommands.Add(5);
            if (containers.Count >= 2)
            {
                stringBuilder.AppendLine("6. Load containers into cargo ship");
                allowedCommands.Add(6);
            }

            if (ships.Any(s => s.GetContainers().Count != 0))
            {
                stringBuilder.AppendLine("7. Unload container from cargo ship");
                allowedCommands.Add(7);
            }
        }

        if (containers.Count != 0 && containers.Any(c => c.getCargoWeight() != 0d)
            || ships.Any(s => s.GetContainers().Count != 0)
            && ships.Any(s => s.GetContainers().Any(c => c.getCargoWeight() != 0d)))
        {
            stringBuilder.AppendLine("8. Unload cargo from container");
            allowedCommands.Add(8);
        }

        if (ships.Any(s => s.GetContainers().Count != 0)
            && containers.Count != 0)
        {
            stringBuilder.AppendLine("9. Swap container on cargo ship with another container");
            allowedCommands.Add(9);
        }

        if (ships.Count >= 2
            && ships.Any(s => s.GetContainers().Count != 0))
        {
            stringBuilder.AppendLine("10. Move container from one ship to another");
            allowedCommands.Add(10);
        }

        if (ships.Count != 0)
        {
            stringBuilder.AppendLine("11. Info about cargo ship");
            allowedCommands.Add(11);
        }

        if (containers.Count != 0)
        {
            stringBuilder.AppendLine("12. Info about container");
            allowedCommands.Add(12);
        }

        stringBuilder.AppendLine("13. Exit application");
        allowedCommands.Add(13);
        return stringBuilder.ToString();
    }
}