// 2025-08-02 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using System.Collections.Generic;

public class OutlineManager : MonoBehaviour
{
    // 특정 레이어에서 오브젝트를 찾는 메서드
    public List<GameObject> FindObjectsInLayer(LayerMask layer)
    {
        // 결과를 저장할 리스트
        List<GameObject> objectsInLayer = new List<GameObject>();

        // 씬에 존재하는 모든 GameObject를 가져옴
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("OutlineObjectTag");

        foreach (GameObject obj in allObjects)
        {
            // 오브젝트가 해당 레이어에 속하는지 확인
            if (((1 << obj.layer) & layer.value) != 0)
            {
                objectsInLayer.Add(obj);
                Debug.Log($"Found object: {obj.name} in layer {obj.layer}");
            }
        }

        return objectsInLayer;
    }

    // 테스트용 메서드
    public void TestFindObjectsInLayer()
    {
        // 예시: "Outline"이라는 레이어를 찾음
        LayerMask outlineLayer = LayerMask.NameToLayer("OutlineObjectLayer");

        if (outlineLayer == -1)
        {
            Debug.LogError("Layer 'Outline' does not exist!");
            return;
        }

        List<GameObject> foundObjects = FindObjectsInLayer(outlineLayer);

        //Debug.Log($"Total objects found in layer: {foundObjects.Count}");
    }

    // Unity의 Start 메서드에서 테스트 실행
    void Start()
    {
        TestFindObjectsInLayer();
    }
}