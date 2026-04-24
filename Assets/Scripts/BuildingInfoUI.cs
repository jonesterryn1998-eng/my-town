using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoUI : MonoBehaviour
{
    [SerializeField] private Text buildingNameText;
    [SerializeField] private Text workersText;
    [SerializeField] private Text detailText;

    private Residence selectedResidence;
    private ProductionBuilding selectedProductionBuilding;

    public void ShowResidence(Residence residence)
    {
        selectedResidence = residence;
        selectedProductionBuilding = null;
        gameObject.SetActive(true);
        Refresh();
    }

    public void ShowProductionBuilding(ProductionBuilding building)
    {
        selectedProductionBuilding = building;
        selectedResidence = null;
        gameObject.SetActive(true);
        Refresh();
    }

    public void Hide()
    {
        selectedResidence = null;
        selectedProductionBuilding = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (selectedResidence != null || selectedProductionBuilding != null)
            Refresh();
    }

    private void Refresh()
    {
        if (selectedResidence != null)
        {
            if (buildingNameText != null)
                buildingNameText.text = selectedResidence.gameObject.name;

            if (workersText != null)
                workersText.text = $"Workers: {selectedResidence.assignedWorkers} / {selectedResidence.PopulationCapacity}";

            if (detailText != null)
                detailText.text = $"Unemployed: {selectedResidence.Unemployed}";
        }
        else if (selectedProductionBuilding != null)
        {
            if (buildingNameText != null)
                buildingNameText.text = selectedProductionBuilding.gameObject.name;

            if (workersText != null)
                workersText.text = $"Workers: {selectedProductionBuilding.assignedWorkers} / {selectedProductionBuilding.MaxWorkers}";

            if (detailText != null)
                detailText.text = $"Income: {selectedProductionBuilding.Income:F0}";
        }
    }
}
