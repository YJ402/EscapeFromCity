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
        //모든 컴포넌트 비활성화.
        GetComponent<Collider>().enabled = false;
        Destroy(GetComponent<Collider>());
        GetComponent<Animator>().enabled = false;
        Destroy(GetComponent<Animator>());
        Destroy(GetComponent<Rigidbody>());

        //자식 객체 삭제
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
        
        //중복하지 않는 랜덤숫자를 dropCount만큼 뽑기
        while(hash.Count < dropCount)
        {
            int i = Random.Range(0, ZombiePartsMesh.Count);
            if (hash.Contains(i)) continue;
            hash.Add(i);
        }

        //dropCount만큼 생성 반복.
        for (int i = 0; i < dropCount; i++)
        {
            //dropPref 객체 생성
            go = Instantiate(dropPref, transform.position, Random.rotation);
            go.transform.SetParent(transform, true);
            go.transform.localScale = Vector3.one;
            
            //생성된 객체의 메쉬 랜덤 지정.
            dropPrefAdjust = go.GetComponentInChildren<DropPrefAdjust>();
            dropPrefAdjust.Init();
            dropPrefAdjust.LoadMesh(ZombiePartsMesh[hash[i]]);

            //날려주기
            rig = go.GetComponent<Rigidbody>();
            rig.AddForce(new Vector3(Random.Range(-throwingStrength, throwingStrength), Random.Range(0.3f, 0.6f), Random.Range(-throwingStrength, throwingStrength)), ForceMode.Impulse);
        }
    }


    IEnumerator Disappear() // 2분 후 드랍아이템 사라짐
    {
        yield return new WaitForSeconds(120f);

        Destroy(gameObject);
    }
}
