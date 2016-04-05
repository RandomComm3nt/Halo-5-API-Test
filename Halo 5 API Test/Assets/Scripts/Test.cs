using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using Assets.Scripts.Data;
using UnityEngine.UI;
using System;

namespace Assets.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject matchPrefab;

        void Start()
        {
            StartCoroutine(MakeRequest());
        }

        private IEnumerator MakeRequest()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
            WWW www = new WWW("https://www.haloapi.com/stats/h5/players/RandomComm3nt/matches?", null, headers);
            yield return www;
            
            PlayerMatchList list = JsonConvert.DeserializeObject<PlayerMatchList>(www.text);

            List<WWW> wwwList = new List<WWW>();
            list.matches = new List<MatchStats>();
            for (int i = 0; i < list.ResultCount; i++)
            {
                www = new WWW("https://www.haloapi.com/stats/h5/matches/" + list.Results[i].Id.MatchId.ToString() + "/events", null, headers);
                yield return www;
                list.matches.Add(JsonConvert.DeserializeObject<MatchStats>(www.text));
                
                Image image = Instantiate(matchPrefab).GetComponent<Image>();
                image.rectTransform.SetParent(transform, false);
                image.rectTransform.anchoredPosition = new Vector2((i % 5) * 120, -Mathf.Floor(i / 5f) * 120);
                Text text = image.GetComponentInChildren<Text>();

                if (list.matches[i] != null)
                {
                    int killCount = 0;
                    for (int j = 0; j < list.matches[i].GameEvents.Count; j++)
                    {
                        if (list.matches[i].GameEvents[j].Killer != null && list.matches[i].GameEvents[j].Killer.Gamertag == "RandomComm3nt")
                            killCount++;
                    }

                    text.text = list.Results[i].MatchCompletedDate.ISO8601Date.Substring(0, 10) + "\n" + "Kills: " + killCount.ToString();
                }
                else
                    text.text = list.Results[i].MatchCompletedDate.ISO8601Date.Substring(0, 10) + "\n" + "Can't load";
            }
        }
    }
}