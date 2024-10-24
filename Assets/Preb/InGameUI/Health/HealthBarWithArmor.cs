namespace Preb.InGameUI.Health
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarWithArmor : HealthBar
    {
        [SerializeField] Slider armorSlider; // Thanh giáp (ArmorBar)

        // Phương thức này thiết lập cả giá trị của thanh máu và thanh giáp
        public void SetHealthAndArmor(float health, float delta,float maxHealth, float armor, float maxArmor)
        {
            SetHealthSliderValue(health, delta, maxHealth);

            if (armorSlider != null)
            {
                armorSlider.value = (armor / maxArmor); // Cập nhật thanh giáp
            }
        }

        public void ResetMaxArmor()
        {
            this.armorSlider.value = 1;
        }
    }

}