using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour
{
    private GameObject player;
    private AIController aiController;

    void Start()
    {
        player = transform.parent.gameObject;
        aiController = transform.GetComponentInParent<AIController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Player") && (col.gameObject != player))
        {
            aiController.ChangeState(StatesEnum.Attack, col.gameObject);
        }
    }
}
