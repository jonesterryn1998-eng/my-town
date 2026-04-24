using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Min(0)]
    public int maxWorkers = 0;

    [Min(0)]
    public int assignedWorkers = 0;

    [SerializeField]
    private float incomePerWorker = 1f;

    public float CurrentIncome => assignedWorkers * incomePerWorker;
}
