using Preb.InGameUI.Health;
using Preb.Over_All.Health_Relate;
using UnityEngine;

public class BaseEnemyBoss : Enemy
{
    [SerializeField] TriggerDamageComponent dc;
    private          ExtendHealthComponent  healthComponent;
    private          HealthBarWithArmor     healthBarUI;
    
    
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("attack");
    }
    public void AttackPoint()
    {
        if (dc)
        {
            dc.SetDamageEnable(true);
            Debug.Log("I attack");
        }
    }
    public void AttackEnd()
    {
        if (dc)
        {
            dc.SetDamageEnable(false);
            Debug.Log("I stop");
        }
    }
    protected override void Start()
    {
        base.Start();
        healthComponent = GetComponent<ExtendHealthComponent>();
        dc.SetTeamInterface(this);
    }

    protected override void Dead()
    {
        base.Dead();
        Debug.Log("Boss has died.");
        
        // Ẩn thanh máu khi boss chết
        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(false);
        }
    }
}