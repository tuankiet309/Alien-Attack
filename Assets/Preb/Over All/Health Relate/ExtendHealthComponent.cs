namespace Preb.Over_All.Health_Relate
{
    using UnityEngine;

    public class ExtendHealthComponent : HealthComponent
    {
        [Header("Armor")]
        [SerializeField] private float armorValue    = 50; // Giá trị giáp hiện tại
        [SerializeField] private float maxArmorValue = 50; // Giá trị giáp tối đa

        public event OnArmorBroken  onArmorBroken;
        public event OnTakeDamamge  onTakeDamamge;
        public event OnRestoreArmor onRestoreArmor;

        public delegate void OnTakeDamamge(float health, float delta, float maxHealth, float armor, float maxArmor, GameObject Instigator);

        public delegate void OnArmorBroken();

        public delegate void OnRestoreArmor();
        
        // Phương thức thay đổi máu và giáp
        public override void ChangeHealth(float amount, GameObject Instigator)
        {
            if (amount == 0 || GetHealth == 0) // Nếu đã hết máu hoặc không có sát thương, thoát
                return;

            if (amount < 0) // Nếu nhận sát thương
            {
                float remainingDamage = amount;

                if (armorValue > 0) // Nếu còn giáp
                {
                    armorValue += amount; // Trừ sát thương vào giáp trước
                    //Debug.Log(this.armorValue);
                    if (armorValue < 0)
                    {
                        remainingDamage = armorValue;
                        // Số sát thương còn lại sau khi phá giáp
                        armorValue = 0; // Giáp đã bị phá
                        
                        this.onArmorBroken?.Invoke();
                    }
                    else
                    {
                        remainingDamage = 0; // Nếu giáp còn, không giảm máu
                    }
                }

                if (remainingDamage < 0) // Nếu vẫn còn sát thương sau khi phá giáp
                {
                    SetHealth(remainingDamage); // Trừ máu sau khi giáp đã bị phá
                    //Debug.Log(GetHealth);
                    //this.InvokeOnHealthChange(GetHealth, remainingDamage, GetMaxHealth);
                }
                onTakeDamamge?.Invoke(GetHealth, amount, GetMaxHealth, armorValue, maxArmorValue, Instigator);

                this.PlayHitAudio();
            }

            if (GetHealth <= 0) // Nếu máu bằng 0, gọi sự kiện chết
            {
                SetHealth(0);
                this.InvokeOnHealthEmpty(Instigator);
                Vector3 loc = transform.position;
                PlayDeathAudioAtPos(loc);
            }
        }
        
        public float GetArmorValue()
        {
            return armorValue;
        }

        public float GetMaxArmorValue()
        {
            return maxArmorValue;
        }

        public void SetArmorValue(float value)
        {
            armorValue = Mathf.Clamp(value, 0, maxArmorValue); // Đảm bảo giá trị giáp không vượt quá giới hạn
            onRestoreArmor?.Invoke();
        }
    }
}
