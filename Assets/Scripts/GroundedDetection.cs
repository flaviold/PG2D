using UnityEngine;
using System.Collections;

public class GroundedDetection : MonoBehaviour {

    private PlayerActions playerActions;

    void Start()
    {
        playerActions = gameObject.GetComponentInParent<PlayerActions>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		if (!playerActions) playerActions = gameObject.GetComponentInParent<PlayerActions>();
        playerActions.isGrounded = true;
    }

    void OnTriggerStay2D(Collider2D col)
	{
		if (!playerActions) playerActions = gameObject.GetComponentInParent<PlayerActions>();
        playerActions.isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
	{
		if (!playerActions) playerActions = gameObject.GetComponentInParent<PlayerActions>();
        playerActions.isGrounded = false;
    }
}
