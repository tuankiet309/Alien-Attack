namespace Preb.Enemy.Boss
{
    using UnityEngine;
    using Enemy = global::Enemy;

    public class BaseEnemyBoss : Enemy
    {
        [SerializeField] private float     armorValue       = 100f; // Giá trị giáp của boss
        [SerializeField] private float     weakenedDuration = 5f; // Thời gian suy yếu khi giáp bị phá
        [SerializeField] private HealthBar healthBarUI;
        private                  bool      isWeakened    = false; // Trạng thái suy yếu của boss
        private                  float     weakenedTimer = 0f; // Bộ đếm thời gian suy yếu

        protected override void Start()
        {
            base.Start(); // Gọi Start từ Enemy
        }

        private void Update()
        {
            base.Update(); // Gọi Update từ Enemy

            if (isWeakened)
            {
                HandleWeakenedState();
            }
        }

        public override void AttackTarget(GameObject target)
        {
            // Thực hiện hành động tấn công đặc biệt của Boss1 tại đây
            Debug.Log("Boss1 is attacking the target!");
            // Ví dụ: Thực hiện tấn công hoặc sử dụng kỹ năng đặc biệt
        }

        protected override void TargetChange(GameObject target, bool sense)
        {
            base.TargetChange(target, sense);

            if (!sense) return;

            // Hiển thị thanh máu khi phát hiện người chơi
            if (this.healthBarUI != null)
            {
                this.healthBarUI.gameObject.SetActive(true);
            }
        }

        protected override void TakenDamage(float health, float delta, float maxHealth, GameObject instigator)
        {
            // Hiển thị thanh máu khi nhận sát thương
            if (healthBarUI != null)
            {
                healthBarUI.gameObject.SetActive(true);
            }
            // Thực hiện giảm giáp thay vì sức khỏe nếu boss còn giáp
            if (armorValue > 0)
            {
                armorValue -= delta;
                Debug.Log("Boss1 took damage to armor. Armor left: " + armorValue);

                if (armorValue <= 0)
                {
                    EnterWeakenedState();
                }
            }
            else
            {
                // Nếu giáp đã hết, giảm trực tiếp sức khỏe
                base.TakenDamage(health, delta, maxHealth, instigator);
            }
        }

        private void EnterWeakenedState()
        {
            Debug.Log("Boss1 entered the weakened state.");
            isWeakened    = true;
            weakenedTimer = weakenedDuration;

            // Ngừng di chuyển và tấn công khi vào trạng thái suy yếu
            //movement_Component.StopMovement();
            // Animator.SetBool("isWeakened", true);
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
            Debug.Log("Boss1 exited the weakened state.");
            isWeakened = false;

            // Phục hồi hành vi của boss sau khi hết suy yếu
            Animator.SetBool("isWeakened", false);
            armorValue = 100f; // Đặt lại giáp hoặc giữ giáp ở mức 0
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
}