using UnityEngine;

public class CardData : ScriptableObject 
{
	public enum CardType
	{
		None, Blessing, Curse
	}

	public enum CardTarget
	{
		Any, Viking, Zombie
	}

	[Header("General Info")]
	public int id;
	[TextArea(2,4)]
	public string description;
	public bool isQuick;
	public CardType type;
	public CardTarget target;
	public Sprite cardImage;

}
