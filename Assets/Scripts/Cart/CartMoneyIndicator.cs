using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CartMoneyIndicator : MonoBehaviour
{
    [SerializeField] private LayerMask _propLayer;
    [SerializeField] TMP_Text _sumText;


    private List<PropInfos> _propInfos = new();
    public StringBuilder _sb = new();

    private void Update()
    {
        _sb.Clear();

        int sum = 0;

        foreach (PropInfos propInfo in _propInfos)
        {
            sum += propInfo.CurrentPrice;
        }

        _sb.Append("$ ");
        _sb.Append(sum);

        _sumText.text = _sb.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_propLayer.value & (1 << other.gameObject.layer)) == 0) return;

        PropInfos prop = other.gameObject.GetComponent<PropInfos>();
        if (prop == null) return;

        _propInfos.Add(prop);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_propLayer.value & (1 << other.gameObject.layer)) == 0) return;

        PropInfos prop = other.gameObject.GetComponent<PropInfos>();
        if (prop == null) return;

        _propInfos.Remove(prop);
    }

    public void DeletePropListWhenDestroyed(PropInfos prop)
    {
        _propInfos.Remove(prop);
    }
}
