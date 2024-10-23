namespace Preb.Enemy.Boss
{
    using Preb.Over_All.AI_Relate.Behavior_Tree;
    using UnityEngine;

    public class LeaderTropperBT: BaseBossBehavior
    {
        [SerializeField] private GameObject dropProjectilePrefab; // Assign your DropProjectile prefab in the inspector
        [SerializeField] private int        numberOfProjectiles = 5;     // Number of projectiles to spawn
        [SerializeField] private float      spawnRadius         = 10f;         // Radius within which projectiles will spawn

        protected override void ConstructTree(out BT_Node root_Node)
        {
            Selector root_Selector = new Selector();
            
            // Evade (Placeholder, can be customized by child bosses)
            BTTask_Evade evadeTask = new BTTask_Evade(this);
            root_Selector.AddChild(evadeTask);
            
            ///using skill
            BTTask_SkillGroup skill1Node = new BTTask_SkillGroup(this, dropProjectilePrefab, numberOfProjectiles, spawnRadius);
            root_Selector.AddChild(skill1Node);
        
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
    }
}