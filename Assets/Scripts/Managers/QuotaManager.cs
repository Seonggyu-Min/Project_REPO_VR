using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuotaManager : MonoBehaviour
{
    private StageManager _stageManager;

    private int _requiredQuota;
    public int RequiredQuota { get { return _requiredQuota; } }
    private int _currentQuota;
    public int CurrentQuota { get { return _currentQuota; } }
    private int _randomRange => (int)(_requiredQuota * 0.1f);

    [SerializeField] private TMP_Text _quotaText;

    private StringBuilder _sb = new();


    //private void Start()
    //{
    //    GameManager.Instance.StageManager.CurrentStage.Subscribe(SetRequiredQuota);
    //    GameManager.Instance.StageManager.CurrentStage.Subscribe(RenewQuotaWhenStageChanged);
    //}

    private void OnDestroy()
    {
        GameManager.Instance.StageManager.CurrentStage.Unsubscribe(SetRequiredQuota);
        GameManager.Instance.StageManager.CurrentStage.Unsubscribe(RenewQuotaWhenStageChanged);
    }

    public void Init(StageManager stageManager)
    {
        _stageManager = stageManager;
        _stageManager.CurrentStage.Subscribe(SetRequiredQuota);
        _stageManager.CurrentStage.Subscribe(RenewQuotaWhenStageChanged);
    }

    private void RenewQuotaWhenStageChanged()
    {
        _sb.Clear();

        _sb.Append(_currentQuota);
        _sb.Append("$ / ");
        _sb.Append(_requiredQuota);
        _sb.Append("$");

        _quotaText.text = _sb.ToString();
    }

    private void SetRequiredQuota()
    {
        _requiredQuota = 5000 * GameManager.Instance.StageManager.CurrentStage.Value;
        int random = Random.Range(_requiredQuota - _randomRange, _requiredQuota + _randomRange);
        _requiredQuota = random;
    }

    public void AddCurrentQuota(int value)
    {
        _currentQuota += value;

        RenewQuotaWhenStageChanged();
    }
}
