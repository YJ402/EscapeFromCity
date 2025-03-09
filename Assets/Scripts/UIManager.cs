using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface Updateable
//{
//    public void UpdateUI();
//}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return _instance;
        }
    }

    //Updateable[] UpdateableUI;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }


    }

    //private void Start()
    //{
    //    StartScene();
    //}

    //void StartScene()
    //{
    //    UpdateableUI = transform.GetComponentsInChildren<Updateable>();
    //}

    //public void UpdateUIs()
    //{
    //    for(int i = 0; UpdateableUI.Length < i; i++)
    //    {
    //        UpdateableUI[i].UpdateUI();
    //    }
    //}
}
