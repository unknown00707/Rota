using UnityEngine;

public class RotatObjSub : MonoBehaviour
{
    public bool isRota = false;
    public int arr = 0;
    public int rotationSpeedAmount = 25; // 속
    public ObjSetting objSetting;
    public GameObject[] bigObjs; // Grounp OBj
    public int curID = 0;

    void Awake()
    {
        bigObjs = objSetting.bigObjs;
    }
    void Update()
    {
        curID = objSetting.curID;
    }
    void FixedUpdate()
    {
        RotaCul();
    }
   // Rotation
    void OnEClick()
    {
        RoataObj(-1); // -1 mean right
    }

    void OnQClick()
    {
        RoataObj(1); // 1 mean left
    }

    void RoataObj(int arrValue)
    {
        arr = arrValue;
        bigObjs[curID].GetComponent<ObjPersonalID>().targetAngle += 90 * arr;
        isRota = true;
    }

    void RotaCul()
    {
        if (isRota)
        {
            if (bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle == bigObjs[curID].GetComponent<ObjPersonalID>().targetAngle)
                isRota = false;
            else if (bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle != bigObjs[curID].GetComponent<ObjPersonalID>().targetAngle)
            {
                bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle += arr * rotationSpeedAmount;
                bigObjs[curID].transform.rotation = Quaternion.Euler(0, 0, bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle);
            }
        }
    }
}
