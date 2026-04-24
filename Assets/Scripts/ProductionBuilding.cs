using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Header("Workers")]
    [SerializeField] private int maxWorkers = 10;

    [Header("Income")]
    [SerializeField] private float incomePerWorker = 5f;

    [HideInInspector] public int assignedWorkers;

    public int MaxWorkers => maxWorkers;

    public float Income => assignedWorkers * incomePerWorker;
}
