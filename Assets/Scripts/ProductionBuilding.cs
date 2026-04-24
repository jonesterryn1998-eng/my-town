using UnityEngine;

/// <summary>
/// Represents a building that produces income based on the number of assigned workers.
/// Worker assignment is managed exclusively by WorkerManager — this class never scans
/// residences or assigns workers itself.
/// </summary>
public class ProductionBuilding : MonoBehaviour
{
    [Header("Workers")]
    [Tooltip("Maximum number of workers this building can employ.")]
    public int maxWorkers;

    /// <summary>
    /// Number of workers currently assigned to this building (set by WorkerManager).
    /// </summary>
    public int assignedWorkers { get; private set; }

    [Header("Income")]
    [Tooltip("Income generated per worker per tick.")]
    public float incomePerWorker = 1f;

    /// <summary>
    /// Called by WorkerManager to update the worker count for this building.
    /// </summary>
    public void SetAssignedWorkers(int workers)
    {
        assignedWorkers = Mathf.Clamp(workers, 0, maxWorkers);
    }

    /// <summary>
    /// Returns the income produced this tick based on currently assigned workers.
    /// </summary>
    public float CalculateIncome()
    {
        return assignedWorkers * incomePerWorker;
    }
}
