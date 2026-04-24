using UnityEngine;

public class Residence : MonoBehaviour
{
    [Min(0)]
    public int populationCapacity;

    [Min(0)]
    public int assignedWorkers;

    public int unemployed => populationCapacity - assignedWorkers;
}
