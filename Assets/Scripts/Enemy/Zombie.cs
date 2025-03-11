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
        Destroy(GetComponent<Collider>());
        GetComponent<Animator>().enabled = false;
        Destroy(GetComponent<Animator>());
        Destroy(GetComponent<Rigidbody>());

        //�ڽ� ��ü ����
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        ItemDrop();
        StartCoroutine(Disappear());
    }

    private void ItemDrop()
    {
        GameObject go;
        Rigidbody rig;
        DropPrefAdjust dropPrefAdjust;
        List<int> hash = new();
        
        //�ߺ����� �ʴ� �������ڸ� dropCount��ŭ �̱�
        while(hash.Count < dropCount)
        {
            int i = Random.Range(0, ZombiePartsMesh.Count);
            if (hash.Contains(i)) continue;
            hash.Add(i);
        }

        //dropCount��ŭ ���� �ݺ�.
        for (int i = 0; i < dropCount; i++)
        {
            //dropPref ��ü ����
            go = Instantiate(dropPref, transform.position, Random.rotation);
            go.transform.SetParent(transform, true);
            go.transform.localScale = Vector3.one;
            
            //������ ��ü�� �޽� ���� ����.
            dropPrefAdjust = go.GetComponentInChildren<DropPrefAdjust>();
            dropPrefAdjust.Init();
            dropPrefAdjust.LoadMesh(ZombiePartsMesh[hash[i]]);

            //�����ֱ�
            rig = go.GetComponent<Rigidbody>();
            rig.AddForce(new Vector3(Random.Range(-throwingStrength, throwingStrength), Random.Range(0.3f, 0.6f), Random.Range(-throwingStrength, throwingStrength)), ForceMode.Impulse);
        }
    }


    IEnumerator Disappear() // 2�� �� ��������� �����
    {
        yield return new WaitForSeconds(120f);

        Destroy(gameObject);
    }
}
