using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Min(0)]
    public int maxWorkers = 10;

    [Min(0)]
    public int assignedWorkers;

    [SerializeField] private int incomePerWorker = 1;

    // Existing income flow remains based on assigned workers.
    public int GetIncomePerTick()
    {
        return assignedWorkers * incomePerWorker;
    }
}
