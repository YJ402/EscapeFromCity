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
    public Rigidbody rigidbody;

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
    [SerializeField] private float GroundWallThreshold = 0.79f;
    HashSet<Collider> touchingGrounds = new();

    private bool jumpInput;

    [SerializeField] private float jumpPower = 2.5f;
    [SerializeField] private float bounceThreshold = 2f;
    [SerializeField] private LayerMask floorLayerMask;
    [SerializeField] private LayerMask enemyheadLayerMask;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody>();

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
        //Debug.Log("땅인가?" + isGrounded);
        //Debug.Log("점프?" + jumpInput);
        Jump();
        animator.SetBool("Grounded", isGrounded);
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

    void Jump()
    {
        if (jumpInput && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > GroundWallThreshold) // 경사가 40도 이하면 바닥으로 인식.
            {
                touchingGrounds.Add(collision.collider);
                isGrounded = true;
                break;
            }
        }

        float contactPower = collision.relativeVelocity.magnitude;  
        if ((floorLayerMask == ((1 << collision.gameObject.layer) | floorLayerMask)) && contactPower / 2f > bounceThreshold)
        {
            Debug.Log("충격량:" + contactPower);
            rigidbody.AddForce(contactPoints[Random.Range(0, contactPoints.Length)].normal * contactPower / 2f, ForceMode.Impulse);
        }

        if (enemyheadLayerMask == ((1 << collision.gameObject.layer) | enemyheadLayerMask))
        {
            Debug.Log("충격량:" + contactPower);
            rigidbody.AddForce(contactPoints[Random.Range(0, contactPoints.Length)].normal * (contactPower * 1.5f), ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurface = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > GroundWallThreshold) // 테스트
            {
                //바닥임
                touchingGrounds.Add(collision.collider);
                validSurface = true;
                break;
            }
        }
        if (!validSurface)
        {
            //바닥 아님
            touchingGrounds.Remove(collision.collider);
        }

        if (touchingGrounds.Count == 0)
            isGrounded = false;
        else isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingGrounds.Remove(collision.collider);

        if (touchingGrounds.Count == 0)
            isGrounded = false;
        else isGrounded = true;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnDirectionInput(InputAction.CallbackContext context)
    {
        directionInput = context.ReadValue<Vector2>(); // 이미 단위벡터로 출력됨.
    }

    public void OnCapsLock(InputAction.CallbackContext context)
    {
        if (walkMode)
            walkMode = false;
        else walkMode = true;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        if (!(context.phase == InputActionPhase.Canceled))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        //if (context.phase == InputActionPhase.Canceled) 여기서 false로 바꿔주면 fixedupdate와 시간차이 때문에 입력이 씹힐수 있음.
        //{
        //    jumpInput = false; 
        //}
    }

    //if (context.phase == InputActionPhase.Disabled)
    //    Debug.Log("비활성화");
    //if (context.phase == InputActionPhase.Waiting)
    //    Debug.Log("활성화");
    //if (context.phase == InputActionPhase.Started)
    //    Debug.Log("스타트");
    //if (context.phase == InputActionPhase.Performed)
    //    Debug.Log("퍼폼");
    //if (context.phase == InputActionPhase.Canceled)
    //    Debug.Log("취소");
}
