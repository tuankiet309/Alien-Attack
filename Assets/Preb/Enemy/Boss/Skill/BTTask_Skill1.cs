namespace Preb.Over_All.AI_Relate.Behavior_Tree
{
    using UnityEngine;

    public class BTTask_Skill1 : BT_Node
    {
        private BaseBossBehavior boss;
        private GameObject       dropProjectilePrefab;
        private int              numberOfProjectiles;
        private float            spawnRadius;

        public BTTask_Skill1(BaseBossBehavior boss, GameObject dropProjectilePrefab, int numberOfProjectiles, float spawnRadius)
        {
            this.boss                 = boss;
            this.dropProjectilePrefab = dropProjectilePrefab;
            this.numberOfProjectiles  = numberOfProjectiles;
            this.spawnRadius          = spawnRadius;
        }

        protected override NodeResult Execute()
        {
            if (!this.boss.CanCastSkill())
            {
                return NodeResult.Failure;
            }

            this.boss.CastSkill();
            // Spawn multiple projectiles around the boss's position
            this.SpawnProjectiles();

            return NodeResult.Success; // Return success after spawning
        }

        private void SpawnProjectiles()
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // Calculate random position within the radius
                Vector3 randomPos = boss.transform.position + Random.insideUnitSphere * spawnRadius;
                randomPos.y = boss.transform.position.y+10; // Maintain the same Y height

                // Instantiate the drop projectile at the calculated position
                GameObject projectile    = Object.Instantiate(dropProjectilePrefab, randomPos, Quaternion.Euler(-90f, 0f, 0f));
                Projectile newProjectile = projectile.GetComponent<Projectile>();

                if (newProjectile != null)
                {
                    newProjectile.Launch(this.boss.gameObject,randomPos);
                }
            }
        }
    }
}