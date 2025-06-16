using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openSpeed = 30f;

    private Coroutine _doorOpenCoroutine;

    [ContextMenu("OpenDoor")]
    public void Open()
    {
        if (_doorOpenCoroutine != null)
            StopCoroutine(_doorOpenCoroutine);

        _doorOpenCoroutine = StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float currentAngle = GetHingeYAngle();
        float targetAngle = 0f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 10f)
        {
            float direction = Mathf.Sign(Mathf.DeltaAngle(currentAngle, targetAngle));
            float rotateAmount = openSpeed * Time.deltaTime * direction;

            transform.Rotate(0f, rotateAmount, 0f);
            currentAngle = GetHingeYAngle();
            yield return null;
        }

        Vector3 rot = transform.eulerAngles;
        rot.y = 0f;
        transform.eulerAngles = rot;

        _doorOpenCoroutine = null;
    }

    private float GetHingeYAngle()
    {
        float angle = transform.localEulerAngles.y;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
