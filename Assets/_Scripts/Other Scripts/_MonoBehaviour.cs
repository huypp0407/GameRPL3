using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.ResetValue();
        this.LoadComponent();
    }

    protected virtual void Reset()
    {
        this.LoadComponent();
        this.ResetValue();
    }

    protected virtual void Start()
    {
        //Override
    }

    protected virtual void LoadComponent()
    {
        //Override
    }
    protected virtual void ResetValue()
    {
        //Override
    }
}
