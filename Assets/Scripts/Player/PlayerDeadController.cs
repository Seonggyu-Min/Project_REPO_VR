using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerDeadController : MonoBehaviour
{
    [SerializeField] private DynamicMoveProvider moveProvider;
    [SerializeField] private XRRayInteractor leftRay;
    [SerializeField] private XRRayInteractor rightRay;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform cameraTransform;

    private void OnEnable()
    {
        moveProvider.enabled = true;
        leftRay.enabled = true;
        rightRay.enabled = true;
    }

    public void TriggerDeathSequence()
    {
        moveProvider.enabled = false;
        leftRay.enabled = false;
        rightRay.enabled = false;

        StartCoroutine(TiltCamera());
    }

    private IEnumerator TiltCamera()
    {
        float duration = 3.0f;
        float elapsed = 0f;

        Quaternion startRot = cameraRoot.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(90f, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cameraRoot.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
            yield return null;
        }

        GameOver();
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");
        GameManager.Instance.StageManager.GoToGameOverScene();
    }
}
