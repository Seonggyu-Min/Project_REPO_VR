using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentQuotaZeroManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.QuotaManager.SetCurrentQuotaToZero();
    }
}
