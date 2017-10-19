using UnityEngine;
using UnityEngine.Networking;

public class MiniZombie : NetworkBehaviour {

    private BoxCollider m_collider;
	public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            m_collider = GetComponent<BoxCollider>();
            if (m_collider != null)
            {
                m_collider.enabled = true;
            }
        }
	}
}
