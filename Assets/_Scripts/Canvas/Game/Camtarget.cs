using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camtarget : _MonoBehaviour
{
    [SerializeField] protected Camera mainCamera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
