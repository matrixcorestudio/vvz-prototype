using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

	public Vector3 destination;
	public bool isMoving = false;
	public iTween.EaseType easeType = iTween.EaseType.easeInOutQuint;
	public float moveSpeed = 1.5f;
	public float iTweenDelay = 0f;


	public void Move (Vector3 destinationPos, float delayTime = 0.25f)
	{
		StartCoroutine(MoveRoutine(destinationPos,delayTime));
	}

	IEnumerator MoveRoutine (Vector3 destinationPos, float delayTime)
	{
		isMoving = true;
		destination = destinationPos;
		yield return new WaitForSeconds(delayTime);
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", destinationPos,
			"delay", iTweenDelay,
			"easetype", easeType,
			"speed", moveSpeed
		));
		while(Vector3.Distance(destinationPos, transform.position) > 0.01f)
		{
			yield return null;
		}
		iTween.Stop(gameObject);
		transform.position = destinationPos;
		isMoving = false;
	}


	public void MoveLeft ()
	{
		Vector3 newPosition = transform.position + Vector3.left;
		Move(newPosition,0);
	}

	public void MoveRight ()
	{
		Vector3 newPosition = transform.position + Vector3.right;
		Move(newPosition,0);
	}

	public void MoveUp ()
	{
		Vector3 newPosition = transform.position + Vector3.up;
		Move(newPosition,0);
	}

	public void MoveDown ()
	{
		Vector3 newPosition = transform.position + Vector3.down;
		Move(newPosition,0);
	}
}
