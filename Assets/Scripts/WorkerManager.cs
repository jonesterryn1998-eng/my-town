using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager Instance { get; private set; }

    private Residence[] residences;
    private ProductionBuilding[] productionBuildings;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        AssignWorkers();
    }

    private void AssignWorkers()
    {
        residences = FindObjectsByType<Residence>(FindObjectsSortMode.InstanceID);
        productionBuildings = FindObjectsByType<ProductionBuilding>(FindObjectsSortMode.InstanceID);

        int totalPopulation = 0;
        for (int i = 0; i < residences.Length; i++)
            totalPopulation += residences[i].PopulationCapacity;

        int remainingPool = totalPopulation;

        for (int i = 0; i < productionBuildings.Length; i++)
        {
            int workers = Mathf.Min(productionBuildings[i].MaxWorkers, remainingPool);
            productionBuildings[i].assignedWorkers = workers;
            remainingPool -= workers;
        }

        int totalAssigned = totalPopulation - remainingPool;

        for (int i = 0; i < residences.Length; i++)
        {
            int share = Mathf.Min(residences[i].PopulationCapacity, totalAssigned);
            residences[i].assignedWorkers = share;
            totalAssigned -= share;
        }
    }
}
