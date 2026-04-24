using UnityEngine;

/// <summary>
/// Centralized system that runs once per tick and distributes workers from a
/// global pool to all ProductionBuildings and then back to all Residences.
///
/// Algorithm:
///   1. Build a global worker pool from total populationCapacity across all residences.
///   2. Assign workers to each ProductionBuilding (up to its maxWorkers) in order.
///   3. Distribute assigned workers back across residences proportionally.
///
/// No other class should assign workers. ProductionBuilding and Residence are
/// passive data holders; WorkerManager is the sole authority.
/// </summary>
public class WorkerManager : MonoBehaviour
{
    [Tooltip("Seconds between worker-assignment ticks. Set to 0 to update every frame.")]
    public float tickInterval = 1f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (tickInterval <= 0f || _timer >= tickInterval)
        {
            _timer = 0f;
            Tick();
        }
    }

    /// <summary>
    /// Runs a full worker-assignment cycle.
    /// </summary>
    public void Tick()
    {
        Residence[] residences = Object.FindObjectsByType<Residence>(FindObjectsSortMode.None);
        ProductionBuilding[] buildings = Object.FindObjectsByType<ProductionBuilding>(FindObjectsSortMode.None);

        // ------------------------------------------------------------------
        // STEP 1: Build global worker pool from all residences
        // ------------------------------------------------------------------
        int totalWorkerPool = 0;
        foreach (Residence r in residences)
        {
            totalWorkerPool += r.populationCapacity;
        }

        // ------------------------------------------------------------------
        // STEP 2: Assign workers to each ProductionBuilding
        // ------------------------------------------------------------------
        int remainingPool = totalWorkerPool;
        int totalAssigned = 0;

        foreach (ProductionBuilding building in buildings)
        {
            int workers = Mathf.Min(building.maxWorkers, remainingPool);
            building.SetAssignedWorkers(workers);
            remainingPool -= workers;
            totalAssigned += workers;
        }

        // ------------------------------------------------------------------
        // STEP 3: Distribute assigned workers back across residences
        // ------------------------------------------------------------------
        // Each residence receives workers proportional to its capacity so that
        // the distribution is deterministic and covers ALL residences correctly.
        int remainingToDistribute = totalAssigned;

        foreach (Residence r in residences)
        {
            int workers = Mathf.Min(r.populationCapacity, remainingToDistribute);
            r.SetWorkers(workers);
            remainingToDistribute -= workers;
        }
    }
}
