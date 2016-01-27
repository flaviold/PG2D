using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerActions : MonoBehaviour
{

    public float speed = 8f;
    public float jumpPower = 11f;

    public float fireRate;
    public float shieldRate;

    public GameObject shot;
    public GameObject shootSpawn;
    public GameObject shield;

    private float shieldUp = 0;
    private float nxtFire = 0;

    const int countOfDamageAnimations = 1;
    int lastDamageAnimation = -1;

    [HideInInspector]
    public bool isGrounded = true;

    private Animator animator;
    private Rigidbody2D rb2D;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float movement, bool jump)
    {
        Jump(jump);
        if (movement != 0)
        {
            Rotate(movement);
            animator.SetFloat("Speed", Mathf.Abs(movement) * speed);
            rb2D.velocity = new Vector2(movement * speed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetFloat("Speed", 0);
        }
    }

    public void Rotate(float movement)
    {
        if (movement < 0) transform.localScale = new Vector3(-1, transform.localScale.y);
        if (movement > 0) transform.localScale = new Vector3(1, transform.localScale.y);
    }

    public void Shoot(bool shoot)
    {
        if (shoot && Time.time > shieldUp)
        {
            float scale = gameObject.transform.localScale.x;
            var shootSpawnAnim = shootSpawn.GetComponent<Animator>();
            shootSpawnAnim.SetTrigger("Shoot");
            animator.SetTrigger("Shoot");
            GameObject shotClone = (GameObject)Instantiate(shot, shootSpawn.transform.position, shootSpawn.transform.rotation);
            shotClone.GetComponent<ShootController>().Shoot(scale);
            nxtFire = Time.time + fireRate;
        }
        //animator.SetFloat("Shooting", Mathf.Abs(fireAnimation));
    }

    public void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");
    }

    public void Defend(bool defend)
    {
        if (defend && Time.time > nxtFire)
        {
            shield.GetComponent<Animator>().SetTrigger("Shield");
            nxtFire = Time.time + fireRate;
        }
    }

    public void Damage()
    {
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        //int id = Random.Range(0, countOfDamageAnimations);
        //if (countOfDamageAnimations > 1)
        //    while (id == lastDamageAnimation)
        //        id = Random.Range(0, countOfDamageAnimations);
        //lastDamageAnimation = id;
        //animator.SetInteger("DamageID", id);
        //animator.SetTrigger("Damage");
    }

    public void Jump(bool jump)
    {
        if (isGrounded && jump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
            //isGrounded = false;
        }
    }

    public void Sitting()
    {
        animator.SetBool("Squat", !animator.GetBool("Squat"));
        animator.SetBool("Aiming", false);
    }

    //void CheckGroundStatus()
    //{
    //    RaycastHit hitInfo;
    //    // 0.1f is a small offset to start the ray from inside the character
    //    // it is also good to note that the transform position in the sample assets is at the base of the character
    //    if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f))
    //    {
    //        isGrounded = true;
    //        animator.SetBool("Grounded", isGrounded);
    //    }
    //    else
    //    {
    //        isGrounded = false;
    //        animator.SetBool("Grounded", isGrounded);
    //    }
    //}
}
