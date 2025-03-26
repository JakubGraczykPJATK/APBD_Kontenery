using System.Reflection;
using System.Runtime.CompilerServices;
using ContainerConsoleApp;

Dictionary<string, CargoShip> _cargoShips = new Dictionary<string, CargoShip>();
Dictionary<string, Container> _containers = new Dictionary<string, Container>();
ConsoleInterface consoleInterface = new ConsoleInterface();
HashSet<int> _allowedCommands = new HashSet<int>();
bool exitFlag = true;

while (exitFlag)
{
    try
    {
        Console.Clear();
        Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
        Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
        Console.WriteLine(consoleInterface.printMenu(_cargoShips.Values.ToList(), _containers.Values.ToList(),
            ref _allowedCommands));
        exitFlag = handleCommand();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine("Enter anything to go back to the menu");
        Console.ReadKey();
    }
}


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
            _cargoShips.Add(cargoShip.GetName().ToUpper(), cargoShip);
        }
            break;
        //Remove cargo ship
        case 2:
        {
            Console.Clear();
            Console.WriteLine("Enter cargo ship id, that you want to remove");
            string id = Console.In.ReadLine().Trim().Replace(".", "");
            _cargoShips.Remove(id.ToUpper());
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
                    _containers.Add(container.getId(), container);
                }
                    break;
                case "gas":
                {
                    Container container =
                        new ContainerForGas(containerWeightIn, maxCargoCapacityIn, heightIn, depthIn);
                    _containers.Add(container.getId(), container);
                }
                    break;
                case "cooling":
                {
                    Console.Clear();
                    Console.WriteLine("Enter container minimal temperature.");
                    float minTemp = float.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
                    Container container =
                        new ContainerForCooling(containerWeightIn, maxCargoCapacityIn, heightIn, depthIn, minTemp);
                    _containers.Add(container.getId(), container);
                }
                    break;
            }
        }
            break;
        //Load Container with cargo
        case 4:
        {
            Console.Clear();
            Console.WriteLine("Enter cargo type, available types:[Liquid,Gas,Product]");
            string cargoType = Console.In.ReadLine().Trim().Replace(".", "");
            Console.Clear();
            Console.WriteLine("Enter cargo weight.");
            double weightIn = double.Parse(Console.In.ReadLine().Trim().Replace(".", ""));

            Cargo cargo = null;
            switch (cargoType.ToLower())
            {
                case "liquid":
                {
                    Console.Clear();
                    Console.WriteLine("Enter if liquid is hazardous.[true,false]");
                    bool hazardousIn = bool.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
                    cargo = new CargoLiquid(weightIn, hazardousIn);
                }
                    break;
                case "gas":
                {
                    cargo = new CargoGas(weightIn);
                }
                    break;
                case "product":
                {
                    Console.Clear();
                    Console.WriteLine("Enter minimal temperature for this cargo product.");
                    float minTempIn = float.Parse(Console.In.ReadLine().Trim().Replace(".", ""));
                    Console.Clear();
                    Console.WriteLine("Enter cargo product name.");
                    string productName = Console.In.ReadLine().Trim().Replace(".", "");
                    cargo = new CargoProduct(weightIn, productName, minTempIn);
                }
                    break;
            }

            Console.Clear();
            Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
            Console.WriteLine($"Enter id of container which you want to load with the Product{{{cargo}}}");
            string containerId = Console.In.ReadLine().Trim().Replace(".", "");
            _containers[containerId.ToUpper()].LoadCargo(cargo);
        }
            break;
        //Load container into cargo ship
        case 5:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
            Console.WriteLine("Enter id of container that you want to load.");
            string containerId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine($"Enter id of ship that you want to be loaded with container {{containerId}}.");
            string shipId = Console.In.ReadLine().Trim().Replace(".", "");

            _cargoShips[shipId.ToUpper()].LoadContainer(_containers[containerId.ToUpper()]);
            _containers.Remove(containerId.ToUpper());
        }
            break;
        //Load containers into cargo ship
        case 6:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
            Console.WriteLine("Do you want to load all of these containers?[yes,no]");
            string answer = Console.In.ReadLine().Trim().Replace(".", "");
            if (answer.ToLower() == "yes")
            {
                Console.Clear();
                Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
                Console.WriteLine("Enter id of ship that you want to be loaded.");
                string shipId = Console.In.ReadLine().Trim().Replace(".", "");

                _cargoShips[shipId.ToUpper()].LoadContainers(_containers.Values.ToList());
                foreach (var con in _containers.Keys)
                {
                    _containers.Remove(con);
                }
            }
        }
            break;
        // Unload container from cargo ship
        case 7:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine("Enter id of ship from which you want to unload container.");
            string shipId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(_cargoShips[shipId.ToUpper()]);
            Console.WriteLine("Enter id of container that you want to unload.");
            string containerId = Console.In.ReadLine().Trim().Replace(".", "");

            Container containerToRemove = _cargoShips[shipId.ToUpper()].RemoveContainer(containerId.ToUpper());
            _containers.Add(containerToRemove.getId(), containerToRemove);
        }
            break;
        //Unload cargo from container
        case 8:
        {
            string answer = "no";
            if (_cargoShips.Values.Any(s => s.GetContainers().Count != 0))
            {
                Console.Clear();
                Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
                Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
                Console.WriteLine("Do you want to unload container from a ship?[yes,no]");
                answer = Console.In.ReadLine().Trim().Replace(".", "");
            }

            if (answer.ToLower() == "yes")
            {
                Console.Clear();
                Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
                Console.WriteLine("Enter id of ship from which you want to unload container cargo.");
                string shipId = Console.In.ReadLine().Trim().Replace(".", "");

                Console.Clear();
                Console.WriteLine(_cargoShips[shipId.ToUpper()]);
                Console.WriteLine("Enter id of the container that you want to unload cargo from.");
                string containerId = Console.In.ReadLine().Trim().Replace(".", "");
                _cargoShips[shipId.ToUpper()].EmptyContainer(containerId.ToUpper());
            }
            else
            {
                Console.Clear();
                Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
                Console.WriteLine("Enter id of the container that you want to unload cargo from.");
                string containerId = Console.In.ReadLine().Trim().Replace(".", "");
                _containers[containerId.ToUpper()].EmptyCargo();
            }
        }
            break;
        //Swap container on cargo ship with another container
        case 9:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine("Enter id of the ship from which you want to swap out a container.");
            string shipId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(_cargoShips[shipId.ToUpper()]);
            Console.WriteLine("Enter id of the container that you want to swap out.");
            string containerOnShip = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
            Console.WriteLine("Enter id of the container that you want to swap in.");
            string containerToSwapIn = Console.In.ReadLine().Trim().Replace(".", "");

            Container containerToAdd = _cargoShips[shipId.ToUpper()]
                .ReplaceWithContainer(containerOnShip, _containers[containerToSwapIn.ToUpper()]);

            _containers.Remove(containerToSwapIn.ToUpper());
            _containers.Add(containerToAdd.getId().ToUpper(), containerToAdd);
        }
            break;
        //Move container from one ship to another
        case 10:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine("Enter id of the ship that you want to move container from.");
            string shipFrom = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(_cargoShips[shipFrom.ToUpper()]);
            Console.WriteLine("Enter id of the container that you want to move out.");
            string containerId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine("Enter id of the ship that you want to move container to.");
            string shipTo = Console.In.ReadLine().Trim().Replace(".", "");

            _cargoShips[shipFrom.ToUpper()].MoveContainer(containerId, _cargoShips[shipTo.ToUpper()]);
        }
            break;
        //Info about cargo ship
        case 11:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printCargoShipList(_cargoShips.Values.ToList()));
            Console.WriteLine("Enter id of the ship that you want to get more info about.");
            string shipId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine("Info about the ship:(press anything to continue)");
            Console.WriteLine(_cargoShips[shipId.ToUpper()]);
            Console.ReadKey();
        }
            break;
        //Info about container
        case 12:
        {
            Console.Clear();
            Console.WriteLine(consoleInterface.printContainerList(_containers.Values.ToList()));
            Console.WriteLine("Enter id of the container that you want to get more info about.");
            string containerId = Console.In.ReadLine().Trim().Replace(".", "");

            Console.Clear();
            Console.WriteLine("Info about the container:(press anything to continue)");
            Console.WriteLine(_containers[containerId.ToUpper()]);
            Console.ReadKey();
        }
            break;
        case 13:
        {
            return false;
        }
    }

    return true;
}