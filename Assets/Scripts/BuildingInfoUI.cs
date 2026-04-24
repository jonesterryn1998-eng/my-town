using UnityEngine;

public class BuildingInfoUI : MonoBehaviour
{
    [SerializeField] private Residence selectedResidence;
    [SerializeField] private ProductionBuilding selectedProductionBuilding;

    // Kept intentionally simple: UI rendering systems can read these values directly.
    public int ResidenceAssignedWorkers =>
        selectedResidence != null ? selectedResidence.assignedWorkers : 0;

    public int ResidenceUnemployed =>
        selectedResidence != null ? selectedResidence.unemployed : 0;

    public int ProductionAssignedWorkers =>
        selectedProductionBuilding != null ? selectedProductionBuilding.assignedWorkers : 0;
}
