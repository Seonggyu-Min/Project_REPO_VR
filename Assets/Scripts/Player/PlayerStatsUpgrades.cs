using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public enum UpgradeType
{
    MoveSpeed,
    RayDistance
}

public class PlayerStatsUpgrades : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentStatsText;
    [SerializeField] private TMP_Text _currentGoldText;

    [SerializeField] private TMP_Text _moveSpeedUpgradeCostText;
    [SerializeField] private TMP_Text _rayDistanceUpgradeCostText;

    [SerializeField] private GameObject _CantUpgradeSpeedInfoText;
    [SerializeField] private GameObject _CantUpgradeRayDistanceInfoText;

    [SerializeField] private PlayerStatsUpdater _playerStatsUpdater;

    private StringBuilder _sb = new();

    private Coroutine _cantUpgradeSpeedCoroutine;
    private Coroutine _cantUpgradeRayDistanceCoroutine;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void OnSelectedMoveSpeedUpgrade()
    {
        if (!GameManager.Instance.UpgradeManager.TryUpgradeMoveSpeed())
        {
            _cantUpgradeSpeedCoroutine = StartCoroutine(ShowCantUpgradeInfoText(UpgradeType.MoveSpeed));
            return;
        }

        UpdateUI();
        _playerStatsUpdater.SetUpStats();
    }

    public void OnSelectedRayDistanceUpgrade()
    {
        if (!GameManager.Instance.UpgradeManager.TryUpgradeRayDistance())
        {
            _cantUpgradeRayDistanceCoroutine = StartCoroutine(ShowCantUpgradeInfoText(UpgradeType.RayDistance));
            return;
        }

        UpdateUI();
        _playerStatsUpdater.SetUpStats();
    }

    private void UpdateUI()
    {
        UpdateCurrentGoldText();
        UpdateCurrentStatText();
        UpdateUpgradeCosts();
    }

    private void UpdateCurrentGoldText()
    {
        _sb.Clear();
        _sb.Append("현재 골드: ");
        _sb.Append(GameManager.Instance.GoldManager.CurrentGold);
        _sb.Append("$");
        _currentGoldText.text = _sb.ToString();
    }

    private void UpdateCurrentStatText()
    {
        _sb.Clear();
        _sb.Append("현재 스탯\n");
        _sb.Append("이동속도: ").Append(GameManager.Instance.UpgradeManager.GetCurrentMoveSpeed()).Append("\n");
        _sb.Append("레이 거리: ").Append(GameManager.Instance.UpgradeManager.GetCurrentRayDistance());
        _currentStatsText.text = _sb.ToString();
    }

    private void UpdateUpgradeCosts()
    {
        _moveSpeedUpgradeCostText.text = $"이동속도 업그레이드: {GameManager.Instance.UpgradeManager.MoveSpeedUpgradeCost}$";
        _rayDistanceUpgradeCostText.text = $"레이 거리 업그레이드: {GameManager.Instance.UpgradeManager.RayDistanceUpgradeCost}$";
    }

    private IEnumerator ShowCantUpgradeInfoText(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.MoveSpeed:
                if (_cantUpgradeSpeedCoroutine != null)
                {
                    StopCoroutine(_cantUpgradeSpeedCoroutine);
                }

                _CantUpgradeSpeedInfoText.SetActive(true);
                _moveSpeedUpgradeCostText.gameObject.SetActive(false);
                yield return new WaitForSeconds(3f);
                _CantUpgradeSpeedInfoText.SetActive(false);
                _moveSpeedUpgradeCostText.gameObject.SetActive(true);
                _cantUpgradeSpeedCoroutine = null;
                break;

            case UpgradeType.RayDistance:
                if (_cantUpgradeRayDistanceCoroutine != null)
                {
                    StopCoroutine(_cantUpgradeRayDistanceCoroutine);
                }

                _CantUpgradeRayDistanceInfoText.SetActive(true);
                _rayDistanceUpgradeCostText.gameObject.SetActive(false);
                yield return new WaitForSeconds(3f);
                _CantUpgradeRayDistanceInfoText.SetActive(false);
                _rayDistanceUpgradeCostText.gameObject.SetActive(true);
                _cantUpgradeRayDistanceCoroutine = null;
                break;
        }
    }
}
