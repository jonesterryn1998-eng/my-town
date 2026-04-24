using UnityEngine;

/// <summary>
/// Production site; assignedWorkers is written only by WorkerManager each tick.
/// </summary>
public class ProductionBuilding : MonoBehaviour
{
    [SerializeField] int maxWorkers;

    int assignedWorkers;

    public int MaxWorkers => maxWorkers;
    public int AssignedWorkers => assignedWorkers;

    internal void SetAssignedWorkersFromManager(int value) =>
        assignedWorkers = Mathf.Clamp(value, 0, maxWorkers);
}
