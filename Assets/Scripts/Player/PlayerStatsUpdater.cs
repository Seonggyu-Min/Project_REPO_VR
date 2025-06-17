using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerStatsUpdater : MonoBehaviour
{
    public DynamicMoveProvider MoveProvider;
    public XRRayInteractor LeftRayInteractor;
    public XRRayInteractor RightRayInteractor;

    private void Start()
    {
        SetUpStats();
    }

    [ContextMenu("Force Update Player Stats")]
    public void SetUpStats()
    {
        UpgradeManager upgrade = GameManager.Instance.UpgradeManager;

        MoveProvider.moveSpeed = upgrade.GetCurrentMoveSpeed();
        LeftRayInteractor.maxRaycastDistance = upgrade.GetCurrentRayDistance();
        RightRayInteractor.maxRaycastDistance = upgrade.GetCurrentRayDistance();
    }
}
