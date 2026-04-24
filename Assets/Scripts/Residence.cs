using UnityEngine;

/// <summary>
/// Represents a residential building that houses workers.
/// Worker assignment is managed exclusively by WorkerManager.
/// </summary>
public class Residence : MonoBehaviour
{
    [Header("Population")]
    [Tooltip("Maximum number of residents this building can house.")]
    public int populationCapacity;

    /// <summary>
    /// Number of residents currently employed (set by WorkerManager).
    /// </summary>
    public int assignedWorkers { get; private set; }

    /// <summary>
    /// Number of residents who are unemployed.
    /// </summary>
    public int unemployed => populationCapacity - assignedWorkers;

    /// <summary>
    /// Called by WorkerManager to update the employment state for this residence.
    /// </summary>
    public void SetWorkers(int workers)
    {
        assignedWorkers = Mathf.Clamp(workers, 0, populationCapacity);
    }
}
