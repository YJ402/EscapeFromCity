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
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnDirectionInput(InputAction.CallbackContext context)
    {
        directionInput = context.ReadValue<Vector2>(); // �̹� �������ͷ� ��µ�.
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
