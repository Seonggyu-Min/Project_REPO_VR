using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _ghostLayerMask;
    [SerializeField] private Transform _eyeRayTransform;

    [SerializeField] private float _eyeRayDistance;
    private float _eyeRayTimer;
    [SerializeField] private float _eyeRayTimerMax;

    private Volume _postProcessVolume;
    private Vignette _vignette;

    private bool _isDead;

    private void Awake()
    {
        _postProcessVolume = GetComponentInChildren<Volume>();
        if (_postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            _vignette = vignette;
        }

        _isDead = false;
    }

    private void Update()
    {
        Debug.DrawRay(_eyeRayTransform.position, _eyeRayTransform.forward * _eyeRayDistance, Color.red);


        if (Physics.Raycast(_eyeRayTransform.position, _eyeRayTransform.forward, out RaycastHit hit, _eyeRayDistance, _ghostLayerMask))
        {
            Debug.Log($"타이머 상승 중{_eyeRayTimer}");

            _eyeRayTimer += Time.deltaTime;

            if (_eyeRayTimer >= _eyeRayTimerMax)
            {
                // 고스트의 상태 추적으로 변경

                BaseMonster ghost = hit.collider.GetComponent<BaseMonster>();
                if (ghost != null && !(ghost.CurrentState is TraceState))
                {
                    ghost.StateMachine.ChangeState(new TraceState(ghost));
                }
            }
        }
        else
        {
            _eyeRayTimer = Mathf.Max(0f, _eyeRayTimer - Time.deltaTime);
        }

        UpdateVignette();
        DoDeathProcess();
    }

    private void UpdateVignette()
    {
        float ratio = Mathf.Clamp01(_eyeRayTimer / _eyeRayTimerMax);
        _vignette.intensity.Override(Mathf.Lerp(0f, 0.6f, ratio));
    }

    public void Die()
    {
        Debug.Log("Player Dead");
        _isDead = true;
    }

    private void DoDeathProcess()
    {
        if (_isDead)
        {
            // 조종 불가
        }
    }
}
