namespace Preb.Over_All.AI_Relate.Behavior_Tree
{
    using UnityEngine;
    using Behavior_Tree = global::Behavior_Tree;

    public class BTTask_SkillGroup : BTTask_Group
    {
        float moveAcceptableDistance  = 2.5f;
        float rotateAcceptableRadious = 10f;
        float skillCoolDownTime;
        
        private GameObject dropProjectilePrefab;
        private int        numberOfProjectiles;
        private float      spawnRadius;

        public BTTask_SkillGroup(Behavior_Tree tree, GameObject dropProjectilePrefab,int numberOfProjectiles,float spawnRadius,float moveAcceptableDistance = 2.5f, float rotateAcceptableRadious = 10f, float skillCoolDownTime = 3f) : base(tree)
        {
            this.moveAcceptableDistance  = moveAcceptableDistance;
            this.rotateAcceptableRadious = rotateAcceptableRadious;
            this.skillCoolDownTime       = skillCoolDownTime;
            this.dropProjectilePrefab    = dropProjectilePrefab;
            this.numberOfProjectiles     = numberOfProjectiles;
            this.spawnRadius             = spawnRadius;
        }

        protected override void ConstructTree(out BT_Node root)
        {
            Sequencer skillSequencer = new Sequencer();
            
            BTTask_RotateTorwardTarget rotateTowardsTarget = new BTTask_RotateTorwardTarget(tree, "Target", rotateAcceptableRadious);
            BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(tree, "Target", moveAcceptableDistance);
            
            BTTask_Skill1 useSkillTask = new BTTask_Skill1(tree.GetComponent<BaseBossBehavior>(),this.dropProjectilePrefab,this.numberOfProjectiles,this.spawnRadius);
            
            CoolDownDecorator skillCoolDownDecorator = new CoolDownDecorator(tree, useSkillTask, skillCoolDownTime);
            
            skillSequencer.AddChild(moveToTarget);
            skillSequencer.AddChild(rotateTowardsTarget);
            skillSequencer.AddChild(skillCoolDownDecorator);
            
            BlackBoard_Decorator skillTargetDecorator = new BlackBoard_Decorator(tree, skillSequencer,
                "Target",
                BlackBoard_Decorator.Condiction.KeyExist,
                BlackBoard_Decorator.NotifyRules.RunCondictionChanged,
                BlackBoard_Decorator.NotifyAbort.Both);

            root = skillTargetDecorator;
        }
    }
}