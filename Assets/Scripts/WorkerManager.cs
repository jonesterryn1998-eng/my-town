using System.Collections.Generic;
using UnityEngine;

namespace MyTown
{
    /// <summary>
    /// Centralized worker assignment system.
    ///
    /// This is the ONLY place in the codebase that decides how many workers a
    /// <see cref="ProductionBuilding"/> has and how many residents of a
    /// <see cref="Residence"/> are employed. Running it once per tick guarantees
    /// a deterministic distribution with no double counting and no per-building
    /// scanning of residences.
    ///
    /// Algorithm (executed once per tick):
    ///   1. Build a global worker pool by summing population capacity across
    ///      every Residence.
    ///   2. Walk every ProductionBuilding in order and grant it
    ///      min(maxWorkers, remainingPool) workers, deducting from the pool.
    ///   3. Walk every Residence in order and grant it employment up to its
    ///      populationCapacity from the assigned-workers total, deducting as
    ///      we go. The leftover at each residence is its unemployed count.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class WorkerManager : MonoBehaviour
    {
        [Tooltip("Seconds between worker reassignment ticks. 0 = every Update frame.")]
        [SerializeField] private float tickInterval = 1f;

        private float tickTimer;

        private static readonly List<Residence> ResidenceBuffer = new List<Residence>();
        private static readonly List<ProductionBuilding> ProductionBuffer = new List<ProductionBuilding>();

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer < tickInterval)
            {
                return;
            }

            tickTimer = 0f;
            Tick();
        }

        /// <summary>
        /// Runs a single assignment tick. Public so tests and other systems can
        /// drive it deterministically without waiting on Unity's Update loop.
        /// </summary>
        public void Tick()
        {
            CollectAll(ResidenceBuffer, ProductionBuffer);
            Assign(ResidenceBuffer, ProductionBuffer);
        }

        private static void CollectAll(List<Residence> residences, List<ProductionBuilding> productions)
        {
            residences.Clear();
            productions.Clear();

            // FindObjectsOfType is the supported Unity API across versions used in
            // this project. We accept its cost because assignment runs at a low
            // tick rate, not every frame.
            residences.AddRange(Object.FindObjectsOfType<Residence>());
            productions.AddRange(Object.FindObjectsOfType<ProductionBuilding>());
        }

        /// <summary>
        /// Pure assignment step extracted from <see cref="Tick"/> so it can be
        /// driven directly with explicit collections (useful for tests and for
        /// callers that already have references in hand).
        /// </summary>
        public static void Assign(IList<Residence> residences, IList<ProductionBuilding> productions)
        {
            int workerPool = 0;
            for (int i = 0; i < residences.Count; i++)
            {
                Residence r = residences[i];
                if (r == null) continue;
                workerPool += Mathf.Max(0, r.PopulationCapacity);
            }

            int totalAssigned = 0;
            for (int i = 0; i < productions.Count; i++)
            {
                ProductionBuilding p = productions[i];
                if (p == null) continue;

                int give = Mathf.Min(p.MaxWorkers, workerPool);
                if (give < 0) give = 0;

                p.SetAssignedWorkersInternal(give);
                workerPool -= give;
                totalAssigned += give;
            }

            int remainingToDistribute = totalAssigned;
            for (int i = 0; i < residences.Count; i++)
            {
                Residence r = residences[i];
                if (r == null) continue;

                int take = Mathf.Min(r.PopulationCapacity, remainingToDistribute);
                if (take < 0) take = 0;

                r.SetAssignedWorkersInternal(take);
                remainingToDistribute -= take;
            }
        }
    }
}
