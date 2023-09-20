using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : EnemyMovementAbstract
{
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected float minDistance = 7f;
    [SerializeField] protected float speed = 2f;

    public float jumpPowew = 1;
    public int numJump = 1;
    public float duration = 1;
    public bool snapping;

    private void OnEnable()
    {
        this.minDistance = 7f;
        this.enemyCtrl.Animator.SetFloat("walk", 1);
        this.speed = 3;
        this.isWalk = true;
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    protected virtual void FixedUpdate()
    {
        this.Move();
    }

    protected virtual void Move()
    {
        if (!isWalk) return;
        this.distance = Vector3.Distance(transform.parent.position, this.target.position);
       
        if (this.distance < this.minDistance)
        {
            StartCoroutine(Jump());
        }

        if (this.distance < 1f)
        {
            this.enemyCtrl.Animator.SetFloat("walk", 0);
            this.enemyCtrl.EnemyShooting.isAttack = true;
            return;
        }
        
        this.enemyCtrl.Animator.SetFloat("walk", 1);
        this.enemyCtrl.EnemyShooting.isAttack = false;

        Vector3 direction = (this.target.position - transform.position).normalized;
        this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(direction.x, this.enemyCtrl.Rigibody.velocity.y, direction.z) * speed, Time.fixedDeltaTime * 12);
    }

    IEnumerator Jump()
    {
        this.minDistance = 0;
        this.enemyCtrl.Animator.SetFloat("walk", 0);
        Transform fx = FXSpawner.Instance.SpawnFx("Impact_4", this.target.position, transform.rotation);
        this.isWalk = false;
        this.enemyCtrl.EnemyLookAtTarget.target = fx;
        yield return new WaitForSeconds(1.5f);
        FXSpawner.Instance.Despawn(fx);
        this.enemyCtrl.Animator.SetTrigger("Jump");
        if(transform.parent != null) transform.parent.DOJump(fx.transform.position, 1, 1, .5f, snapping);
        yield return new WaitForSeconds(1f);
        this.enemyCtrl.EnemyLookAtTarget.target = PlayerCtrl.Instance.transform;
        this.speed = 4;
        this.isWalk = true;
        this.enemyCtrl.Animator.SetFloat("speed", 2);

    }

}
