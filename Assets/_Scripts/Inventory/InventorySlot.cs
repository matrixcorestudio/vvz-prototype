using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Image cardImage;
    public Button discardButton;
    public Button useButton;
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
        descriptionText.text = 
            "Quick: "+(newCard.isQuick?"Yes":"No")+"\n" 
            +"Target: "+newCard.target.ToString()+"\n"
            +newCard.description;
        cardImage.sprite = newCard.cardImage;
        cardImage.enabled = true;
        discardButton.interactable = true;
        useButton.interactable = true;
    }

    public void ClearSlot()
    {
        m_card = null;
        nameText.text = "";
        descriptionText.text = "";
        cardImage.sprite = null;
        cardImage.enabled = false;
        discardButton.interactable = false;
        useButton.interactable = false;
    }

    public void OnDiscardButton()
    {
        m_inventory.Remove(m_card);
    }

    public void OnUseCardButton()
    {
        if (m_card != null)
        {
            //m_card.Use();
            m_inventory.Remove(m_card);
        }
    }
}
