using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class GrabPriceIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    private StringBuilder _sb = new();


    public void RenewPrice(int value)
    {
        _sb.Clear();
        _sb.Append("$ ");
        _sb.Append($"{value}");

        _priceText.text = _sb.ToString();
    }
}
