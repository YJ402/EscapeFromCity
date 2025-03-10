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

// ���� ����
public enum MovementDirection
{
    Forward,
    Backward
}

// ���� ���� �ܰ�
public enum JumpPhase
{
    None,   // ���� �ƴ� ��
    Rise,   // ����
    Float,  // �ξ�
    Land    // ����
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

        // �ִ� �÷��� �ӵ� �ʱ�ȭ
        //def_moveSpeed = 1 *
        //moveAnimPlaySpeed  = def_moveSpeed  / moveSpeed;

    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
        //Debug.Log("���ΰ�?" + isGrounded);
        //Debug.Log("����?" + jumpInput);
        Jump();
        animator.SetBool("Grounded", isGrounded);
    }

    private void Move()
    {
        Vector3 movementInput = directionInput; // �𷺼� ��ǲ �� ���� �����صα�.

        float l_moveSpeed = moveSpeed;

        if (walkMode)
            l_moveSpeed *= walkScale;

        //���� �̵� ��/actualMovement: movementInput�� ī�޶� �������� ��ȯ. currentMovement: �ӵ��� �ε巯�� ����.
        currentMovementX = Mathf.Lerp(currentMovementX, movementInput.x, Time.fixedDeltaTime * interpolation);
        currentMovementZ = Mathf.Lerp(currentMovementZ, movementInput.y, Time.fixedDeltaTime * interpolation);
        actualMovement = (cam.transform.right * currentMovementX + cam.transform.forward * currentMovementZ) * l_moveSpeed;

        //actualMovement�� ũ�⸦ �����ϸ鼭 y�� ����
        float temp = actualMovement.magnitude;
        actualMovement.y = 0;
        actualMovement = actualMovement.normalized * temp;
        transform.position += actualMovement;

        //�ִϸŰ�����/ moveAnimParameter(���� Ʈ�� �Ķ����): moveAnimParameter �� ����.
        moveAnimParameter = movementInput.magnitude;
        if (walkMode)
            moveAnimParameter *= walkScale; // ����: input�� �ԷµǴ� ���� ������ �Է��� �ƴ϶� �Է��� �������� ȣ���.
        if (currentMovementZ < 0)
            moveAnimParameter *= -1;
        animator.SetFloat("MoveAnimParameter", moveAnimParameter);

        //�ִ��÷��̼ӵ�/moveSpeed(�÷��̾� �ӵ�/�⺻��0.032) * moveAnimPlaySpeed(����Ʈ �ӵ����� �ִϸ��̼��� 1�� ����ӵ��� ������ ���� ���)
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
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > GroundWallThreshold) // ��簡 40�� ���ϸ� �ٴ����� �ν�.
            {
                touchingGrounds.Add(collision.collider);
                isGrounded = true;
                break;
            }
        }

        float contactPower = collision.relativeVelocity.magnitude;  
        if ((floorLayerMask == ((1 << collision.gameObject.layer) | floorLayerMask)) && contactPower / 2f > bounceThreshold)
        {
            Debug.Log("��ݷ�:" + contactPower);
            rigidbody.AddForce(contactPoints[Random.Range(0, contactPoints.Length)].normal * contactPower / 2f, ForceMode.Impulse);
        }

        if (enemyheadLayerMask == ((1 << collision.gameObject.layer) | enemyheadLayerMask))
        {
            Debug.Log("��ݷ�:" + contactPower);
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
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > GroundWallThreshold) // �׽�Ʈ
            {
                //�ٴ���
                touchingGrounds.Add(collision.collider);
                validSurface = true;
                break;
            }
        }
        if (!validSurface)
        {
            //�ٴ� �ƴ�
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
        directionInput = context.ReadValue<Vector2>(); // �̹� �������ͷ� ��µ�.
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

        //if (context.phase == InputActionPhase.Canceled) ���⼭ false�� �ٲ��ָ� fixedupdate�� �ð����� ������ �Է��� ������ ����.
        //{
        //    jumpInput = false; 
        //}
    }

    //if (context.phase == InputActionPhase.Disabled)
    //    Debug.Log("��Ȱ��ȭ");
    //if (context.phase == InputActionPhase.Waiting)
    //    Debug.Log("Ȱ��ȭ");
    //if (context.phase == InputActionPhase.Started)
    //    Debug.Log("��ŸƮ");
    //if (context.phase == InputActionPhase.Performed)
    //    Debug.Log("����");
    //if (context.phase == InputActionPhase.Canceled)
    //    Debug.Log("���");
}
