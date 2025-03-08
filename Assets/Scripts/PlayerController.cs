using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementState
{
    Idle,
    Walking,
    Running,
    Jumping
}

// 방향 상태
public enum MovementDirection
{
    Forward,
    Backward
}

// 점프 세부 단계
public enum JumpPhase
{
    None,   // 점프 아닐 때
    Rise,   // 도약
    Float,  // 부양
    Land    // 착지
}

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector2 mouseDelta;
    Camera cam;
    Animator animator;

    [Header("Movement")]
    Vector2 directionInput;
    [SerializeField] private float moveSpeed = 0.032f;
    private bool walkMode;
    private float walkScale = 0.33f;
    private float currentMovementX;
    private float currentMovementZ;
    public float interpolation = 20;
    private Vector3 actualMovement;
    float moveAnimPlaySpeed = 31.25f;
    float moveAnimParameter;

    [Header("Jump")]
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        cam = Camera.main;

        // 애니 플레이 속도 초기화
        //def_moveSpeed = 1 *
        //moveAnimPlaySpeed  = def_moveSpeed  / moveSpeed;

    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementInput = directionInput; // 디렉션 인풋 값 따로 저장해두기.

        float l_moveSpeed = moveSpeed;

        if (walkMode)
            l_moveSpeed *= walkScale;

        //실제 이동 값/actualMovement: movementInput을 카메라 방향으로 전환. currentMovement: 속도의 부드러운 증감.
        currentMovementX = Mathf.Lerp(currentMovementX, movementInput.x, Time.fixedDeltaTime * interpolation);
        currentMovementZ = Mathf.Lerp(currentMovementZ, movementInput.y, Time.fixedDeltaTime * interpolation);
        actualMovement = (cam.transform.right * currentMovementX + cam.transform.forward * currentMovementZ) * l_moveSpeed;

        //actualMovement의 크기를 유지하면서 y값 제거
        float temp = actualMovement.magnitude;
        actualMovement.y = 0;
        actualMovement = actualMovement.normalized * temp;
        transform.position += actualMovement;

        //애니매개변수/ moveAnimParameter(블렌더 트리 파라미터): moveAnimParameter 값 조절.
        moveAnimParameter = movementInput.magnitude;
        if (walkMode)
            moveAnimParameter *= walkScale; // 문제: input이 입력되는 것이 연속적 입력이 아니라 입력이 있을때만 호출됨.
        if (currentMovementZ < 0)
            moveAnimParameter *= -1;
        animator.SetFloat("MoveAnimParameter", moveAnimParameter);

        //애니플레이속도/moveSpeed(플레이어 속도/기본값0.032) * moveAnimPlaySpeed(디폴트 속도에서 애니메이션이 1의 재생속도를 가지기 위한 계수)
        animator.SetFloat("MoveAnimPlaySpeed", moveSpeed * moveAnimPlaySpeed);

    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnDirectionInput(InputAction.CallbackContext context)
    {
        directionInput = context.ReadValue<Vector2>(); // 이미 단위벡터로 출력됨.
        Debug.Log(directionInput);
    }

    public void OnCapsLock(InputAction.CallbackContext context)
    {
        if (walkMode)
            walkMode = false;
        else walkMode = true;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {

    }
}
