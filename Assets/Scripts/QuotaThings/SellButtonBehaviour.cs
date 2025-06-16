using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButtonBehaviour : MonoBehaviour
{
    [SerializeField] private SellBoxBehaviour _sellBox;

    public event Action OnSellButtonSelectedAction;

    public void OnSellButtonSelected()
    {
        OnSellButtonSelectedAction?.Invoke();
    }
}
