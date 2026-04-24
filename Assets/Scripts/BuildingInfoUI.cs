using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoUI : MonoBehaviour
{
    [Header("Optional selected targets")]
    [SerializeField]
    private Residence selectedResidence;

    [SerializeField]
    private ProductionBuilding selectedProductionBuilding;

    [Header("UI output fields")]
    [SerializeField]
    private Text residenceAssignedWorkersText;

    [SerializeField]
    private Text residenceUnemployedText;

    [SerializeField]
    private Text productionAssignedWorkersText;

    private void Update()
    {
        if (selectedResidence != null)
        {
            if (residenceAssignedWorkersText != null)
            {
                residenceAssignedWorkersText.text = selectedResidence.assignedWorkers.ToString();
            }

            if (residenceUnemployedText != null)
            {
                residenceUnemployedText.text = selectedResidence.unemployed.ToString();
            }
        }

        if (selectedProductionBuilding != null && productionAssignedWorkersText != null)
        {
            productionAssignedWorkersText.text = selectedProductionBuilding.assignedWorkers.ToString();
        }
    }
}
