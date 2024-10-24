using System.Collections;
using UnityEngine;

namespace Preb.Weapon.DropProjectile
{
    public class DropProjectile : Projectile
    {
        [SerializeField] private GameObject warningPrefab;  // Prefab cảnh báo
        [SerializeField] private float expansionDuration = 2f; // Thời gian để cảnh báo mở rộng
        [SerializeField] private float maxWarningRadius = 3f;   // Kích thước tối đa của cảnh báo
        [SerializeField] private float dropSpeed = 10f;    // Tốc độ rơi của projectile
        [SerializeField] private float fallHeight = 10f;   // Chiều cao từ đó projectile bắt đầu rơi

        private bool hasExploded = false;

        public override void Launch(GameObject instigator, Vector3 destination)
        {
            base.Launch(instigator, destination);
            
            // Hiển thị cảnh báo trên mặt đất trước khi rơi
            StartCoroutine(ShowWarningAndFall());
        }

        // Coroutine để hiển thị cảnh báo và sau đó cho projectile rơi xuống
        private IEnumerator ShowWarningAndFall()
        {
            // Tạo cảnh báo dưới đất
            GameObject warning      = Instantiate(warningPrefab, transform.position - new Vector3(0, this.fallHeight, 0), Quaternion.Euler(-90, 0, 0));
            Vector3    initialScale = Vector3.zero;
            Vector3    targetScale  = Vector3.one * maxWarningRadius * 2f; // Phóng to cảnh báo theo đường kính

            float elapsedTime = 0f;

            // Mở rộng cảnh báo
            while (elapsedTime <= expansionDuration)
            {
                elapsedTime                  += Time.deltaTime;
                warning.transform.localScale =  Vector3.Lerp(initialScale, targetScale, elapsedTime / expansionDuration);
                yield return null;
            }

            // Sau khi cảnh báo mở rộng hoàn tất, bắt đầu rơi
            Destroy(warning.gameObject);  // Hủy cảnh báo sau khi rơi

            StartCoroutine(FallToGround());
        }


        // Logic để rơi từ trên cao xuống
        private IEnumerator FallToGround()
        {
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;
            Vector3 initialPosition = startPosition;
            Vector3 targetPosition = transform.position - Vector3.up * fallHeight; // Mục tiêu là vị trí hiện tại của game object (trên mặt đất)

            // Rơi dần dần xuống vị trí hiện tại
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * dropSpeed;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                yield return null;
            }

            // Kích hoạt nổ sau khi rơi chạm đất
            Explode();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (hasExploded) return; // Đảm bảo chỉ nổ một lần
            base.OnTriggerEnter(other); // Gọi logic OnTriggerEnter từ lớp cơ sở
        }

        public override void Explode()
        {
            if (hasExploded) return; // Đảm bảo chỉ nổ một lần

            hasExploded = true; // Đánh dấu đã nổ
            base.Explode(); // Gọi Explode từ lớp cơ sở
        }
    }
}
