using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Enemy : MonoBehaviour,BehaviorTreeInterface,ITeamInterface,ISpawnInterface
{
    [SerializeField]HealthComponent healthComponent; //Thành phần Health
    [SerializeField]Animator animator; //Animator
    [SerializeField]Perception_Component perceeption_Component; //Thành phần tri giác
    [SerializeField]Behavior_Tree behavior; //Cây hành vi
    [SerializeField]MovementComponent movement_Component;
    [SerializeField]RewardComponent reward_Component;

    [SerializeField] int TeamID = 2;
    Vector3 preLocation;
    public int GetTeamID()
    {
        return (int)TeamID;
    }

    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    private void Awake()
    {
        perceeption_Component = GetComponent<Perception_Component>();
        perceeption_Component.onPerceptionTargetChanged += TargetChange; //Gàn hàm targetChange để gọi mỗi khi có thay đổi về hành target
                                                                         //Để ở awake nó có thể được spawn bởi spawner trước khi hàm start hoạt 
    }
    protected virtual void Start()
    {
        
        if(healthComponent != null ) //nếu có
        {
            healthComponent.onHealthEmpty += StartDeath; //Gọi hàm StartDeath mỗi khi event onHealthEmpty đc invoke
            healthComponent.onTakeDamamge += TakenDamage; //Gọi hàm TakenDamage mỗi khi event onTakeDamamge đc invoke

        }

    }


    private void TargetChange(GameObject target, bool sense)
    {
        if(sense)
        {
            behavior.Board.SetOrAddData("Target", target.gameObject); 
        }
        else
        {
            behavior.Board.SetOrAddData("LastSeenLocation",target.transform.position);
            behavior.Board.RemoveData("Target");
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator) 
    {
        
    }

    private void StartDeath(GameObject killer)
    {
        TriggerDeathAnimation(); //bắt đầu animaton chết
    }
    public void OnDeathAnimationFinished() //Hủy gameObject khi chết// Gọi bằng animation event của animation dead
    {
        Dead();
        reward_Component.OnDeathReward();
        //Need coroutine to make sure some logic apply on Dead() can run before destroy;
        StartCoroutine(StartDestroy());
    }

    IEnumerator StartDestroy()
    {

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
    
        Destroy(gameObject);
    }
    protected virtual void Dead()
    {
    }

    private void TriggerDeathAnimation() 
    {
        if (animator != null)
        {
            animator.SetTrigger("isDead");//trigger để bắt đầu animation dead
        }
    }
    // Update is called once per frame
    protected void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        if (movement_Component==null)
        {
            return;
        }
        Vector3 posDelta = transform.position - preLocation;
        float speed = posDelta.magnitude / Time.deltaTime;
        Animator.SetFloat("speed", speed);
        preLocation = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(behavior && behavior.Board.GetData("Target", out GameObject target))
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(drawTargetPos, 1f);
            Gizmos.DrawLine(transform.position + Vector3.up, target.transform.position);
        }
    }

    public void RotateToward(GameObject target, bool verticleAim = false)
    {
        Vector3 aimDir = target.transform.position - transform.position;
        aimDir.y = verticleAim ? aimDir.y : 0;
        aimDir = aimDir.normalized;
        movement_Component.RotateTowards(aimDir);
    }



    public virtual void AttackTarget(GameObject target)
    {
    }

    public void SpawnBy(GameObject spawnerGameobject)
    {
        Behavior_Tree spawnerBehaviorTree = spawnerGameobject.GetComponent<Behavior_Tree>();

        if (spawnerBehaviorTree != null && spawnerBehaviorTree.Board.GetData<GameObject>("Target", out GameObject spawnerTarget))
        {
            Debug.Log(spawnerTarget + "++++++++++++++++++++++++++++++");
            Perception_Stimuli targetStimuli = spawnerTarget.GetComponent<Perception_Stimuli>();
            if(perceeption_Component && targetStimuli)
            {
                perceeption_Component.AssignPercivedStimuli(targetStimuli);
            }
        }

    }
}
