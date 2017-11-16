using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Text nameText;
    public Image cardImage;
    public Button discardButton;
    CardData m_card;
    Inventory m_inventory;

    public void Init(Inventory inventory)
    {
        m_inventory = inventory;
    }

    public void AddCard(CardData newCard)
    {
        m_card = newCard;
        nameText.text = newCard.name;
        cardImage.sprite = newCard.cardImage;
        cardImage.enabled = true;
        discardButton.interactable = true;
    }

    public void ClearSlot()
    {
        m_card = null;
        nameText.text = "";
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
