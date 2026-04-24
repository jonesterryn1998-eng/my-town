using UnityEngine;
using UnityEngine.UI;

namespace MyTown
{
    /// <summary>
    /// Displays employment information for a selected building.
    ///
    /// This component is read-only with respect to the worker system: it only
    /// reads <see cref="Residence.AssignedWorkers"/>, <see cref="Residence.Unemployed"/>
    /// and <see cref="ProductionBuilding.AssignedWorkers"/>. It never writes to
    /// them; assignment is the exclusive responsibility of <see cref="WorkerManager"/>.
    /// </summary>
    public class BuildingInfoUI : MonoBehaviour
    {
        [SerializeField] private Text infoLabel;

        [SerializeField] private Residence residenceTarget;
        [SerializeField] private ProductionBuilding productionTarget;

        public void SetTarget(Residence residence)
        {
            residenceTarget = residence;
            productionTarget = null;
        }

        public void SetTarget(ProductionBuilding production)
        {
            productionTarget = production;
            residenceTarget = null;
        }

        private void Update()
        {
            if (infoLabel == null)
            {
                return;
            }

            if (residenceTarget != null)
            {
                infoLabel.text =
                    $"Residence\n" +
                    $"Population: {residenceTarget.PopulationCapacity}\n" +
                    $"Employed:   {residenceTarget.AssignedWorkers}\n" +
                    $"Unemployed: {residenceTarget.Unemployed}";
                return;
            }

            if (productionTarget != null)
            {
                infoLabel.text =
                    $"Production Building\n" +
                    $"Workers: {productionTarget.AssignedWorkers} / {productionTarget.MaxWorkers}";
                return;
            }

            infoLabel.text = string.Empty;
        }
    }
}
