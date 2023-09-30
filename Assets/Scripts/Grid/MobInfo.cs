using UnityEngine;

public class MobInfo : MonoBehaviour
{
    public int level;
    public GameObject nextLevelPrefab;

    public MobType mobType;
}

public enum MobType
{
    melee,
    range
}