using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : _MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get => instance; }

    [SerializeField] protected int levelCurrent;
    public int score;
    [SerializeField] protected float hp;
    [SerializeField] protected float hpMax;
    [SerializeField] protected Vector3 playerPos;

    protected override void Awake()
    {
        base.Awake();
        if (Player.instance != null) return;
        Player.instance = this;
    }

    public virtual void LoadData()
    {
        this.levelCurrent = MapLevel.Instance.LevelCurrent;
        this.score = TextScore.Instance.Score - 1;
        this.hp = PlayerCtrl.Instance.PlayerDamageReceiver.Hp;
        this.hpMax = PlayerCtrl.Instance.PlayerDamageReceiver.HpMax;
        this.playerPos = transform.position;
    }

    public virtual void SetData(PlayerData playerData)
    {
        MapLevel.Instance.SetCurrentLevel(playerData.levelCurrent);
        TextScore.Instance.SetScore(playerData.score);
        PlayerCtrl.Instance.PlayerDamageReceiver.SetHP(playerData.hp, playerData.hpMax);
        transform.parent.position = playerData.playerPos;
    }
}
