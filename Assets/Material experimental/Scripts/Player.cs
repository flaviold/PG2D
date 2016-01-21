using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {

    public float maxSpeed = 3f;
    public float speed = 50f;
    public float jumpPower = 50f;

    public ForceMode2D fMode2D;

    [HideInInspector]
    public bool grounded = true;

    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        var force = (Vector2.right * speed * h);
        Debug.Log("Força: " + force + "\nAxis: " + h);

        //Limitar Velocidade
        if (rb2d.velocity.x > maxSpeed) rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        if (rb2d.velocity.x < -maxSpeed) rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);

        //Virar Personagem
        if (h < 0) transform.localScale = new Vector3(-1, transform.localScale.y);
        if (h > 0) transform.localScale = new Vector3(1, transform.localScale.y);

        //Parar Personagem quando eixo for 0
        if (h == 0) rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
}
