using ContainerConsoleApp;

Dictionary<string, CargoShip> _cargoShips = new Dictionary<string, CargoShip>();
List<Container> _containers = new List<Container>();
ConsoleInterface consoleInterface = new ConsoleInterface();
HashSet<int> _allowedCommands = new HashSet<int>();

do
{
    Console.Clear();
    Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
    Console.WriteLine(consoleInterface.printContainerList(_containers));
    Console.WriteLine(consoleInterface.printMenu(_cargoShips.Values.ToList(), _containers, ref _allowedCommands));
} while (handleCommand());


bool handleCommand()
{
    int? option = null;
    while (option == null)
    {
        option = int.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
    }

    if (!_allowedCommands.Contains(option.Value))
    {
        return true;
    }

    switch (option)
    {
        //Create cargo ship
        case 1:
        {
            Console.Clear();
            Console.WriteLine("Enter cargo ship max speed in knots");
            float maxSpeedIn = float.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            Console.Clear();
            Console.WriteLine("Enter cargo ship max number of containers");
            int maxContainersIn = int.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            Console.Clear();
            Console.WriteLine("Enter cargoship max weight in tons");
            double maxWeightInTonsIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            CargoShip cargoShip = new CargoShip(maxSpeedIn, maxContainersIn, maxWeightInTonsIn);
            _cargoShips.Add(cargoShip.getName(), cargoShip);
        }
            break;
        //Remove cargo ship
        case 2:
        {
            Console.Clear();
            Console.WriteLine("Enter cargo ship id, that you want to remove");
            string id = Console.In.ReadLine().Trim().Replace(".", "");
            _cargoShips.Remove(id);
        }
            break;
        //Create Container
        case 3:
        {
            Console.Clear();
            Console.WriteLine("Enter container type, available types:[Liquid,Gas,Cooling]");
            string containerType = Console.In.ReadLine().Trim().Replace(".", "");
            Console.Clear();
            Console.WriteLine("Enter container weight in Kg");
            double containerWeightIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            Console.Clear();
            Console.WriteLine("Enter container max cargo capacity in Kg");
            double maxCargoCapacityIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            Console.Clear();
            Console.WriteLine("Enter container height in Cm");
            double heightIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            Console.Clear();
            Console.WriteLine("Enter container depth in Cm");
            double depthIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
            switch (containerType.ToLower())
            {
                case "liquid":
                {
                    Container container =
                        new ContainerForLiquid(containerWeightIn, maxCargoCapacityIn, heightIn, depthIn);
                    _containers.Add(container);
                }
                    break;
                case "gas":
                {
                    Container container =
                        new ContainerForGas(containerWeightIn, maxCargoCapacityIn, heightIn, depthIn);
                    _containers.Add(container);
                }
                    break;
                case "cooling":
                {
                    Container container =
                        new ContainerForCooling(containerWeightIn, maxCargoCapacityIn, heightIn, depthIn);
                    _containers.Add(container);
                }
                    break;
            }
        }
            break;
        //Load Container with cargo
        case 4:
        {
        }
            break;
        case 5:
        {
        }
            break;
        case 6:
        {
        }
            break;
        case 7:
        {
        }
            break;
        case 8:
        {
        }
            break;
        case 9:
        {
        }
            break;
        case 10:
        {
        }
            break;
        case 11:
        {
        }
            break;
        case 12:
        {
        }
            break;
        case 13:
        {
            return false;
        }
            break;
    }

    return true;
}