using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager statManager;

    [SerializeField] TextMeshProUGUI Wood;
    [SerializeField] TextMeshProUGUI Fiber;
    [SerializeField] TextMeshProUGUI Hide;
    [SerializeField] TextMeshProUGUI Stone;
    [SerializeField] TextMeshProUGUI Iron;

    private void Awake() // Singleton pattern
    {
        if (statManager != null && statManager != this)
        {
            Destroy(this);
        }
        else
        {
            statManager = this;
        }
    }
    void Update()
    {
        
    }

    public void UpdateStats(Dictionary<string, (Item item, int count)> itemCounts)
    {
        if (itemCounts.ContainsKey("Wood"))
            Wood.text = "Wood: " + itemCounts["Wood"].count.ToString();
        else
            Wood.text = "Wood: 0";

        if (itemCounts.ContainsKey("Fiber"))
            Fiber.text = "Fiber: " + itemCounts["Fiber"].count.ToString();
        else
            Fiber.text = "Fiber: 0";

        if (itemCounts.ContainsKey("Hide"))
            Hide.text = "Hide: " + itemCounts["Hide"].count.ToString();
        else
            Hide.text = "Hide: 0";

        if (itemCounts.ContainsKey("Stone"))
            Stone.text = "Stone: " + itemCounts["Stone"].count.ToString();
        else
            Stone.text = "Stone: 0";

        if (itemCounts.ContainsKey("Iron"))
            Iron.text = "Iron: " + itemCounts["Iron"].count.ToString();
        else
            Iron.text = "Iron: 0";
    }
}
