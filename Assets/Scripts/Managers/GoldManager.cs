using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int CurrentGold { get; private set; } = 0;
    public int TotalGold { get; private set; } = 0;


    public void AddGold(int amount)
    {
        CurrentGold += amount;
        TotalGold += amount;
    }

    public void RemoveGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
        }
    }

    public void ResetGold()
    {
        CurrentGold = 0;
        TotalGold = 0;
    }
}
