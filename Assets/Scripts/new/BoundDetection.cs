using UnityEngine;
using System.Collections;

public class BoundDetection : MonoBehaviour {

    private BnUActions playerActions;

    void Start()
    {
        playerActions = gameObject.GetComponentInParent<BnUActions>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!playerActions) playerActions = gameObject.GetComponentInParent<BnUActions>();
        if (col.gameObject.tag == "Bounds") Debug.Log(gameObject.name);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!playerActions) playerActions = gameObject.GetComponentInParent<BnUActions>();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!playerActions) playerActions = gameObject.GetComponentInParent<BnUActions>();
        if (col.gameObject.tag == "Bounds") Debug.Log("Not " + gameObject.name);
    }
}
