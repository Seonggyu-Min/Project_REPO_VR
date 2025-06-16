using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInfos : MonoBehaviour
{
    [SerializeField] private bool _isSmall;
    
    // 디버그용 직렬화, 추후 삭제
    [SerializeField] private int _maxPrice;
    [SerializeField] private int _currentPrice;

    public int MaxPrice => _maxPrice;
    public int CurrentPrice => _currentPrice;

    private void OnEnable()
    {
        SetPrice();
        PropPriceRegisterer.Register(this);
    }
    private void OnDestroy()
    {
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
