using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseMeleeMob;
    [SerializeField] private GameObject baseRangeMob;
    [SerializeField] private Cell[] playerCells;

    public static MobsSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnMeleeMob()
    {
        var cell = FindFreeCell();
        if (cell != null)
        {
            Vector3 pos = cell.GetComponent<Collider>().bounds.center;
            var mob = Instantiate(baseMeleeMob, pos, Quaternion.identity);

            cell.mob = mob;
            cell.isFree = false;
            
        }
    }

    public void SpawnRangeMob()
    {
        var cell = FindFreeCell();
        if (cell != null)
        {
            Vector3 pos = cell.GetComponent<Collider>().bounds.center;
            var mob = Instantiate(baseRangeMob, pos, Quaternion.identity);

            cell.mob = mob;
            cell.isFree = false;
        }
    }

    private Cell FindFreeCell()
    {
        for (int i = 0; i < playerCells.Length; i++)
        {
            if (playerCells[i].isFree)
            {
                return playerCells[i];
            }
        }

        return null;
    }
}
