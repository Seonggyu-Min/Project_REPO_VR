using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundFinishButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _infoTextObj;
    [SerializeField] private GameObject _nextStageBtnObj;

    private Coroutine _infoVanishCoroutine;

    public void OnRoundFinishButtonSelected()
    {
        if (GameManager.Instance.QuotaManager.CurrentQuota.Value < GameManager.Instance.QuotaManager.RequiredQuota.Value)
        {
            _infoVanishCoroutine = StartCoroutine(InfoVanishRoutine());
        }
        else
        {
            GameManager.Instance.StageManager.GoToShopScene();
        }
    }

    private IEnumerator InfoVanishRoutine()
    {
        _nextStageBtnObj.SetActive(false);
        _infoTextObj.SetActive(true);
        yield return new WaitForSeconds(10f);
        _nextStageBtnObj.SetActive(true);
        _infoTextObj.SetActive(false);
    }
}
