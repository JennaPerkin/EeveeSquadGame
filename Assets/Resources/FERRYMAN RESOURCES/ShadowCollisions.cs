using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShadowCollisions : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    void Start()
    {
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shadow Hit Something");
        if(collision.gameObject.CompareTag("Shadow"))
        {
            Debug.Log("Hit Shadow");
            Destroy(collision.gameObject);
            transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z);
            progressionCheck.enemiesDefeated += 1;
            Debug.Log("Enemies Defeated: " + progressionCheck.enemiesDefeated);
        }

        else if(collision.gameObject.CompareTag("DestroyShadow"))
        {
            Debug.Log("Shadow Hit Ground");
            Destroy(gameObject);
        }
    }
}
