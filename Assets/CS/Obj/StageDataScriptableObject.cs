using UnityEngine;
using System.Collections.Generic;

// 스테이지에 배치될 오브젝트의 정보 구조체
[System.Serializable]
public struct StageElement
{
    // 배치할 프리팹의 이름 (Resources에서 로드할 때 사용)
    public string prefabName; 
    // 배치할 위치 (2D 평면)
    public Vector2 position; 
    // 오브젝트의 고유 속성 (예: 회전 속도)
    public float rotationSpeed; 
}

// 이 클래스가 ScriptableObject 에셋 파일이 됩니다.
[CreateAssetMenu(fileName = "StageData", menuName = "GameData/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 고유 ID")]
    public int stageID;

    [Header("배치할 요소 목록")]
    // 이 리스트에 Inspector에서 요소들을 추가/편집합니다.
    public List<StageElement> elementsToSpawn; 

    // 필요하다면 배경 정보, 시간 제한 등 다른 데이터도 추가할 수 있습니다.
}
