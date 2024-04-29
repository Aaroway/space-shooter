using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;
    public static CameraShake Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Camera is null");
            }
            return _instance;
        }
    }
    public Transform cameraTransform = default;
    private Vector3 _originalPosition = default;
    public float shakeFrequncy = default;
    public bool cameraShake = false;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _originalPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraShake == true)
        {
            CameraShakes();
        }
        else
        {
            StopShake();
        }
    }
    public void StartShaking()
    {
        if (!cameraShake)
        {
            cameraShake = true;
            StartCoroutine(StopAfterDuration());
        }
    }
    private IEnumerator StopAfterDuration()
    {
        yield return new WaitForSeconds(1f);
        cameraShake = false;
    }
    public void CameraShakes()
    {
        cameraTransform.position = _originalPosition + Random.insideUnitSphere * shakeFrequncy;
    }
    public void StopShake()
    {
        cameraTransform.position = _originalPosition;
    }
}
