using Unity.Mathematics;
using UnityEngine;

public class RotatObjSub : MonoBehaviour
{
    public ObjSetting objSetting;
    public bool isRota = false;
    public int arr = 0;
    public float arrRota = 0;
    public int pastArr = 0;
    public int rotationSpeedAmount = 5; // 속
    public GameObject[] bigObjs; // Grounp OBj
    public int curID = 0;
    public int pastID = 0;

    void Awake()
    {
        objSetting = gameObject.GetComponent<ObjSetting>();
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

        if(Mathf.Abs(bigObjs[curID].GetComponent<ObjPersonalID>().targetAngle) - Mathf.Abs(bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle) > 0)
            arrRota = Mathf.Sign(bigObjs[curID].GetComponent<ObjPersonalID>().targetAngle);
        else
            arrRota = -Mathf.Sign(bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle);

        if (arrRota == 0)
            arrRota = arr;
        
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
                bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle += (int)arrRota * rotationSpeedAmount;
                bigObjs[curID].transform.rotation = Quaternion.Euler(0, 0, bigObjs[curID].GetComponent<ObjPersonalID>().currentAngle);
            }
        }

        if (!bigObjs[pastID].GetComponent<ObjPersonalID>().isSameAngle)
        {
            bigObjs[pastID].GetComponent<ObjPersonalID>().currentAngle += pastArr * rotationSpeedAmount;
            bigObjs[pastID].transform.rotation = Quaternion.Euler(0, 0, bigObjs[pastID].GetComponent<ObjPersonalID>().currentAngle);
        }
    }
    
    public void OnClickNum()
    {
        pastArr = (int)arrRota;
    }
}
