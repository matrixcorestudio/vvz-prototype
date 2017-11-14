using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image cardImage;
    public Button discardButton;
    CardData m_card;
    Inventory m_inventory;

    private void Start()
    {
       // m_inventory = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Inventory>();
    }

    public void AddCard(CardData newCard)
    {
        m_card = newCard;
        cardImage.sprite = newCard.cardImage;
        cardImage.enabled = true;
        discardButton.interactable = true;
    }

    public void ClearSlot()
    {
        m_card = null;
        cardImage.sprite = null;
        cardImage.enabled = false;
        discardButton.interactable = false;
    }

    public void OnDiscardButton()
    {
        m_inventory.Remove(m_card);
    }

    public void OnUseCard()
    {
        if (m_card != null)
        {
            //m_card.Use();
        }
    }
}
