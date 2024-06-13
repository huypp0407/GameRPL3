using UnityEngine;

public class EnemyCtrl : _MonoBehaviour
{
    [Header("EnemyCtrl")]
    [SerializeField] protected EnemyLookAtTarget enemyLookTarget;
    public EnemyLookAtTarget EnemyLookAtTarget => enemyLookTarget;
    public EnemyMovementAbstract enemyMove;
    [SerializeField] protected EnemyDamageReceive enemyDamageReceiver;
    [SerializeField] protected EnemyFootStep enemyFootStep;
    [SerializeField] protected EnemyImpact enemyImpact;
    [SerializeField] protected Abilities abilities;

    [SerializeField] protected EnemyDamageSender enemyDamageSender;
    public EnemyDamageSender EnemyDamageSender => enemyDamageSender;

    [SerializeField] protected EnemyShootingCtrl enemyShooting;
    public EnemyShootingCtrl EnemyShooting => enemyShooting;

    [SerializeField] protected EnemySO enemySO;
    public EnemySO EnemySO => enemySO;

    [SerializeField] protected CanvasHealth canvasHealth;
    public CanvasHealth CanvasHealth => canvasHealth;

    [SerializeField] protected AnimationEvents animationEvent;
    public AnimationEvents AnimationEvent => animationEvent;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected SpawnPoints spawnPoints;
    public SpawnPoints SpawnPoints => spawnPoints;

    public EnemySpawner enemySpawner;
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected Rigidbody _rigibody;
    public Rigidbody Rigibody => _rigibody;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnemyLookAtTarget();
        this.LoadEnemyMovement();
        this.LoadEnemyDamageReceive();
        this.LoadEnemyImpact();
        this.LoadEnemyAbility();
        this.LoadEnemyDamageSender();
        this.LoadEnemyShooting();
        this.LoadEnemyFootStep();
        this.LoadEnemySO();
        this.LoadHealthBar();
        this.LoadAnimationEvent();
        this.LoadAnimator();
        this.LoadSpawnPoint();
        this.LoadCollider();
        this.LoadRigibody();
    }

    protected virtual void LoadCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigibody != null) return;
        this._rigibody = GetComponent<Rigidbody>();
    }

    public void SetUp()
    {
        if(this.enemyMove != null) this.enemyMove.enemyCtrl = this;
        this.enemyLookTarget.enemyCtrl = this;
        this.enemyDamageReceiver.enemyCtrl = this;
        this.enemyImpact.enemyCtrl = this;
        this.enemyDamageSender.enemyCtrl = this;
        if (this.enemyShooting != null) {
          this.enemyShooting.enemyCtrl = this;
        }
        if (this.enemyFootStep != null) this.enemyFootStep.enemyCtrl = this;
        if (this.abilities != null) this.abilities.enemyCtrl = this;
    }

    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints = GetComponentInChildren<SpawnPoints>();
    }

    protected virtual void LoadEnemyAbility()
    {
        if (this.abilities != null) return;
        this.abilities = GetComponentInChildren<Abilities>();
    }

    protected virtual void LoadEnemySO()
    {
        if (this.enemySO != null) return;
        string resPath = "Enemy/" + transform.name;
        this.enemySO = Resources.Load<EnemySO>(resPath);
    }

    protected virtual void LoadHealthBar()
    {
        if (this.canvasHealth != null) return;
        this.canvasHealth = GetComponentInChildren<CanvasHealth>();
    }

    protected virtual void LoadEnemyImpact()
    {
        if (this.enemyImpact != null) return;
        this.enemyImpact = GetComponentInChildren<EnemyImpact>();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
    }

    protected virtual void LoadAnimationEvent()
    {
        if (this.animationEvent != null) return;
        this.animationEvent = GetComponentInChildren<AnimationEvents>();
    }

    protected virtual void LoadEnemyFootStep()
    {
        if (this.enemyFootStep != null) return;
        this.enemyFootStep = GetComponentInChildren<EnemyFootStep>();
    }

    protected virtual void LoadEnemyLookAtTarget()
    {
        if (this.enemyLookTarget != null) return;
        this.enemyLookTarget = GetComponentInChildren<EnemyLookAtTarget>();
    }

    protected virtual void LoadEnemyDamageReceive()
    {
        if (this.enemyDamageReceiver != null) return;
        this.enemyDamageReceiver = GetComponentInChildren<EnemyDamageReceive>();
    }

    protected virtual void LoadEnemyMovement()
    {
        if (this.enemyMove != null) return;
        this.enemyMove = GetComponentInChildren<EnemyMovementAbstract>();
    }

    protected virtual void LoadEnemyDamageSender()
    {
        if (this.enemyDamageSender != null) return;
        this.enemyDamageSender = GetComponentInChildren<EnemyDamageSender>();
    }

    protected virtual void LoadEnemyShooting()
    {
        if (this.enemyShooting != null) return;
        this.enemyShooting = GetComponentInChildren<EnemyShootingCtrl>();
    }
}
