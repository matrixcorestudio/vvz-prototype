using UnityEngine;

public class CharacterDrag : MonoBehaviour 
{
	public float distance = 10f;

	void OnMouseDrag ()
	{
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = new Vector3(Mathf.Round(objectPosition.x),Mathf.Round(objectPosition.y),Mathf.Round(objectPosition.z));
	}
}
