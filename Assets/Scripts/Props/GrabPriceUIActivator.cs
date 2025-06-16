using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabPriceUIActivator : MonoBehaviour
{
    private XRGrabInteractable _xrGrabInteractable;
    private Transform _currentInteractor;
    private PropInfos _propInfos;

    private GameObject _rightPriceUIObj;
    private GrabPriceIndicator _rightPriceUI;
    private GameObject _leftPriceUIObj;
    private GrabPriceIndicator _leftPriceUI;

    private GrabPriceIndicator _currentActiveUI;

    private bool _isGrabbing;

    private void Awake()
    {
        _propInfos = GetComponent<PropInfos>();
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        _xrGrabInteractable.selectEntered.AddListener(OnGrab);
        _xrGrabInteractable.selectExited.AddListener(OnRelease);
    }

    private void Update()
    {
        if (_isGrabbing && _propInfos != null && _currentActiveUI != null)
        {
            _currentActiveUI.RenewPrice(_propInfos.CurrentPrice);
        }
        else if(_propInfos == null)
        {
            Debug.LogWarning($"_propInfos°¡ null");
        }
    }
    private void OnDestroy()
    {
        _xrGrabInteractable.selectEntered.RemoveListener(OnGrab);
        _xrGrabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        _isGrabbing = true;

        _currentInteractor = args.interactorObject.transform;

        if (_currentInteractor.name.Contains("Left"))
        {
            _currentActiveUI = _leftPriceUI;
            _leftPriceUIObj.SetActive(true);
        }
        else
        {
            _currentActiveUI = _rightPriceUI;
            _rightPriceUIObj.SetActive(true);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (_currentInteractor == null) return;

        _isGrabbing = false;

        if (_currentActiveUI != null)
        {
            _currentActiveUI.gameObject.SetActive(false);
            _currentActiveUI = null;
        }

        _currentInteractor = null;
    }

    public void ConnectUI(GameObject rightUIObj, GameObject leftUIObj)
    {
        _rightPriceUIObj = rightUIObj;
        _rightPriceUI = _rightPriceUIObj.GetComponent<GrabPriceIndicator>();

        _leftPriceUIObj = leftUIObj;
        _leftPriceUI = _leftPriceUIObj.GetComponent<GrabPriceIndicator>();
    }
}
