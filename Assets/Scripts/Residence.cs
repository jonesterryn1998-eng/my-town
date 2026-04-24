using UnityEngine;

namespace MyTown
{
    /// <summary>
    /// A residential building that provides population which can be employed by
    /// <see cref="ProductionBuilding"/>s.
    ///
    /// IMPORTANT: This class is intentionally a passive data container with respect
    /// to employment. It does NOT scan for production buildings or assign workers
    /// itself. All worker accounting is performed by <see cref="WorkerManager"/>,
    /// which is the single source of truth for assignment.
    /// </summary>
    public class Residence : MonoBehaviour
    {
        [Tooltip("How many residents can live here. This is the maximum number of workers this residence can supply.")]
        [SerializeField] private int populationCapacity = 4;

        public int PopulationCapacity => populationCapacity;

        public int AssignedWorkers { get; private set; }

        public int Unemployed => Mathf.Max(0, populationCapacity - AssignedWorkers);

        internal void SetAssignedWorkersInternal(int value)
        {
            AssignedWorkers = Mathf.Clamp(value, 0, populationCapacity);
        }
    }
}
