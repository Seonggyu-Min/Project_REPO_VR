using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollideController : MonoBehaviour
{
    [SerializeField] private LayerMask _cartLayer;

    [SerializeField] private float _collideDetectStartTime;
    private float _collideDetectTimer;
    private bool _canCollide; // 초기에 prop이 떨어지면서 스폰되기에 플래그 추가

    private PropInfos _propInfos;


    private void Awake() => Init();
    private void Update() => HandleDetectTime();
    private void OnCollisionEnter(Collision collision) => HandleCollision(collision);


    private void Init()
    {
        _propInfos = GetComponent<PropInfos>();

        _collideDetectTimer = 0f;
        _canCollide = false;
    }

    private void HandleCollision(Collision collision)
    {
        if (!_canCollide) return;

        // 카트와 프롭의 충돌은 가격 감소 적용 안함
        if (((1 << collision.gameObject.layer) & _cartLayer) != 0) return;

        _propInfos.ReductPrice();
    }

    private void HandleDetectTime()
    {
        if (_collideDetectTimer >= _collideDetectStartTime)
        {
            _canCollide = true;
            return;
        }

        _collideDetectTimer += Time.deltaTime;
    }
}
