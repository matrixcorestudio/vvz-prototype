using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class CharacterRandomMover : MonoBehaviour {
	public int xLimit;
	public int zLimit;
	PlayerMover m_playerMover;
	Vector3[] directions = 
	{
		Vector3.forward,
		Vector3.back,
		Vector3.right,
		Vector3.left,
	};

	void Start () 
	{
		m_playerMover = GetComponent<PlayerMover>();
	}

	void Update () 
	{
		if(!m_playerMover.isMoving)
		{
			m_playerMover.Move(RandomPoint(),0);
		}
	}
		
	Vector3 RandomPoint ()
	{
		Vector3 newPoint = transform.position + directions[Random.Range(0, directions.Length)];
		newPoint.x = Mathf.Clamp(newPoint.x, -xLimit, xLimit);
		newPoint.z = Mathf.Clamp(newPoint.z, -zLimit, zLimit);
		return newPoint;
	}
}
