// 2025-08-02 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public Material outlineMaterial; // 아웃라인 효과를 위한 Material
    public string outlineTag = "OutlineObject"; // 아웃라인 효과를 적용할 태그
    public LayerMask outlineLayer; // 아웃라인 효과를 적용할 레이어

    private Renderer[] renderers;

    void Start()
    {
        // 태그를 기준으로 오브젝트 찾기
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(outlineTag);

        foreach (GameObject obj in objectsWithTag)
        {
            ApplyOutline(obj);
        }

        // 레이어를 기준으로 오브젝트 찾기
        foreach (GameObject obj in FindObjectsInLayer(outlineLayer))
        {
            ApplyOutline(obj);
        }
    }

    void ApplyOutline(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // 기존 Material 배열에 아웃라인 Material 추가
            Material[] materials = renderer.materials;
            Material[] newMaterials = new Material[materials.Length + 1];
            materials.CopyTo(newMaterials, 0);
            newMaterials[materials.Length] = outlineMaterial;
            renderer.materials = newMaterials;
        }
    }

    GameObject[] FindObjectsInLayer(LayerMask layer)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        System.Collections.Generic.List<GameObject> objectsInLayer = new System.Collections.Generic.List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (((1 << obj.layer) & layer) != 0)
            {
                objectsInLayer.Add(obj);
            }
        }

        return objectsInLayer.ToArray();
    }
}
