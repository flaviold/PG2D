using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
public class PlayerController : MonoBehaviour {

    private PlayerActions playerActions;
    private float movement;
    private bool jump;
    private bool shoot;
    private bool defend;
    
    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) playerActions.Jump();
        if (Input.GetButtonDown("Fire5")) playerActions.Defend();
        if (Input.GetButtonDown("Fire3")) playerActions.Shoot();
        playerActions.Move(Input.GetAxis("Horizontal"));
    }

    private void FixedUpdate()
    {
        //movement = Input.GetAxis("Horizontal");
    }
}
