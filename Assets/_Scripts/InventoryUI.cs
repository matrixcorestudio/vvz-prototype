using UnityEngine;

public class InventoryUI : MonoBehaviour {

    Inventory inventory;
	void Start ()
    {
        inventory = Inventory.Instance;
        inventory.cardChangeEvent += UpdateUI;
    }
	
	void Update ()
    {
		
	}

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
