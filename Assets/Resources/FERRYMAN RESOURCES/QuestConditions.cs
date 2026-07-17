using UnityEngine;

public class QuestConditions : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    public int minimumSoulsCollected;
    public int minimumEnemiesKilled;
    public bool givesGiftofMnemosyne;
    public bool givesFireResistancePotion;
    public bool givesGiftofEuphrosyne;
    public bool givesTrueSight;

    [Header("Dialogue")]
    public string textBeforeQuest;
    public string textAfterQuest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
    }

    // Update is called once per frame
    public void CheckConditions()
    {
        if (progressionCheck.soulsCollected < minimumSoulsCollected)
        {
            Debug.Log("Not Enough Souls");
            GetComponent<DialogueBalloonAction>().textToDisplay = textBeforeQuest;
        }

        if (progressionCheck.enemiesDefeated < minimumEnemiesKilled)
        {
            Debug.Log("Not Enough Enemies");
            GetComponent<DialogueBalloonAction>().textToDisplay = textBeforeQuest;
        }
        if (progressionCheck.enemiesDefeated >= minimumEnemiesKilled && progressionCheck.soulsCollected >= minimumSoulsCollected)
        {
            GivesGift();
        }
    }

    void GivesGift()
    {
        if (givesGiftofMnemosyne)
        {
            progressionCheck.hasGiftofMnemosyne = true;
            Debug.Log("Charon Receives Gift of Mnemosyne");
            GetComponent<DialogueBalloonAction>().textToDisplay = textAfterQuest;
        }
        else if (givesFireResistancePotion)
        {
            progressionCheck.hasFireResistancePotion = true;
            Debug.Log("Charon Receives Fire Resistance Potion");
            GetComponent<DialogueBalloonAction>().textToDisplay = textAfterQuest;
        }
        else if (givesGiftofEuphrosyne)
        {
            progressionCheck.hasGiftofEuphrosyne = true;
            Debug.Log("Charon Receives Gift of Euphrosyne");
            GetComponent<DialogueBalloonAction>().textToDisplay = textAfterQuest;
        }
        else if (givesTrueSight)
        {
            progressionCheck.hasTrueSight = true;
            Debug.Log("Charon Receives Gift of True Sight");
            GetComponent<DialogueBalloonAction>().textToDisplay = textAfterQuest;
        }
    }
}
