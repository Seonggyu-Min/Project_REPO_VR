using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnPointRegistrant : MonoBehaviour
{
    private PropsGenerator _propsGenerator;

    [SerializeField] private List<Transform> _smallSpawnPoints = new();
    [SerializeField] private List<Transform> _largeSpawnPoints = new();

    private void Awake()
    {
        _propsGenerator = FindObjectOfType<PropsGenerator>();
    }

    private void Start()
    {
        TryRegisterSpawnList();
    }

    private void TryRegisterSpawnList()
    {
        if (_propsGenerator != null)
        {
            _propsGenerator.RegisterSpawnPoints(_smallSpawnPoints, _largeSpawnPoints);
        }
        else
        {
            Debug.LogWarning("_propsGenerator 가 null, 등록 실패");
        }
    }
}
