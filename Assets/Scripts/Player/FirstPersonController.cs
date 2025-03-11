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

    float currentRotX; // �����̳� xȸ����
    float currentRotY; // �÷��̾� yȸ����
    Vector3 targetRot;

    Player player;

    private void Start()
    {
        currentRotX = cameraContainer.localRotation.eulerAngles.x; // ������ �����Ҷ� ���⼭ �ʱ�ȭ.
        currentRotY = transform.rotation.eulerAngles.y; // ������ �����Ҷ� ���⼭ �ʱ�ȭ.

        targetRot = new Vector3(currentRotX, currentRotY, 0);

        Cursor.lockState = CursorLockMode.Locked;

        player = GameManager.Instance.player;
    }


    private void LateUpdate()
    {
        if (player.playerState == PlayerState.Navigation)
            RotateCam(GameManager.Instance.player.controller.mouseDelta);
    }

    // z���� ����. 
    public void RotateCam(Vector2 mouseDelta)
    {
        if (mouseDelta.magnitude > 0)
        {
            targetRot.y += ((Vector3)mouseDelta).x * sensitivity; // ����° ����: �ٺ��� ������ �ƴ�. ���콺 �����Ӱ� ȸ�� ��ǥ�� �����ؾ���.
            targetRot.x += -((Vector3)mouseDelta).y * sensitivity; // ������1: �ΰ��� �߰� 
            targetRot.x = Mathf.Clamp(targetRot.x, verticalClampMin, verticalClampMax); // ù��° ����,: �ٺ��� ������ x Ŭ���� ����.
        }
        // �ι�° ����: �Ź� currentRot�� �ʱ�ȭ�ϴ� ��. ���ʹϾ� -> ���Ϸ� -> ���ʹϾ����� ��ȯ�� �Դٰ����Ҷ����� �� Ÿ�� ���̷����� ���� ���̰� ����ؼ� �߻���.
        currentRotX = Mathf.Lerp(currentRotX, targetRot.x, Time.deltaTime / smoothing);
        currentRotY = Mathf.Lerp(currentRotY, targetRot.y, Time.deltaTime / smoothing);

        cameraContainer.localRotation = Quaternion.Euler(currentRotX, 0, 0);
        transform.rotation = Quaternion.Euler(0, currentRotY, 0);
    }
}
