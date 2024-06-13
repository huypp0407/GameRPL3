using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyMovementAbstract : _MonoBehaviour
{
    public Transform target;
    public bool isWalk = true;
    public bool isUpdatePath = true;
    public EnemyCtrl enemyCtrl;

    protected override void Start()
    {
        target = PlayerCtrl.Instance.transform;
    }
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected float minDistance = 7f;
    [SerializeField] protected float speed = 2f;

    public bool snapping;
    protected virtual void OnEnable() {
        this.minDistance = 7f;
        // this.enemyCtrl.Animator.SetFloat("walk", 1);
        this.speed = 3;
        this.isWalk = true;
        target = PlayerCtrl.Instance.transform;
        StartCoroutine (UpdatePath ());
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
    const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	PathOther path;

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new PathOther(waypoints, transform.parent.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds (.3f);
		}
		PathRequestManager.RequestPath (new PathRequest(transform.parent.position, this.target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
      if (!isUpdatePath) {
        StopCoroutine("FollowPath");
        yield return null;
      }
			else if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (new PathRequest(transform.parent.position, target.position, OnPathFound));
				targetPosOld = target.position;
			}
		}
	}

  protected virtual void DoSomething() {
    
  }
	IEnumerator FollowPath() {
		bool followingPath = true;
		int pathIndex = 0;
		transform.parent.LookAt (path.lookPoints [0]);
    
		float speedPercent = 1;
    
		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.parent.position.x, transform.parent.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.parent.position);
				transform.parent.rotation = Quaternion.Lerp (transform.parent.rotation, targetRotation, Time.deltaTime * turnSpeed);
				// transform.parent.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;

		}
	}
  public void OnDrawGizmos() {
    if (path != null) {
      path.DrawWithGizmos ();
    }
  }
}
