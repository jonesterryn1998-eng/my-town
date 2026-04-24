using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single authority for worker assignment each tick: global pool, buildings, then residence split.
/// </summary>
public class WorkerManager : MonoBehaviour
{
    void Update() => RunWorkerAssignment();

    public void RunWorkerAssignment()
    {
        var residences = CollectResidences();
        var buildings = CollectProductionBuildings();

        int workerPool = 0;
        for (int i = 0; i < residences.Count; i++)
            workerPool += residences[i].PopulationCapacity;

        for (int i = 0; i < buildings.Count; i++)
        {
            int cap = buildings[i].MaxWorkers;
            int assign = Mathf.Min(cap, workerPool);
            buildings[i].SetAssignedWorkersFromManager(assign);
            workerPool -= assign;
        }

        int remainingEmployed = 0;
        for (int i = 0; i < buildings.Count; i++)
            remainingEmployed += buildings[i].AssignedWorkers;

        for (int i = 0; i < residences.Count; i++)
        {
            int cap = residences[i].PopulationCapacity;
            int give = Mathf.Min(cap, remainingEmployed);
            residences[i].SetAssignedWorkersFromManager(give);
            remainingEmployed -= give;
        }
    }

    static List<Residence> CollectResidences()
    {
        var found = FindObjectsOfType<Residence>(false);
        var list = new List<Residence>(found);
        list.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));
        return list;
    }

    static List<ProductionBuilding> CollectProductionBuildings()
    {
        var found = FindObjectsOfType<ProductionBuilding>(false);
        var list = new List<ProductionBuilding>(found);
        list.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));
        return list;
    }
}
