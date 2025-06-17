using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuotaManager : MonoBehaviour
{
    private StageManager _stageManager;

    public ObservableProperty<int> CurrentQuota = new(0);
    public ObservableProperty<int> RequiredQuota = new(0);

    private int _randomRange => (int)(RequiredQuota.Value * 0.1f);

    private void OnDestroy()
    {
        GameManager.Instance.StageManager.CurrentStage.Unsubscribe(SetRequiredQuota);
    }

    public void Init(StageManager stageManager)
    {
        _stageManager = stageManager;
        _stageManager.CurrentStage.Subscribe(SetRequiredQuota);

        SetCurrentQuotaToZero();
        SetRequiredQuota();
    }

    private void SetRequiredQuota()
    {
        int baseQuota = 5000 * GameManager.Instance.StageManager.CurrentStage.Value;
        int random = Random.Range(baseQuota - (int)(baseQuota * 0.1f), baseQuota + (int)(baseQuota * 0.1f));
        RequiredQuota.Value = random;
    }

    public void SetCurrentQuotaToZero()
    {
        CurrentQuota.Value = 0;
    }

    public void AddCurrentQuota(int value)
    {
        CurrentQuota.Value += value;
        GameManager.Instance.GoldManager.AddGold(value);
    }

    [ContextMenu("Test Add Gold")]
    public void TestAddGold()
    {
        AddCurrentQuota(10000);
    }
}
