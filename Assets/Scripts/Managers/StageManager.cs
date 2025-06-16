using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public ObservableProperty<int> CurrentStage = new();

    private void Awake()
    {
        CurrentStage.Value = 1;
    }

    public void GoToShopScene()
    {
        Debug.Log("Go To Shop");
    }
}
