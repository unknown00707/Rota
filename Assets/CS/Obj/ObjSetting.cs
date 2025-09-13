
using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ObjSetting : MonoBehaviour
{
    public GameObject[] bigObjs; // Grounp OBj
    public MeshRenderer[][] objs; // 
    public int curID = 0;
    public LayerMask layerMask;

    void Awake()
    {
        BigObjsInit();
        curID = 0;
        //CharildInit();
        //ObjInillayColorInit();
    }

    void Update()
    {
        OnPointerDown();
        OnClickNumCul();
    }

    // Selected - color
    void CheckMySelected(int myIDList)
    {
        ChangeColorObj(myIDList); // color Change
        curID = myIDList;
    }

    void ChangeColorObj(int index)
    {
        foreach (MeshRenderer[] i in objs)
        {
            i[index].material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); // child color change to gray
        }
    }

    void ObjInillayColorInit()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            //colors[i] = objs[i].GetComponent<MeshRenderer>().material.color;
        }
    }

    // Seleted - Mouse
    void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                print("hit");
                for (int i = 0; i < bigObjs.Length; i++)
                {
                    if (hit.collider.name == bigObjs[i].name)
                    {
                        CulNameCompareObj(i);
                        return;
                    }
                }
            }
        }
    }

    void CulNameCompareObj(int name)
    {
        curID = name;
    }

    void CharildInit()
    {
        for (int i = 0; i < bigObjs.Length; i++)
        {
            var charilds = bigObjs[i].GetComponentsInChildren<MeshRenderer>();
            // foreach (var ij in charilds)
            // {
            //     objs[i].Append(ij); 
            // }
            Transform[] tr = bigObjs[i].GetComponentsInChildren<Transform>();
            foreach (Transform t in tr)
            {
                print(t.gameObject.name);
            }
        }
    }


    // Seleted - Num
    void OnClickNumCul()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            curID = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            curID = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            curID = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            curID = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            curID = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            curID = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            curID = 6;

        if (curID >= bigObjs.Length)
            curID = 0;
        else if (curID < 0)
            curID = bigObjs.Length;
    }


    public void BigObjsInit()
    {
        if(GameObject.Find("RedG") == true)
            bigObjs[0] = GameObject.Find("RedG");
        if(GameObject.Find("GreenG") == true)
            bigObjs[1] = GameObject.Find("GreenG");
        
    }
}
