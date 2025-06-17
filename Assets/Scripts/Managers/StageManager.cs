using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class StageManager : MonoBehaviour
{
    public ObservableProperty<int> CurrentStage = new();

    private void Awake()
    {
        CurrentStage.Value = 1;
    }

    [ContextMenu("Go To Shop")]
    public void GoToShopScene()
    {
        CurrentStage.Value++;
        SceneManager.LoadScene(1);
        Debug.Log("Go To Shop");
    }

    [ContextMenu("Go To Game")]
    public void GoToGameScene()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Go To Game");
    }

    [ContextMenu("Restart Game")]
    public void GoToGameOverScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToGameSceneWithRestart()
    {
        CurrentStage.Value = 1;
        GameManager.Instance.UpgradeManager.ResetPlayerStats();
        GameManager.Instance.GoldManager.ResetGold();
        SceneManager.LoadScene(0);
    }
}
