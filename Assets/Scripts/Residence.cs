using UnityEngine;

/// <summary>
/// Housing; population capacity and per-tick worker display fields are maintained only by WorkerManager.
/// </summary>
public class Residence : MonoBehaviour
{
    [SerializeField] int populationCapacity;

    int assignedWorkers;

    public int PopulationCapacity => populationCapacity;
    public int AssignedWorkers => assignedWorkers;
    public int Unemployed => populationCapacity - assignedWorkers;

    internal void SetAssignedWorkersFromManager(int value) =>
        assignedWorkers = Mathf.Clamp(value, 0, populationCapacity);
}
