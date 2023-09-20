using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : _MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    const string EVENT_STEP_NAME = "stepdown";

    protected override void Awake()
    {
        base.Awake();
        this.LoadFootStepEventRegister();
    }

    protected virtual void LoadFootStepEventRegister()
    {
        this.playerCtrl.AnimationEvent.OnCustomEvent += this.OnCustomEventOfPlayer;
    }

    protected virtual void OnCustomEventOfPlayer(string eventName)
    {
        if (eventName != PlayerFootStep.EVENT_STEP_NAME) return;
        AudioClip audioClip = this.playerCtrl.PlayerSO.WalkStep();
        SoundSpawner.Instance.PlayEffect(audioClip, transform.position, transform.rotation);
    }
}
