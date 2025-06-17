using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuotaTextIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text _quotaText;

    private StringBuilder _sb = new();

    private void Start()
    {
        QuotaManager quotaManager = GameManager.Instance.QuotaManager;

        quotaManager.CurrentQuota.Subscribe(UpdateQuotaText);
        quotaManager.RequiredQuota.Subscribe(UpdateQuotaText);

        UpdateQuotaText();
    }

    private void OnDisable()
    {
        QuotaManager quotaManager = GameManager.Instance.QuotaManager;

        quotaManager.CurrentQuota.Unsubscribe(UpdateQuotaText);
        quotaManager.RequiredQuota.Unsubscribe(UpdateQuotaText);
    }

    private void UpdateQuotaText()
    {
        QuotaManager quotaManager = GameManager.Instance.QuotaManager;

        _sb.Clear();
        _sb.Append(quotaManager.CurrentQuota.Value);
        _sb.Append("$ / ");
        _sb.Append(quotaManager.RequiredQuota.Value);
        _sb.Append("$");

        _quotaText.text = _sb.ToString();
    }
}
