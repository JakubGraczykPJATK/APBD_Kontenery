namespace ContainerConsoleApp;

public class OverfillException(string id, double weight)
    : Exception("Container: " + id + " Unable to load " + weight + " of cargo.");

public class WrongCargoTypeException(string id, Type expected, Type received)
    : Exception("Container: " + id + " Expected cargo type " + expected + ", but received: " + received);