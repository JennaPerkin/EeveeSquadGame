using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelWithCondition : MonoBehaviour
{
    private ProgressionVariables progressionCheck;
    public string LevelToLoad;
    public bool needsGiftofMnemosyne;
    public bool needsFireResistancePotion;
    public bool needsGiftFromEuphrosyne;
    public bool needsTrueSight;
    public void LoadLevel()
    {
        progressionCheck = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ProgressionVariables>();
        if(needsGiftofMnemosyne)
        {
            if (progressionCheck.hasGiftofMnemosyne) LoadLevelBackground();
        }

        if(needsFireResistancePotion)
        {
            if (progressionCheck.hasFireResistancePotion) LoadLevelBackground();
        }

        if(needsGiftFromEuphrosyne)
        {
            if (progressionCheck.hasGiftofEuphrosyne) LoadLevelBackground();
        }

        if (needsTrueSight)
            if (progressionCheck.hasTrueSight) LoadLevelBackground();
    }

    void LoadLevelBackground()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
