using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BnUActions))]
public class BnUController : MonoBehaviour {

    private BnUActions playerActions;
    private Vector2 movement;

    // Use this for initialization
    void Start ()
    {
        playerActions = GetComponent<BnUActions>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetButtonDown("Jump")) playerActions.Jump();
        //if (Input.GetButtonDown("Fire5")) playerActions.Defend();
        //if (Input.GetButtonDown("Fire3")) playerActions.Shoot();

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerActions.Move(movement);
    }
}
