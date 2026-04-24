using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays employment information for a selected building.
/// Reads data directly from Residence.assignedWorkers / Residence.unemployed
/// and ProductionBuilding.assignedWorkers — no local worker calculations.
/// </summary>
public class BuildingInfoUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject infoPanel;

    [Header("Shared labels")]
    public TextMeshProUGUI buildingNameLabel;

    [Header("Production Building labels")]
    public TextMeshProUGUI productionWorkersLabel;
    public TextMeshProUGUI productionMaxWorkersLabel;

    [Header("Residence labels")]
    public TextMeshProUGUI residenceAssignedLabel;
    public TextMeshProUGUI residenceUnemployedLabel;
    public TextMeshProUGUI residenceCapacityLabel;

    private ProductionBuilding _selectedProduction;
    private Residence _selectedResidence;

    // ------------------------------------------------------------------
    // Public API — call these to show info for a clicked building
    // ------------------------------------------------------------------

    public void ShowProductionBuilding(ProductionBuilding building)
    {
        _selectedProduction = building;
        _selectedResidence = null;
        infoPanel.SetActive(true);
        Refresh();
    }

    public void ShowResidence(Residence residence)
    {
        _selectedResidence = residence;
        _selectedProduction = null;
        infoPanel.SetActive(true);
        Refresh();
    }

    public void Hide()
    {
        _selectedProduction = null;
        _selectedResidence = null;
        infoPanel.SetActive(false);
    }

    // ------------------------------------------------------------------
    // Internal refresh — called every frame so values stay current
    // ------------------------------------------------------------------

    private void Update()
    {
        if (infoPanel.activeSelf)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        if (_selectedProduction != null)
        {
            RefreshProductionBuilding(_selectedProduction);
        }
        else if (_selectedResidence != null)
        {
            RefreshResidence(_selectedResidence);
        }
    }

    private void RefreshProductionBuilding(ProductionBuilding building)
    {
        SetText(buildingNameLabel, building.name);
        SetText(productionWorkersLabel, building.assignedWorkers.ToString());
        SetText(productionMaxWorkersLabel, building.maxWorkers.ToString());

        // Hide residence-specific labels
        SetActive(residenceAssignedLabel, false);
        SetActive(residenceUnemployedLabel, false);
        SetActive(residenceCapacityLabel, false);

        SetActive(productionWorkersLabel, true);
        SetActive(productionMaxWorkersLabel, true);
    }

    private void RefreshResidence(Residence residence)
    {
        SetText(buildingNameLabel, residence.name);
        SetText(residenceAssignedLabel, residence.assignedWorkers.ToString());
        SetText(residenceUnemployedLabel, residence.unemployed.ToString());
        SetText(residenceCapacityLabel, residence.populationCapacity.ToString());

        // Hide production-specific labels
        SetActive(productionWorkersLabel, false);
        SetActive(productionMaxWorkersLabel, false);

        SetActive(residenceAssignedLabel, true);
        SetActive(residenceUnemployedLabel, true);
        SetActive(residenceCapacityLabel, true);
    }

    // ------------------------------------------------------------------
    // Helpers — guard against unassigned references in the Inspector
    // ------------------------------------------------------------------

    private static void SetText(TextMeshProUGUI label, string text)
    {
        if (label != null) label.text = text;
    }

    private static void SetActive(Component component, bool active)
    {
        if (component != null) component.gameObject.SetActive(active);
    }
}
