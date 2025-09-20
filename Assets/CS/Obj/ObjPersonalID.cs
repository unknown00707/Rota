using UnityEngine;

public class ObjPersonalID : MonoBehaviour
{
    public int targetAngle = 0;
    public int currentAngle = 0;
    public bool isSameAngle = false;

    void Update()
    {
        if (currentAngle != targetAngle)
            isSameAngle = false;
        else
            isSameAngle = true;
    }
}
