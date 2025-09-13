using UnityEngine;

public class OnCamera : MonoBehaviour
{
    public GameObject[] onSiderCamera;
    private int curCameraNum = 0;

    void Awake()
    {
        curCameraNum = 0;
        CameraSet();
    }

    void OnClickA()
    {
        curCameraNum--;
        CameraSet();
    }

    void OnClickD()
    {
        curCameraNum++;
        CameraSet();
    }

    void CameraSet()
    {
        if (curCameraNum <= -1)
            curCameraNum = onSiderCamera.Length - 1;

        if (curCameraNum >= onSiderCamera.Length)
            curCameraNum = 0;
        

        foreach (GameObject obj in onSiderCamera)
        {
            obj.SetActive(false);
        }

        onSiderCamera[curCameraNum].SetActive(true);
    }
}
