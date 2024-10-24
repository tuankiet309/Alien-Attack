using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour,ITeamInterface
{
    // Start is called before the first frame update
    [SerializeField] Joystick moveStick; //Kết nối tới script của joystick để thực hiện lấy giá trị từ joy
    [SerializeField] Joystick aimStick;
    [SerializeField] float moveSpeed = 1f; // tốc độ di chuyển
    [SerializeField] int TeamID = 1;
    Animator animator; // animator
    float animationTurnSpeed; //tốc độ hoạt ảnh khi quay người
    [SerializeField] float animTurnSpeedToLerp; // độ trễ khi quay người - chủ yếu để animation trông mượt và không bị khựng

    Vector2 moveInput; //biến để chứa giá trị từ event trả về từ moveStick
    Vector2 aimInput; // biến chứa giá trị từ evetn trả về từ aimstick
    CameraController cameraController; //Lấy script cameracontroller về để thực hiện truyền giá trị để quay camera khi di chuyển
    Camera mainCam; //camera
    MovementComponent movement;
    CharacterController characterController; //Built-in character controller do lười =)))

    [Header("Health And Damage")]
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerValueGauge healthBar;
    [SerializeField] UIManager UImanager;

    [Header("Ability And Stamina")]
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] PlayerValueGauge staminaBar;

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent; // Lấy scrript inventory để thực hiện đổi đồ
    void Start()
    {
        mainCam = Camera.main; //Lấy camera để thực hiện tính toán di chuyển nhật vật theo hướng của camera
        moveStick.onStickValueUpdate += moveStickUpdate; // Đây không phải phép cộng, đây là thực hiện đăng ký hàm moveStickUpdate vào event onStickValueUpdate của joy move, để mỗi khi
        aimStick.onStickValueUpdate += aimStickUpdate;   // drag sẽ được truyển tham chiếu giá trị mới. tương tự  đăng ký hàm aimStickUpdate vào event onStickValueUpdate của joy aim
        aimStick.onStickTapped += StartSwitchWeapon;     // Đăng ký hàm SwitchWeapon mỗi khi tap vào aimStick
        cameraController = FindObjectOfType<CameraController>(); //lấy cameracoltrller từ camera
        animator = GetComponent<Animator>(); // Lấy animator của player
        movement = GetComponent<MovementComponent>();
        characterController = GetComponent<CharacterController>();
        healthComponent.onHealthChange += HeathChange;
        healthComponent.BroadCastHealthValueImmediately();
        
        healthComponent.onHealthEmpty += DeadSequence;
        abilityComponent.onStaminaChanged += StaminaChange;
        abilityComponent.BroadCastStaminaRightAway();
        GameStatic.GameStarted();
    }

    private void StaminaChange(float stamina, float maxStamina)
    {
        staminaBar.UpdateValueVisualization(stamina, 0, maxStamina);
    }

    private void DeadSequence(GameObject killer)
    {
        animator.SetLayerWeight(2, 1);
        animator.SetTrigger("isDead");
        UImanager.SetGameplayControlEnable(false);
        
    }

    private void HeathChange(float health, float delta, float maxHealth)
    {
        healthBar.UpdateValueVisualization(health, delta, maxHealth);
    }

    public int GetTeamID()
    {
        return TeamID;
    }
    public void AttackingPoint() //Hàm này được gọi trong animation event của từng weapon, chạy khi đến  1 frame
    {
        inventoryComponent.GetActiveWeapon().Attack(); // Thực hiện chạy function tấn công của vũ khi đang trang bị.
    }
    private void StartSwitchWeapon() //Hàm thay đổi vũ khí
    {
        if (inventoryComponent.GetWeaponCount() < 2)
            return;
        animator.SetTrigger("switching"); //Bắt đầu trigger để thực hiện animation đổi vũ khi
        
    }
    public void SwitchWeapon() //Hàm này được gọi khi animation đổi vũ khi xẩy ra bằng animation event ở 1 frame nhất định
    {
        inventoryComponent.NextWeapon();    //Đổi vũ khi tiếp theo
    }
    void moveStickUpdate(Vector2 inputVal) //Đây là hàm đã được đăng ký, thay đổi giá trị moveInput bằng với giá trị tham số inputVal trả về từ event onStickValueUpdate từ joy move
    {
        moveInput = inputVal;
    }
    void aimStickUpdate(Vector2 inputVal) //Đây là hàm đã được đăng ký, thay đổi giá trị aim bằng với giá trị tham số inputVal trả về từ event onStickValueUpdate từ joy aim
    { 
        aimInput = inputVal;
        if(aimInput.magnitude != 0) //Nếu aimInput có giá trị aka aimStick đc kéo
        {
            animator.SetBool("shooting", true); //set biến thành true hực hiện chuyển qua animation bắn
        }
        else
        {
            animator.SetBool("shooting", false); //set biến thành false chuyển qua animation dừng bắn
        }
    }
    Vector3 stickInputToWorldDirection(Vector2 inputVal) //hàm này sử lí các giá trị từ các joystick để chuyển thành hướng/vector để tác động lên nhân vật theo dựa trên hướng của camera
    {
        Vector3 rightDir = mainCam.transform.right; //lấy hướng vector phải của camera ( camera đã bị rotate ) theo world space (local space thì lúc nào cũng là 0,0,-1)
        Vector3 forwardDir = Vector3.Cross(rightDir, Vector3.up); //Lấy vector hướng về trước của camera bằng cách tính tích có hướng (véc tơ vuông góc với 2 véc tơ đầu vào) của vector phải của camera
                                                                  //và vector hướng lên trên(kp của camera) của world space
        return rightDir * inputVal.x + forwardDir * inputVal.y; // Cộng vào vì 2 vector không ảnh hưởng lên nhau, cộng vào để thành vector tổng hợp
    }
    void Update()
    {
        PerformMove(); //Xử lí di chuyển và quay người
        UpdateCamera(); //Xử lí quay camera
    }

    private void PerformMove()
    {
        Vector3 moveDir = stickInputToWorldDirection(moveInput);  //Sau khi xử lí giá trị từ move joystick trả về thành hướng di chuyển của nhân vật theo hướng của camera, gán vào biến moveDir                                                            
        
        characterController.Move(moveDir * Time.deltaTime * moveSpeed); //di chuyển nhân vật
        
        UpdateAim(moveDir);

        float forward = Vector3.Dot(moveDir,transform.forward);
        float right = Vector3.Dot(moveDir,transform.right);
        
        animator.SetFloat("forwardSpeed",forward);
        animator.SetFloat("rightSpeed", right);

        characterController.Move(Vector3.down * Time.deltaTime * 10f);
    }

    private void UpdateAim(Vector3 moveDir)
    {
        Vector3 aimDir = moveDir; // aimDir được gán với moveDir để nếu không tương tác với joy aim, nhân vật vẫn quay theo hướng di chuyển :>
        if (aimInput.magnitude != 0) //Nếu vector trả về từ aimstick khác không thì thực hiện gán aimDir vào giá trị đã được xử lí thành hướng quay của nhân vật theo hướng của camera - Quay theo joystick
        {
            aimDir = stickInputToWorldDirection(aimInput);
        }
        RotateTowards(aimDir); //Thực hiện quay nhân vật theo hướng đã được xử lí

    }

    private void UpdateCamera()
    {
        if ((moveInput.magnitude != 0 && aimInput.magnitude == 0) && cameraController != null) //Nếu 2 stick đc kéo thực hiện quay camera
        {
            cameraController.AddYawnInput(moveInput.x); //truyền tham số giá trị khi joystick kéo trái kéo phải để làm camera quay theo
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = movement.RotateTowards(aimDir);
        
        animationTurnSpeed = Mathf.Lerp(animationTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeedToLerp);
        animator.SetFloat("turnSpeed", animationTurnSpeed);
    }

    internal void AddMoveSpeed(float boostAmount)
    {
        moveSpeed += boostAmount;
    }
    public void DeadFinish()
    {
        UImanager.SwitchToDeathMenu();
    }
}
