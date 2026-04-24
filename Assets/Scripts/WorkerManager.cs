using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float tickIntervalSeconds = 1f;

    private float tickTimer;
    private readonly List<Residence> residences = new();
    private readonly List<ProductionBuilding> productionBuildings = new();

    private void Update()
    {
        if (tickIntervalSeconds <= 0f)
        {
            RunTick();
            return;
        }

        tickTimer += Time.deltaTime;
        while (tickTimer >= tickIntervalSeconds)
        {
            tickTimer -= tickIntervalSeconds;
            RunTick();
        }
    }

    public void RunTick()
    {
        RefreshCollections();

        int workerPool = BuildGlobalWorkerPool();
        int totalAssignedWorkers = AssignWorkersToBuildings(workerPool);
        DistributeWorkersToResidences(totalAssignedWorkers);
    }

    private void RefreshCollections()
    {
        residences.Clear();
        residences.AddRange(FindObjectsOfType<Residence>());
        residences.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));

        productionBuildings.Clear();
        productionBuildings.AddRange(FindObjectsOfType<ProductionBuilding>());
        productionBuildings.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));
    }

    private int BuildGlobalWorkerPool()
    {
        int totalPopulation = 0;

        foreach (Residence residence in residences)
        {
            int population = Mathf.Max(0, residence.populationCapacity);
            totalPopulation += population;
            residence.assignedWorkers = 0;
        }

        return totalPopulation;
    }

    private int AssignWorkersToBuildings(int availableWorkers)
    {
        int remainingWorkerPool = availableWorkers;
        int totalAssignedWorkers = 0;

        foreach (ProductionBuilding building in productionBuildings)
        {
            int maxWorkers = Mathf.Max(0, building.maxWorkers);
            int assignedWorkers = Mathf.Min(maxWorkers, remainingWorkerPool);

            building.assignedWorkers = assignedWorkers;

            remainingWorkerPool -= assignedWorkers;
            totalAssignedWorkers += assignedWorkers;
        }

        return totalAssignedWorkers;
    }

    private void DistributeWorkersToResidences(int totalAssignedWorkers)
    {
        int remainingAssignedWorkers = totalAssignedWorkers;

        foreach (Residence residence in residences)
        {
            int population = Mathf.Max(0, residence.populationCapacity);
            int assignedWorkers = Mathf.Min(population, remainingAssignedWorkers);

            residence.assignedWorkers = assignedWorkers;
            remainingAssignedWorkers -= assignedWorkers;
        }
    }
}
