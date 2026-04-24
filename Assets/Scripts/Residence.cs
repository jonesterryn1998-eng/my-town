using UnityEngine;

public class Residence : MonoBehaviour
{
    [Header("Population")]
    [SerializeField] private int populationCapacity = 7;

    [HideInInspector] public int assignedWorkers;

    public int PopulationCapacity => populationCapacity;

    public int Unemployed => populationCapacity - assignedWorkers;
}
