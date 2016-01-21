using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

    public float speed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shield")
        {
            //Vector3 tempVec = GetComponent<Rigidbody>().velocity;
            //tempVec.x *= -1;
            //GetComponent<Rigidbody>().velocity = tempVec;
            //return;
        }
        if (other.tag == "Player")
        {
            //GameManager.Kill(other.name);
        }
        Destroy(gameObject);
    }

    public void Shoot(float scale)
    {
        gameObject.transform.localScale = new Vector3(scale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        Vector2 direction = new Vector2(scale, 0).normalized;

        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
