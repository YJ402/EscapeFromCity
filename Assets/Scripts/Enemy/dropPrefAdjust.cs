using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropPrefAdjust : MonoBehaviour
{
    MeshRenderer meshrenderer;
    MeshFilter filter;
    MeshCollider collider;
    //public int randomMesh;


    private void Start()
    {
        if (meshrenderer == null)
            meshrenderer = GetComponent<MeshRenderer>(); 
        transform.localPosition -= meshrenderer.localBounds.center;
    }
    public void Init()
    {
        if(meshrenderer == null)
            meshrenderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
    }
    public void LoadMesh(Mesh _mesh)
    {
        filter.mesh = _mesh;
        collider.sharedMesh = _mesh;
    }

}
