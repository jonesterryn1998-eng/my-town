using UnityEngine;

namespace MyTown
{
    /// <summary>
    /// A building that requires workers to operate and generates income based on
    /// how many workers have been assigned to it.
    ///
    /// IMPORTANT: This class never scans residences and never assigns workers.
    /// <see cref="AssignedWorkers"/> is set exclusively by <see cref="WorkerManager"/>.
    /// </summary>
    public class ProductionBuilding : MonoBehaviour
    {
        [Tooltip("Maximum number of workers this building can employ.")]
        [SerializeField] private int maxWorkers = 10;

        [Tooltip("Income generated per fully-employed worker per tick.")]
        [SerializeField] private float incomePerWorker = 1f;

        public int MaxWorkers => maxWorkers;

        public int AssignedWorkers { get; private set; }

        public float CurrentIncome => AssignedWorkers * incomePerWorker;

        internal void SetAssignedWorkersInternal(int value)
        {
            AssignedWorkers = Mathf.Clamp(value, 0, maxWorkers);
        }
    }
}
