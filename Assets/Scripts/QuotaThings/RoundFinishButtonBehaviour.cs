using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundFinishButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _infoTextObj;

    public void OnRoundFinishButtonSelected()
    {
        if (GameManager.Instance.QuotaManager.CurrentQuota < GameManager.Instance.QuotaManager.RequiredQuota)
        {
            InfoVanishRoutine();
        }
        else
        {
            GameManager.Instance.StageManager.GoToShopScene();
        }
    }

    private IEnumerator InfoVanishRoutine()
    {
        _infoTextObj.SetActive(true);
        yield return new WaitForSeconds(10f);
        _infoTextObj.SetActive(false);
    }
}
