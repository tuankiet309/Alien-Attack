using System.Threading.Tasks;
using Preb.Over_All.Health_Relate;
using UnityEngine;

public class BaseBossBehavior : Behavior_Tree
{
    [SerializeField] protected float acceptableDistance = 2.5f;
    [SerializeField] protected float acceptableRadius   = 10f;
    [SerializeField] protected float coolDown           = 1f;

    [SerializeField] private float weakenedDuration = 5f; // Thời gian suy yếu khi giáp bị phá
    private                  bool  isWeakened       = false;
    private                  float weakenedTimer    = 0f;
    private                  bool  canUseSkill      = true;

    private ExtendHealthComponent healthComponent;

    private void Start()
    {
        base.Start();
        healthComponent = GetComponent<ExtendHealthComponent>();

        if (healthComponent != null)
        {
            healthComponent.onArmorBroken += EnterWeakenedState;
        }
    }

    public bool IsArmorBroken()
    {
        return this.healthComponent.GetArmorValue() <= 0;
    }

    protected override void ConstructTree(out BT_Node root_Node)
    {
        Selector root_Selector = new Selector();

        
        // Evade (Placeholder, can be customized by child bosses)
        BTTask_Evade evadeTask = new BTTask_Evade(this);
        root_Selector.AddChild(evadeTask);
        
        // Attack Target
        BTTask_AttackTargetGroup attackTargetGroup = new BTTask_AttackTargetGroup(this, acceptableDistance, acceptableRadius, coolDown);
        root_Selector.AddChild(attackTargetGroup);

        // Weakened State
        BTTask_Weakened weakenedTask = new BTTask_Weakened(this);
        root_Selector.AddChild(weakenedTask);

        // Patrolling
        BTTask_PatrollingGroup patrolTask = new BTTask_PatrollingGroup(this);
        root_Selector.AddChild(patrolTask);

        root_Node = root_Selector;
    }

    private void Update()
    {
        base.Update();
        if (isWeakened)
        {
            HandleWeakenedState();
        }
    }

    public void EnterWeakenedState()
    {
        if (!isWeakened)
        {
            Debug.Log("Boss entered weakened state.");
            isWeakened    = true;
            weakenedTimer = weakenedDuration;

            // Tắt di chuyển và tấn công khi bị suy yếu
            //animator.SetBool("isWeakened", true);
        }
    }

    private void HandleWeakenedState()
    {
        weakenedTimer -= Time.deltaTime;

        if (weakenedTimer <= 0)
        {
            ExitWeakenedState();
        }
    }

    private void ExitWeakenedState()
    {
        Debug.Log("Boss exited weakened state.");
        isWeakened = false;
        
        this.healthComponent.SetArmorValue(this.healthComponent.GetMaxArmorValue());
        // Phục hồi hành vi boss sau khi hết suy yếu
        //animator.SetBool("isWeakened", false);
    }

    public bool IsWeakenComplete()
    {
        return !this.isWeakened;
    }
    
    public bool ShouldEvade()
    {
        // Kiểm tra nếu HP thấp hoặc điều kiện khác để boss né tránh (cơ chế ...)
        return healthComponent.GetHealth <= 0.3f * healthComponent.GetMaxHealth;
    }
    
    public void StartEvade()
    {
        
    }

    public virtual bool IsEvadeComplete()
    {
        //tùy chỉnh từng boss
        return true;
    }

    public async void CastSkill()
    {
        this.canUseSkill = false;
        await Task.Delay((int)(this.coolDown * 1000));
        // Re-enable the skill after cooldown is complete
        this.canUseSkill = true;
    }

    public bool CanCastSkill()
    {
        return this.canUseSkill;
    }
}