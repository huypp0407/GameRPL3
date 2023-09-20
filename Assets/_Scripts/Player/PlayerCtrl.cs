using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : _MonoBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerFootStep playerFootStep;
    public PlayerMove playerMove;

    [SerializeField] protected PlayerDamageReceiver playerDamageReceiver;
    public PlayerDamageReceiver PlayerDamageReceiver => playerDamageReceiver;

    [SerializeField] protected PlayerShooting playerShooting;

    [SerializeField] protected AbilityCtrl abilityCtrl;
    public AbilityCtrl AbilityCtrl => abilityCtrl;

    [SerializeField] protected Inventory inventory;
    public Inventory Inventory => inventory;

    [SerializeField] protected PlayerSO playerSO;
    public PlayerSO PlayerSO => playerSO;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected AnimationEvents animationEvent;
    public AnimationEvents AnimationEvent => animationEvent;

    [SerializeField] protected SkinnedMeshRenderer meshCharacter;
    public SkinnedMeshRenderer MeshCharacter => meshCharacter;

    [SerializeField] protected Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    public GameObject weaponPos;

    public GameObject handPos;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerCtrl.instance == null) PlayerCtrl.instance = this; ;

        this.SetUp();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerDamageReceiver();
        this.LoadPlayerShooting();
        this.LoadAbilities();
        this.LoadInventory();
        this.LoadPlayerSO();
        this.LoadAnimationEvent();
        this.LoadMeshCharacter();
        this.LoadAnimator();
        this.LoadPlayerFootStep();
        this.LoadPlayerMove();
        this.LoadRigidbody();
    }

    void SetUp()
    {
        this.playerFootStep.playerCtrl = this;
        this.playerMove.playerCtrl = this;
        this.playerDamageReceiver.playerCtrl = this;
        this.playerShooting.playerCtrl = this;
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void LoadPlayerFootStep()
    {
        if (this.playerFootStep != null) return;
        this.playerFootStep = GetComponentInChildren<PlayerFootStep>();
    }

    protected virtual void LoadPlayerShooting()
    {
        if (this.playerShooting != null) return;
        this.playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    protected virtual void LoadPlayerMove()
    {
        if (this.playerMove != null) return;
        this.playerMove = GetComponentInChildren<PlayerMove>();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
    }

    protected virtual void LoadMeshCharacter()
    {
        if (this.meshCharacter != null) return;
        this.meshCharacter = GameObject.Find("CharacterSkin").GetComponentInChildren<SkinnedMeshRenderer>();
    }

    protected virtual void LoadPlayerDamageReceiver()
    {
        if (this.playerDamageReceiver != null) return;
        this.playerDamageReceiver = GetComponentInChildren<PlayerDamageReceiver>();
    }

    protected virtual void LoadAnimationEvent()
    {
        if (this.animationEvent != null) return;
        this.animationEvent = GetComponentInChildren<AnimationEvents>();
    }

    protected virtual void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        string resPath = "Player/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(resPath);
    }

    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = GetComponentInChildren<Inventory>();
    }

    protected virtual void LoadAbilities()
    {
        if (this.abilityCtrl != null) return;
        this.abilityCtrl = GetComponentInChildren<AbilityCtrl>();
    }

    protected override void Start()
    {
        string character = PlayerPrefs.GetString("Character");
        ItemProfileSO item = ItemProfileSO.FindByItemName(character);
        this.meshCharacter.sharedMesh = item.mesh;
    }
}
