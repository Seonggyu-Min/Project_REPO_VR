using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollideController : MonoBehaviour
{
    [SerializeField] private LayerMask _cartLayer;

    [SerializeField] private float _collideDetectStartTime;
    private float _collideDetectTimer;
    private bool _canCollide; // �ʱ⿡ prop�� �������鼭 �����Ǳ⿡ �÷��� �߰�

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

        // īƮ�� ������ �浹�� ���� ���� ���� ����
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
