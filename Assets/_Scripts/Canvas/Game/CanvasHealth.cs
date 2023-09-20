using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHealth : _MonoBehaviour
{
    [SerializeField] protected PlayerDame dame;
    public PlayerDame Dame => dame;


    private void LateUpdate()
    {
        transform.LookAt(transform.position + GameCtrl.Instance.MainCamera.transform.forward);
    }
}