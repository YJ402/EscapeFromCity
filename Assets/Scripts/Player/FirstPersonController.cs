using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("objects")]
    public Transform cameraContainer;

    [Header("Option")]
    [SerializeField] private float verticalClampMin = -80f;
    [SerializeField] private float verticalClampMax = 80f;
    [SerializeField] private float smoothing = 0.15f; // interploation^-1
    [SerializeField] private float sensitivity = 0.3f;

    float currentRotX; // 컨테이너 x회전값
    float currentRotY; // 플레이어 y회전값
    Vector3 targetRot;

    Player player;

    private void Start()
    {
        currentRotX = cameraContainer.localRotation.eulerAngles.x; // 각도는 시작할때 여기서 초기화.
        currentRotY = transform.rotation.eulerAngles.y; // 각도는 시작할때 여기서 초기화.

        targetRot = new Vector3(currentRotX, currentRotY, 0);

        Cursor.lockState = CursorLockMode.Locked;

        player = GameManager.Instance.player;
    }


    private void LateUpdate()
    {
        if (player.playerState == PlayerState.Navigation)
            RotateCam(GameManager.Instance.player.controller.mouseDelta);
    }

    // z축은 고정. 
    public void RotateCam(Vector2 mouseDelta)
    {
        if (mouseDelta.magnitude > 0)
        {
            targetRot.y += ((Vector3)mouseDelta).x * sensitivity; // 세번째 문제: 근본적 문제는 아님. 마우스 움직임과 회전 좌표는 교차해야함.
            targetRot.x += -((Vector3)mouseDelta).y * sensitivity; // 개선안1: 민감도 추가 
            targetRot.x = Mathf.Clamp(targetRot.x, verticalClampMin, verticalClampMax); // 첫번째 문제,: 근본적 문제는 x 클램프 조절.
        }
        // 두번째 문제: 매번 currentRot을 초기화하는 것. 쿼터니언 -> 오일러 -> 쿼터니언으로 변환이 왔다갔다할때마다 두 타입 차이로인한 값의 차이가 계속해서 발생함.
        currentRotX = Mathf.Lerp(currentRotX, targetRot.x, Time.deltaTime / smoothing);
        currentRotY = Mathf.Lerp(currentRotY, targetRot.y, Time.deltaTime / smoothing);

        cameraContainer.localRotation = Quaternion.Euler(currentRotX, 0, 0);
        transform.rotation = Quaternion.Euler(0, currentRotY, 0);
    }
}
