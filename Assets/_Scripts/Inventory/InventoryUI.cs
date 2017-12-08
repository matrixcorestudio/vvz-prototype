using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform cardsParent;
    public GameObject inventoryUI;
    public GameObject inventoryButtonsPanel;
    Inventory m_inventory;
    PlayerDrawCard m_playerDrawCard;
    InventorySlot[] m_slots;
    bool isInitialized = false;

    public void Init(Inventory inventory, bool isLocalPlayer = false)
    {
        m_inventory = inventory;
        m_playerDrawCard = m_inventory.gameObject.GetComponent<PlayerDrawCard>();
        m_inventory.InventoryChangeEvent += UpdateUI;
        m_slots = cardsParent.GetComponentsInChildren<InventorySlot>();
        foreach (var slot in m_slots)
        {
            slot.Init(m_inventory);
            if (!isLocalPlayer)
            {
                slot.discardButton.gameObject.SetActive(false);
                slot.useButton.gameObject.SetActive(false);
            }
        }
        isInitialized = true;
        if (!isLocalPlayer)
        {
            inventoryButtonsPanel.SetActive(false);
        }
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

    /**Inventory options**/
    public void DrawRandomButton()
    {
        m_playerDrawCard.CmdDrawRandom();
    }

    public void DrawBlessingButton()
    {
        m_playerDrawCard.CmdDrawBlessing();
    }

    public void DrawCurseButton()
    {
        m_playerDrawCard.CmdDrawCurse();
    }
}
