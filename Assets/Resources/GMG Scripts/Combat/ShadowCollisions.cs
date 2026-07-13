using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShadowCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Shadow Hit Something");
        if(collision.gameObject.CompareTag("Shadow"))
        {
            Debug.Log("Hit Shadow");
            Destroy(collision.gameObject);
            transform.localScale *= 2;
        }

        else if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Shadow Hit Ground");
            Destroy(gameObject);
        }
    }
}
