using UnityEngine;
using Assets.Scripts.Data;
using System.Collections;

public class EventRenderer : MonoBehaviour
{
    public static EventRenderer singleton;

    [SerializeField] GameObject playerKillPrefab;
    [SerializeField] GameObject playerDeathPrefab;
    [SerializeField] GameObject playerAssistPrefab;

    private void Start()
    {
        if (singleton != null)
            Destroy(singleton);
        singleton = this;
    }

    public void DrawMatchArrows(MatchStats stats)
    {
        stats.FindPlayerEvents();
        for (int i = 0; i < stats.playerKills.Count; i++)
        {
            GameObject arrow = Instantiate(playerKillPrefab);
            arrow.transform.SetParent(transform, false);
            SetArrow(arrow, stats.playerKills[i].KillerWorldLocation, stats.playerKills[i].VictimWorldLocation);
        }
    }

    public void SetArrow(GameObject arrow, Vector3 start, Vector3 end)
    {
        Debug.Log("delta: " + (end - start).ToString());
        arrow.transform.localPosition = new Vector3(start.x, start.z, start.y);
        arrow.transform.localScale = new Vector3(1, 1, Vector3.Magnitude(end - start));
        arrow.transform.localRotation = Quaternion.LookRotation(new Vector3(end.x - start.x, end.z - start.z, end.y - start.y), Vector3.up);
    }

    public void Clear()
    {

    }
}
