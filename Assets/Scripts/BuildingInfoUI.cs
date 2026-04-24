using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays worker counts from Residence / ProductionBuilding only (no per-building residence maps).
/// </summary>
public class BuildingInfoUI : MonoBehaviour
{
    [SerializeField] Text residenceAssignedText;
    [SerializeField] Text residenceUnemployedText;
    [SerializeField] Text productionAssignedText;

    [SerializeField] Residence residenceTarget;
    [SerializeField] ProductionBuilding productionTarget;

    void Update()
    {
        if (residenceTarget != null)
        {
            if (residenceAssignedText != null)
                residenceAssignedText.text = residenceTarget.AssignedWorkers.ToString();
            if (residenceUnemployedText != null)
                residenceUnemployedText.text = residenceTarget.Unemployed.ToString();
        }

        if (productionTarget != null && productionAssignedText != null)
            productionAssignedText.text = productionTarget.AssignedWorkers.ToString();
    }
}
