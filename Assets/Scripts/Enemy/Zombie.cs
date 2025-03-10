using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject dropPref;
    [SerializeField] int dropCount =5;
    [SerializeField] private  List<Mesh> ZombiePartsMesh;
    [SerializeField] private float throwingStrength = 0.5f;


    public void SteppedHead()
    {
        Die();
    }

    private void Die()
    {
        //��� ������Ʈ ��Ȱ��ȭ.
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Destroy(GetComponent<Rigidbody>());

        //�ڽ� ��ü ����
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        Throw();
        StartCoroutine(Disappear());
    }

    private void Throw()
    {
        GameObject go;
        Rigidbody rig;
        DropPrefAdjust dropPrefAdjust;
        List<int> hash = new();
        
        while(hash.Count < dropCount)
        {
            int i = Random.Range(0, ZombiePartsMesh.Count);
            //if (hash.Contains(i)) break;
            hash.Add(i);
        }

        //����
        for (int i = 0; i < dropCount; i++)
        {
            //��ü ����
            go = Instantiate(dropPref, transform.position, Random.rotation);
            go.transform.SetParent(transform, true);
            go.transform.localScale = Vector3.one;
            
            //�޽� ���� ����.
            dropPrefAdjust = go.GetComponentInChildren<DropPrefAdjust>();
            dropPrefAdjust.Init();
            dropPrefAdjust.LoadMesh(ZombiePartsMesh[hash[i]]);

            //rigidbody ���ٸ� �ٿ��ֱ�
            if (!go.TryGetComponent<Rigidbody>(out rig))
            {
                rig = go.AddComponent<Rigidbody>();
            }
            //�����ֱ�
            rig.AddForce(new Vector3(Random.Range(-throwingStrength, throwingStrength), Random.Range(0.3f, 0.6f), Random.Range(-throwingStrength, throwingStrength)), ForceMode.Impulse);
        }
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(120f);

        Destroy(gameObject);
    }
}
