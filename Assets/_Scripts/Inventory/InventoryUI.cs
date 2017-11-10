using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform cardsParent;
    public GameObject inventoryUI;
    Inventory m_inventory;
    InventorySlot[] m_slots;
	void Start ()
    {
        m_inventory = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Inventory>();
        m_inventory.inventoryChangeEvent += UpdateUI;
        m_slots = cardsParent.GetComponentsInChildren<InventorySlot>();
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Inventory"))
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
