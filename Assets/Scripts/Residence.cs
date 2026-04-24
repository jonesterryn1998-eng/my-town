using UnityEngine;

public class Residence : MonoBehaviour
{
    [Min(0)]
    public int populationCapacity = 0;

    [Min(0)]
    public int assignedWorkers = 0;

    public int unemployed => Mathf.Max(0, populationCapacity - assignedWorkers);
}
