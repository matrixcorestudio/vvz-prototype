using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] float panSpeed = 10f;
	[SerializeField] float scrollSpeed = 2f;
	[SerializeField] Vector2 panLimit;
	[SerializeField] float minCameraSize = 5f;
	[SerializeField] float maxCameraSize = 15f;

	[SerializeField] float dragSpeed = 1.5f;
	Vector3 dragOrigin;

	Vector3 m_startPosition;
	Vector3 m_currentPosition;
	Vector2 m_keyInput;

	void Start()
	{
		m_startPosition = transform.position;
	}

	void Update () 
	{
		m_currentPosition = transform.position;
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Camera.main.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;

		m_keyInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		m_currentPosition.x += m_keyInput.x * panSpeed * Time.deltaTime;
		m_currentPosition.y += m_keyInput.y * panSpeed * Time.deltaTime;


		m_currentPosition.x = Mathf.Clamp(m_currentPosition.x, m_startPosition.x - panLimit.x, m_startPosition.x + panLimit.x);
		m_currentPosition.y = Mathf.Clamp(m_currentPosition.y, m_startPosition.y - panLimit.y, m_startPosition.y + panLimit.y);
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCameraSize, maxCameraSize);
		transform.position = m_currentPosition;
	}

	void LateUpdate ()
	{
		if (Input.GetMouseButtonDown(1))
		{
			dragOrigin = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(1)) return;

		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);

		transform.Translate(move, Space.World);
	}
		
}
