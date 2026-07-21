using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDeath : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    private Text enemiesKilledText;
    public AudioSource audioSrc;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
        enemiesKilledText = GameObject.FindGameObjectWithTag("EnemyKilledUI").GetComponent<Text>();
    }

    public void Death()
    {
        if(gameObject != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            audioSrc.Play();
            Destroy(gameObject);
            progressionCheck.enemiesDefeated += 1;
            enemiesKilledText.text = progressionCheck.enemiesDefeated.ToString();
            Debug.Log("Enemies Defeated: " + progressionCheck.enemiesDefeated);
        }
    }
}
