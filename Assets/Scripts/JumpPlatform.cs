using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    float mass;
    float massLog;
    float jumpPower;
    Vector3 jumpVector;
    [SerializeField][Range(0f, 1f)] private float ratio = 0.7f;

    private void Start()
    {
        mass = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        jumpPower = 2f * Mathf.Log(mass) - 2.2836f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ContactPoint[] contactPoints = collision.contacts; 
            Vector3 normalVector = -contactPoints[Random.Range(0, contactPoints.Length)].normal;
            Vector3 bounceVector = ((normalVector * ratio) + Vector3.up * (1 - ratio));
            bounceVector = bounceVector.normalized;
            collision.rigidbody.AddForce(bounceVector * jumpPower, ForceMode.Impulse); // 이게 코드가 안되는데 이유가 뭘까?
        }
    }
}
