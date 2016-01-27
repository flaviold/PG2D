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
        if (!jump)
        {
            jump = (Input.GetButtonDown("Jump"));
        }
        defend = Input.GetButtonDown("Fire5");
        shoot = Input.GetButtonDown("Fire3");
        playerActions.Move(movement, jump);
		playerActions.Shoot(shoot);
        playerActions.Defend(defend);
        jump = false;
    }

    private void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
    }
}
