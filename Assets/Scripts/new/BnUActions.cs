using UnityEngine;
using System.Collections;

public class BnUActions : MonoBehaviour {

    public float speed = 8f;
    public float acceleration = 4f;
    private float cSpeed = 0f;

    public float fireRate;
    public float shieldRate;

    public GameObject shot;
    public GameObject shootSpawn;
    public GameObject shield;

    private float shieldUp = 0;
    private float nxtFire = 0;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 movement)
    {
        var newPos = gameObject.transform.position;

        if (movement.magnitude != 0)
        {
            Rotate(movement.x);
            animator.SetFloat("Speed", Mathf.Abs(movement.magnitude) * speed);
            
            if (cSpeed < speed)
            {
                newPos.x += movement.x * cSpeed * Time.deltaTime;
                newPos.y += movement.y * cSpeed * Time.deltaTime;
                cSpeed += acceleration;
            }
            else
            {
                cSpeed = speed;
                newPos.x += movement.x * cSpeed * Time.deltaTime;
                newPos.y += movement.y * cSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (cSpeed > 0)
            {
                newPos.x += movement.x * cSpeed * Time.deltaTime;
                newPos.y += movement.y * cSpeed * Time.deltaTime;
                cSpeed += 2*acceleration;
            }
            else if (cSpeed < 0)
            {
                cSpeed = 0;
            }
            animator.SetFloat("Speed", 0);
        }

        gameObject.transform.position = newPos;
    }

    public void Rotate(float movement)
    {
        if (movement < 0) transform.localScale = new Vector3(-1, transform.localScale.y);
        if (movement > 0) transform.localScale = new Vector3(1, transform.localScale.y);
    }
}
