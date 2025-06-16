using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _smallPropPrefabs = new();
    [SerializeField] private List<GameObject> _largePropPrefabs = new();

    [SerializeField] private int _maxSmallPropsNum;
    private int _currentSmallPropsNum = 0;
    [SerializeField] private int _maxLargePropsNum;
    private int _currentLargePropsNum = 0;

    private List<Transform> _smallPropsSpawnPoints = new();
    private List<Transform> _largePropsSpawnPoints = new();

    [Header("UI Register")]
    [SerializeField] private GameObject _rightPriceUI;
    [SerializeField] private GameObject _leftPriceUI;

    private void Awake()
    {
        InitNum();
    }
    private void Start()
    {
        SpawnProps();
    }

    private void InitNum()
    {
        _currentSmallPropsNum = 0;
        _currentLargePropsNum = 0;
    }

    public void RegisterSpawnPoints(List<Transform> smallList, List<Transform> largeList)
    {
        _smallPropsSpawnPoints.AddRange(smallList);
        _largePropsSpawnPoints.AddRange(largeList);

        Debug.Log("RegisterSpawnPoints");
    }

    private void SpawnProps()
    {
        while (_currentSmallPropsNum < _maxSmallPropsNum)
        {
            Debug.Log($"_smallPropsSpawnPoints 수: {_smallPropsSpawnPoints.Count}");
            Debug.Log($"_largePropsSpawnPoints 수: {_largePropsSpawnPoints.Count}");

            if (_smallPropsSpawnPoints.Count <= 0)
            {
                Debug.LogWarning($"smallPropsSpawnPoints.Count가 고갈되었습니다.");
                return;
            }

            int spawnPointRandomIndex = UnityEngine.Random.Range(0, _smallPropsSpawnPoints.Count);
            int smallPropRandomIndex = UnityEngine.Random.Range(0, _smallPropPrefabs.Count);

            GameObject instance = Instantiate(_smallPropPrefabs[smallPropRandomIndex]);
            instance.transform.position = _smallPropsSpawnPoints[spawnPointRandomIndex].position;
            instance.transform.SetParent(transform);

            GrabPriceUIActivator grabPriceUIActivator = instance.GetComponent<GrabPriceUIActivator>();
            grabPriceUIActivator.ConnectUI(_rightPriceUI, _leftPriceUI);

            _currentSmallPropsNum++;

            _smallPropsSpawnPoints.Remove(_smallPropsSpawnPoints[spawnPointRandomIndex]);
        }

        while (_currentLargePropsNum <= _maxLargePropsNum)
        {
            if (_largePropsSpawnPoints.Count <= 0)
            {
                Debug.LogWarning($"largePropsSpawnPoints.Count가 고갈되었습니다.");
                return;
            }

            int spawnPointRandomIndex = UnityEngine.Random.Range(0, _largePropsSpawnPoints.Count);
            int largePropRandomIndex = UnityEngine.Random.Range(0, _largePropPrefabs.Count);

            GameObject instance = Instantiate(_largePropPrefabs[largePropRandomIndex]);
            instance.transform.position = _largePropsSpawnPoints[spawnPointRandomIndex].position;
            instance.transform.SetParent(transform);

            GrabPriceUIActivator grabPriceUIActivator = instance.GetComponent<GrabPriceUIActivator>();
            grabPriceUIActivator.ConnectUI(_rightPriceUI, _leftPriceUI);

            _currentLargePropsNum++;

            _largePropsSpawnPoints.Remove(_largePropsSpawnPoints[spawnPointRandomIndex]);
        }
    }
}
