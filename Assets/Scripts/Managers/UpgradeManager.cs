using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class UpgradeManager : MonoBehaviour
{
    public int MoveSpeedUpgradeCount { get; private set; } = 0;
    public int RayDistanceUpgradeCount { get; private set; } = 0;

    public float BaseMoveSpeed = 1.0f;
    public float MoveSpeedIncrement = 0.2f;

    public float BaseRayDistance = 5.0f;
    public float RayDistanceIncrement = 1.0f;

    public int MoveSpeedUpgradeCost => 1000 + MoveSpeedUpgradeCount * 1000;
    public int RayDistanceUpgradeCost => 1000 + RayDistanceUpgradeCount * 1000;

    public bool TryUpgradeMoveSpeed()
    {
        if (GameManager.Instance.GoldManager.CurrentGold < MoveSpeedUpgradeCost)
            return false;

        GameManager.Instance.GoldManager.RemoveGold(MoveSpeedUpgradeCost);
        MoveSpeedUpgradeCount++;
        return true;
    }

    public bool TryUpgradeRayDistance()
    {
        if (GameManager.Instance.GoldManager.CurrentGold < RayDistanceUpgradeCost)
            return false;

        GameManager.Instance.GoldManager.RemoveGold(RayDistanceUpgradeCost);
        RayDistanceUpgradeCount++;
        return true;
    }

    public void ResetPlayerStats()
    {
        MoveSpeedUpgradeCount = 0;
        RayDistanceUpgradeCount = 0;
    }

    public float GetCurrentMoveSpeed() => BaseMoveSpeed + MoveSpeedUpgradeCount * MoveSpeedIncrement;
    public float GetCurrentRayDistance() => BaseRayDistance + RayDistanceUpgradeCount * RayDistanceIncrement;
}
