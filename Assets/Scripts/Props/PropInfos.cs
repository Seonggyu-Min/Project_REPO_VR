using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInfos : MonoBehaviour
{
    [SerializeField] private bool _isSmall;

    private int _maxPrice;
    private int _currentPrice;

    [SerializeField] private CartMoneyIndicator _cartMoneyIndicator;

    public int MaxPrice => _maxPrice;
    public int CurrentPrice => _currentPrice;

    private void OnEnable()
    {
        SetPrice();
        PropPriceRegisterer.Register(this);
    }
    private void OnDestroy()
    {
        _cartMoneyIndicator.DeletePropListWhenDestroyed(this);
        PropPriceRegisterer.Unregister(this);
    }

    private void SetPrice()
    {
        if (_isSmall)
        {
            int randomPrice = Random.Range(50, 500);
            _maxPrice = randomPrice;
        }
        else
        {
            int randomPrice = Random.Range(1000, 10000);
            _maxPrice = randomPrice;
        }

        _currentPrice = _maxPrice;
    }

    public void ReductPrice()
    {
        //_currentPrice -= (int)(_maxPrice * 0.1f);

        _currentPrice = Mathf.Max(0, _currentPrice - (int)(_maxPrice * 0.1f));

        if (_currentPrice <= 0)
        {
            // TODO: 오브젝트 풀링
            Destroy(gameObject);
        }
    }
}
