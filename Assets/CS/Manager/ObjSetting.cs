
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AppUI.UI;
using Unity.Mathematics;
using UnityEngine;

public class ObjSetting : MonoBehaviour
{
    public RotatObjSub rotatObjSub;
    public GameObject[] bigObjs; // Grounp OBj
    public int curID = 0;
    public int pastID = 0;
    public LayerMask bigLayerMask;
    public LayerMask forCulSamailObjLayerMask;

    public bool reciveGameEnd = false;

    public float rayDistance = 20f;
    private const int MAX_HITS = 10;
    private readonly RaycastHit[] hitResults = new RaycastHit[MAX_HITS];



    void Awake()
    {
        rotatObjSub = gameObject.GetComponent<RotatObjSub>();
        curID = 0;

        BigObjsInit();

    }

    void Update()
    {
        if (!reciveGameEnd)
        {
            OnPointerDown(); // mouse select
            OnClickNumCul(); // Num select
        }

        if (Input.GetKeyDown(KeyCode.Space)) // game end fouction
            DetectObjAllForEnd();


        if (Input.GetKeyDown(KeyCode.UpArrow)) // Test
            ChangeColorObj(true);


        if (Input.GetKeyDown(KeyCode.DownArrow)) // Test
            ChangeColorObj(false);


    }

    //Init
    public void BigObjsInit()
    {
        if (GameObject.Find("RedG") == true)
            bigObjs[0] = GameObject.Find("RedG");
        if (GameObject.Find("GreenG") == true)
            bigObjs[1] = GameObject.Find("GreenG");

    }

    
    // Selected - color
    void ChangeColorObj(bool isOffSmaillObj)
    {
        Color orignColor;

        for (int i = 0; i < bigObjs.Length; i++)
        {
            if (i != curID)
            {
                MeshRenderer[] allMeshRenderers = bigObjs[i].GetComponentsInChildren<MeshRenderer>();
                orignColor = allMeshRenderers[i].material.color;
                float targetAlpha;

                if (isOffSmaillObj)
                    targetAlpha = 0.2f;
                else
                    targetAlpha = 1f;

                foreach (MeshRenderer mesh in allMeshRenderers)
                    mesh.material.color = new Color(orignColor.r, orignColor.g, orignColor.b, targetAlpha); // child color change to gray   
            }
        }
    }

    // Seleted - Mouse
    void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, bigLayerMask))
            {
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
        OnClickNum();
        rotatObjSub.OnClickNum();
        curID = name;
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

    void OnClickNum()
    {
        pastID = curID;
    }


    // Game End & Cul Correct : ?
    void DetectObjAllForEnd()
    {
        Vector3 direction = transform.forward;
        bool toGameEndPossibe;

        List<string> points = new();

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector3 origin = new(i, j, -5);

                int hitCount = Physics.RaycastNonAlloc(
                origin,
                direction,
                hitResults,  // 미리 할당된 배열을 여기에 전달
                rayDistance,
                forCulSamailObjLayerMask
                );

                if (hitCount > 0)
                {
                    Debug.Log($"총 {hitCount}개의 오브젝트가 감지되었습니다.");

                    // 감지된 오브젝트 배열을 반복문으로 순회
                    for (int x = 0; x < hitCount; x++)
                    {
                        // 충돌한 오브젝트의 이름과 거리를 출력
                        RaycastHit hit = hitResults[x];
                        if (hitCount % 2 == 1)
                        {
                            Debug.Log($"   감지된 오브젝트: {hit.collider.gameObject.name} (거리: {hit.distance:F2}) + is true");
                        }
                        else
                        {
                            points.Add(hit.collider.gameObject.name);
                            Debug.Log($"   감지된 오브젝트: {hit.collider.gameObject.name} (거리: {hit.distance:F2}) + is false");
                        }
                    }
                }
                else
                {
                    Debug.Log($"총 {hitCount}개의 오브젝트가 감지되었습니다. (위치: {i} , {j})");
                }
            }
        }

        toGameEndPossibe = points.Count <= 0;

        foreach (string item in points)
        {
            Debug.Log(item); 
        }
        print(toGameEndPossibe);
    }
}
