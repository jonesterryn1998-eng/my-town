using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] private float tickIntervalSeconds = 1f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (tickIntervalSeconds <= 0f)
        {
            RunWorkerAssignmentTick();
            return;
        }

        if (timer < tickIntervalSeconds)
        {
            return;
        }

        while (timer >= tickIntervalSeconds)
        {
            timer -= tickIntervalSeconds;
            RunWorkerAssignmentTick();
        }
    }

    private void RunWorkerAssignmentTick()
    {
        Residence[] residences = FindObjectsByType<Residence>(FindObjectsSortMode.None)
            .OrderBy(r => r.gameObject.GetInstanceID())
            .ToArray();
        ProductionBuilding[] productionBuildings = FindObjectsByType<ProductionBuilding>(FindObjectsSortMode.None)
            .OrderBy(b => b.gameObject.GetInstanceID())
            .ToArray();

        int totalWorkerPool = BuildGlobalWorkerPool(residences);
        int totalAssignedWorkers = AssignWorkersToBuildings(productionBuildings, totalWorkerPool);
        DistributeWorkersBackToResidences(residences, totalAssignedWorkers);
    }

    private static int BuildGlobalWorkerPool(IReadOnlyList<Residence> residences)
    {
        int totalWorkerPool = 0;

        for (int i = 0; i < residences.Count; i++)
        {
            totalWorkerPool += Mathf.Max(0, residences[i].populationCapacity);
        }

        return totalWorkerPool;
    }

    private static int AssignWorkersToBuildings(IReadOnlyList<ProductionBuilding> productionBuildings, int totalWorkerPool)
    {
        int remainingWorkerPool = Mathf.Max(0, totalWorkerPool);
        int totalAssignedWorkers = 0;

        for (int i = 0; i < productionBuildings.Count; i++)
        {
            ProductionBuilding building = productionBuildings[i];
            int workersForBuilding = Mathf.Min(Mathf.Max(0, building.maxWorkers), remainingWorkerPool);

            building.assignedWorkers = workersForBuilding;
            totalAssignedWorkers += workersForBuilding;
            remainingWorkerPool -= workersForBuilding;
        }

        return totalAssignedWorkers;
    }

    private static void DistributeWorkersBackToResidences(IReadOnlyList<Residence> residences, int totalAssignedWorkers)
    {
        int remainingAssignedWorkers = Mathf.Max(0, totalAssignedWorkers);

        for (int i = 0; i < residences.Count; i++)
        {
            Residence residence = residences[i];
            int residencePopulation = Mathf.Max(0, residence.populationCapacity);
            int workersForResidence = Mathf.Min(residencePopulation, remainingAssignedWorkers);

            residence.assignedWorkers = workersForResidence;
            remainingAssignedWorkers -= workersForResidence;
        }
    }
}
