using UnityEngine;

public class QuestConditions : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    public int minimumSoulsCollected;
    public int minimumEnemiesKilled;
    public bool givesGiftof;
    public bool givesFireResistancePotion;
    public bool givesGiftofE;
    public bool givesTrueSight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
    }

    // Update is called once per frame
    void CheckConditions()
    {
        if (progressionCheck.soulsCollected < minimumSoulsCollected) Debug.Log("Not Enough Souls");
        if (progressionCheck.enemiesDefeated < minimumEnemiesKilled) Debug.Log("Not Enough Enemies");
        else
        {
            GivesGift();
        }
    }

    void GivesGift()
    {

    }
}
