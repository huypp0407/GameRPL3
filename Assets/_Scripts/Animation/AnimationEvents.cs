using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action<string> OnCustomEvent = s => { };

    public void CustomEvent(string eventName)
    {
        OnCustomEvent(eventName);
    }
}
