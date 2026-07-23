using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShadowCollisions : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    private Text enemiesKilledText;
    void Start()
    {
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
        enemiesKilledText = GameObject.FindGameObjectWithTag("EnemyKilledUI").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Shadow Hit Something");
        if(collision.gameObject.CompareTag("Shadow"))
        {
            if(collision != null)
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<BoxCollider2D>().enabled = false;
                bool isKilledEnemy = false;
                if(!isKilledEnemy)
                {
                    isKilledEnemy = true;
                    collision.GetComponent<EnemyDeath>().Death();
                }
                //Debug.Log("Hit Shadow");
                //Destroy(collision.gameObject);
                transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z);
                //progressionCheck.enemiesDefeated += 1;
                //enemiesKilledText.text = progressionCheck.enemiesDefeated.ToString();
                //Debug.Log("Enemies Defeated: " + progressionCheck.enemiesDefeated);
            }
        }

        else if(collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Shadow Hit Ground");
            Destroy(gameObject);
        }
    }
}
