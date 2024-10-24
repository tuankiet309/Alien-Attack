namespace Preb.Over_All.Health_Relate
{
    using System;
    using Preb.InGameUI.Health;
    using UnityEngine;

    public class ExtendHealthUIComponent : MonoBehaviour
    {
        [SerializeField] private HealthBarWithArmor    HealthBarWithArmor;
        [SerializeField] private ExtendHealthComponent healthComponent;

        private void Start()
        {
            // Khi giáp hoặc máu thay đổi, cập nhật UI
            healthComponent.onTakeDamamge += (float health, float delta, float maxHealth, float armor, float maxArmor, GameObject instigator) =>
            {
                if (this.HealthBarWithArmor != null)
                {
                    this.HealthBarWithArmor.gameObject.SetActive(true);
                    HealthBarWithArmor.SetHealthAndArmor(health, delta,maxHealth, armor, maxArmor);
                }
            };

            // Khi hết máu, xử lý sự kiện OnOwnerDead trên UI
            //healthComponent.onHealthEmpty += HealthBarWithArmor.OnOwnerDead;
            
            //Đặt lại UI giáp khi boss thoát khỏi weaken state
            this.healthComponent.onRestoreArmor += this.HealthBarWithArmor.ResetMaxArmor;
        }
    }
}