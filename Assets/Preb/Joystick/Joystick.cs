using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform ThumbStick;
    [SerializeField] RectTransform Background;
    [SerializeField] RectTransform CenterTrans;

    bool bWasDragging;

    public delegate void OnStickValueUpdate(Vector2 inputVal); // sử dụng delegate này để lưu trữ tham chiếu của các hàm có kiểu trả về, tham số tương tự
    public event OnStickValueUpdate onStickValueUpdate; // event để gọi mỗi khi có sự thay đổi - nó sẽ gọi tới mọi hàm đc đăng ký với event này

    public delegate void OnStickTapped();
    public event OnStickTapped onStickTapped;

    private void Start()
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 TouchPos = eventData.position;
        Vector2 CenterPos = Background.position;
        Vector2 localOffset = Vector2.ClampMagnitude(TouchPos - CenterPos, Background.sizeDelta.x / 2); // Clamnp posittion của joystick không vượt quá background bằng cách clamp khoảng cách của                                                                                                 
                                                                                                        // <vị trí kéo và vị trí trung tâm background> nhỏ hơn bán kính background
        ThumbStick.position = CenterPos + localOffset; // Di chuyển joystick đến vị trí kéo sau khi đã được clamp ko vượt quá phạm vi <Center không đổi>
        Vector2 inputVal = localOffset/ (Background.sizeDelta.x/2);
        onStickValueUpdate?.Invoke(inputVal); //Dòng này gọi (kích hoạt) tất cả các phương thức đã đăng ký với event onStickValueUpdate và truyền inputVal làm tham số.
                                              //Dấu ?. là null-conditional operator, đảm bảo rằng event chỉ được kích hoạt nếu có bất kỳ
                                              //đối tượng nào đã đăng ký, nếu không có đối tượng nào đăng ký, nó sẽ không làm gì cả.  
        bWasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Background.position = eventData.position; //Khi ấn vào background và joystick sẽ di chuyển đến vị trí ngón tay tạo cảm giác thuận lợi, không gò bó thao tác joystick 1 chỗ cố định
        ThumbStick.position = eventData.position; //
        bWasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Background.position = CenterTrans.position; //Khi nhả tay ra trả về vị trí cũ (lưu ý là vị trí của toàn bộ back và joy đều không cố định như trong hàm Down giải thích)
        ThumbStick.position = Background.position;
        onStickValueUpdate?.Invoke(Vector2.zero);   //đưa tham số 0 khi nhả thao tác với joystick
        if (!bWasDragging) 
        {
            onStickTapped?.Invoke();
        }
    }
}
