using UnityEngine;

public class RotatObjSub : MonoBehaviour
{
    public bool isRota = false;
    public int arr = 0;
    public int pastArr = 0;
    public int rotationSpeedAmount = 5; // 속
    public ObjSetting objSetting;
    public GameObject[] bigObjs; // Grounp OBj
    public int curID = 0;
    public int pastID = 0;

    void Awake()
    {
        bigObjs = objSetting.bigObjs;
    }
    void Update()
    {
        curID = objSetting.curID;
        pastID = objSetting.pastID;
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
            if (bigObjs[curID].GetComponent<ObjPersonalID>().isSameAngle)
                isRota = false;
            else if (!bigObjs[curID].GetComponent<ObjPersonalID>().isSameAngle)
            {
                bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle += arr * rotationSpeedAmount;
                bigObjs[curID].transform.rotation = Quaternion.Euler(0, 0, bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle);
            }
        }

        if (!bigObjs[pastID].GetComponent<ObjPersonalID>().isSameAngle)
        {
            bigObjs[pastID].GetComponent<ObjPersonalID>().currentAngle += pastArr * rotationSpeedAmount;
            bigObjs[pastID].transform.rotation = Quaternion.Euler(0, 0, bigObjs[pastID].GetComponent<ObjPersonalID>().currentAngle);
        }
    }
    
    void OnClickNum()
    {
        pastArr = arr;
    }
}
