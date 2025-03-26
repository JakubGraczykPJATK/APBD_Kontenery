namespace ContainerConsoleApp;

public class OverfillException(string id, double weight)
    : Exception("Container: " + id + " Unable to load " + weight + "kgs of cargo.");

public class WrongCargoTypeException(string id, Type expected, Type received)
    : Exception("Container: " + id + " Expected cargo type " + expected + ", but received: " + received);

public class WrongProductTypeException(string id, string expected, string received)
    : Exception("Container: " + id + " Expected product " + expected + ", but received: " + received);

public class WrongMinimalTemperatureException(string id, float productTemp)
    : Exception("Container: " + id + " Container temp is higher than minimal temp for the product");

public class ShipOverfillException(string shipId, int maxNumber) :
    Exception("Ship: " + shipId + " can handle maximally " + maxNumber + " of containers.");

public class ShipCapacityException(string shipId, double maxWeight) :
    Exception("Ship: " + shipId + " can handle maximally " + maxWeight + " tons of weight.");