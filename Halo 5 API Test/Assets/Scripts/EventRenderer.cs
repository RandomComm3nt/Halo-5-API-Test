using UnityEngine;
using Assets.Scripts.Data;
using System.Collections;

public class EventRenderer : MonoBehaviour
{
    [SerializeField] GameObject playerKillPrefab;
    [SerializeField] GameObject playerDeathPrefab;
    [SerializeField] GameObject playerAssistPrefab;

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
        arrow.transform.localPosition = start;
        arrow.transform.localScale = new Vector3(Vector3.Magnitude(end - start), 1, 1);
        arrow.transform.localRotation = Quaternion.LookRotation(end - start, Vector3.up);
    }

    public void Clear()
    {

    }
}
