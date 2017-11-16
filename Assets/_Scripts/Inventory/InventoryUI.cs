using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform cardsParent;
    public GameObject inventoryUI;
    Inventory m_inventory;
    InventorySlot[] m_slots;
    bool isInitialized = false;

    public void Init(Inventory inventory)
    {
        m_inventory = inventory;
        m_inventory.InventoryChangeEvent += UpdateUI;
        m_slots = cardsParent.GetComponentsInChildren<InventorySlot>();
        foreach (var slot in m_slots)
        {
            slot.Init(m_inventory);
        }
        isInitialized = true;
    }
	
	void Update ()
    {
        if (isInitialized && Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    void UpdateUI()
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            if (i < m_inventory.cards.Count)
            {
                m_slots[i].AddCard(m_inventory.cards[i]);
            }
            else
            {
                m_slots[i].ClearSlot();
            }
        }
    }
}
