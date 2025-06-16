using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellBoxBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _propsLayer;
    [SerializeField] private Collider _triggerZone;
    [SerializeField] SellButtonBehaviour _sellButton;

    [SerializeField] private GameObject _pushObj;

    private int sum;
    private Coroutine _sellCoroutine;
    private List<PropInfos> props;

    private void Awake()
    {
        _triggerZone = GetComponent<Collider>();
    }

    private void Start()
    {
        _sellButton.OnSellButtonSelectedAction += SellProps;
    }

    private void OnDisable()
    {
        _sellButton.OnSellButtonSelectedAction -= SellProps;
    }

    private void SellProps()
    {
        Bounds bound = _triggerZone.bounds;

        props = PropPriceRegisterer.GetPropsInBounds(bound);
        sum = 0;

        foreach (PropInfos info in props)
        {
            sum += info.CurrentPrice;
        }

        _sellCoroutine = StartCoroutine(PushRoutine());
    }

    private IEnumerator PushRoutine()
    {
        while (_pushObj.transform.position.y > 3f)
        {
            _pushObj.transform.position += Vector3.down * Time.deltaTime * 5f;
            yield return null;
        }

        _sellCoroutine = StartCoroutine(PullRoutine());
    }

    private IEnumerator PullRoutine()
    {
        foreach (PropInfos info in props)
        {
            Destroy(info.gameObject);
        }

        GameManager.Instance.QuotaManager.AddCurrentQuota(sum);

        while (_pushObj.transform.position.y < 9f)
        {
            _pushObj.transform.position += Vector3.up * Time.deltaTime * 5f;
            yield return null;
        }
    }
}
