using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDeath : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    private Text enemiesKilledText;
    public AudioSource audioSrc;
    bool hasPointBeenScored = false;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
        enemiesKilledText = GameObject.FindGameObjectWithTag("EnemyKilledUI").GetComponent<Text>();
    }

    public void Death()
    {
        Destroy(gameObject);
        if (!hasPointBeenScored)
        {
            hasPointBeenScored = true;
            progressionCheck.enemiesDefeated += 1;
            enemiesKilledText.text = progressionCheck.enemiesDefeated.ToString();
            Debug.Log("Enemies Defeated: " + progressionCheck.enemiesDefeated);
        }
        if (audioSrc != null)
        {
            audioSrc.Play();
        }
    }
}
