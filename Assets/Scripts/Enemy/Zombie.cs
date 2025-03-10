using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public List<GameObject> dropPref;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SteppedHead()
    {
        Die();
    }

    private void Die()
    {
        Throw();
        Destroy(gameObject);
    }

    private void Throw()
    {
        GameObject go;
        Rigidbody rig;
        //생성
        for (int i = 0; i < dropPref.Count; i++)
        {
            go = Instantiate(dropPref[i], transform.position, Random.rotation);
            //rigidbody 없다면 붙여주기
            if (!go.TryGetComponent<Rigidbody>(out rig))
            {
                rig = go.AddComponent<Rigidbody>();
            }
            //날려주기
            rig.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }

    }
}
