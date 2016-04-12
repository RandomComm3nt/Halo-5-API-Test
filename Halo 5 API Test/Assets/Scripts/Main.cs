using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Api;
using Newtonsoft.Json;
using System;
using System.IO;
using Unity.IO.Compression;
using System.Text;

namespace Assets.Scripts
{

    public class Main : MonoBehaviour
    {
        [SerializeField] private MapDataList mapDataList;
        private Dictionary<SerializableGuid, MapMatchResults> mapMatchResults;
        private int seasonMatches;
        DateTime seasonStart;

        private void Start()
        {
            StartCoroutine(LoadSeasonInfo());
            //StartCoroutine(LoadMapInfo());
        }

        private IEnumerator LoadSeasonInfo()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
            WWW www = new WWW("https://www.haloapi.com/metadata/h5/metadata/seasons", null, headers);
            yield return www;

            List<Season> seasons = JsonConvert.DeserializeObject<List<Season>>(www.text);
            seasonStart = DateTime.Parse(seasons[seasons.Count - 1].startDate);
        }

        private IEnumerator LoadMapInfo()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
            WWW www = new WWW("https://www.haloapi.com/metadata/h5/metadata/maps?", null, headers);
            yield return www;
            List<MapMetadata> list = JsonConvert.DeserializeObject<List<MapMetadata>>(DecodeGzip(www.bytes));
            Debug.Log("Maps loaded");
        }

        public void SubmitGamertag()
        {
            StartCoroutine(FindSeasonStart());
        }

        private IEnumerator FindSeasonStart()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            
            int min = 0;
            int stepSize = 64;
            int max = stepSize;
            while (min != max)
            {
                Debug.Log(min);
                Debug.Log(max);
                WWW www = new WWW("https://www.haloapi.com/stats/h5/players/RandomComm3nt/matches?modes=arena&start=" + max.ToString() + "&count=1", null, headers);
                yield return www;
                MatchResultList list = JsonConvert.DeserializeObject<MatchResultList>(www.text, settings);
                if (list.ResultCount == 0 || DateTime.Parse(list.Results[0].MatchCompletedDate.ISO8601Date).CompareTo(seasonStart) <= 0)
                {
                    // too far
                    max = min + Mathf.FloorToInt(stepSize * 0.5f);
                    stepSize = Mathf.CeilToInt(stepSize * 0.25f);
                }
                else
                {
                    // not far enough
                    min = max;
                    max += stepSize;
                }
            }
            seasonMatches = min;
            Debug.Log("Matches: " + seasonMatches.ToString());
            StartCoroutine(LoadSeasonMatches());
        }

        private IEnumerator LoadSeasonMatches()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");

            List<MatchResult> results = new List<MatchResult>();

            for (int i = 0; i < seasonMatches;)
            {
                int matches = (i < seasonMatches - 25 ? 25 : seasonMatches - i);

                WWW www = new WWW("https://www.haloapi.com/stats/h5/players/RandomComm3nt/matches?modes=arena&start=" + i.ToString() + "&count=" + matches.ToString(), null, headers);
                yield return www;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                results.AddRange(JsonConvert.DeserializeObject<MatchResultList>(www.text, settings).Results);
                
                i += 25;
            }

            SortMatchesToMaps(results);
        }

        private void SortMatchesToMaps(List<MatchResult> results)
        {
            // set up map result list
            mapMatchResults = new Dictionary<SerializableGuid, MapMatchResults>();
            for (int i = 0; i < mapDataList.mapData.Count; i++)
            {
                MapMatchResults map = new MapMatchResults();
                map.map = mapDataList.mapData[i];
                mapMatchResults.Add(mapDataList.mapData[i].Guid, map);
            }

            // sort matches
            for (int i = 0; i < results.Count; i++)
            {
                try
                {
                    mapMatchResults[results[i].MapId].AddMatch(results[i]);
                }
                catch (KeyNotFoundException)
                {
                    Debug.Log("Map key not found.");
                }
            }

            Debug.Log("Sorted!");
        }

        private string DecodeGzip(byte[] array)
        {
            // taken and adapted from stackexchange
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(array, 0, array.Length);

                // this is bad, need a way to get correct length
                var buffer = new byte[array.Length * 4];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        //private void LoadMatchStats()
        //{
        //    List<WWW> wwwList = new List<WWW>();
        //    list.matches = new List<MatchStats>();
        //    for (int i = 0; i < list.ResultCount; i++)
        //    {
        //        www = new WWW("https://www.haloapi.com/stats/h5/matches/" + list.Results[i].Id.MatchId.ToString() + "/events", null, headers);
        //        yield return www;
        //        list.matches.Add(JsonConvert.DeserializeObject<MatchStats>(www.text));

        //        Image image = Instantiate(matchPrefab).GetComponent<Image>();
        //        image.rectTransform.SetParent(transform, false);
        //        image.rectTransform.anchoredPosition = new Vector2((i % 5) * 120, -50 - Mathf.Floor(i / 5f) * 120);
        //        Text text = image.GetComponentInChildren<Text>();

        //        if (list.matches[i] != null)
        //        {
        //            eventRenderer.DrawMatchArrows(list.matches[0]);
        //            int killCount = 0;
        //            for (int j = 0; j < list.matches[i].GameEvents.Count; j++)
        //            {
        //                Debug.Log("new game");
        //                if (list.matches[i].GameEvents[j].Killer != null && list.matches[i].GameEvents[j].Killer.Gamertag == "RandomComm3nt")
        //                {
        //                    killCount++;
        //                    Debug.Log(list.matches[i].GameEvents[j].KillerWorldLocation);
        //                    Debug.Log(list.matches[i].GameEvents[j].VictimWorldLocation);
        //                }
        //            }

        //            text.text = list.Results[i].MatchCompletedDate.ISO8601Date.Substring(0, 10) + "\n" + "Kills: " + killCount.ToString();
        //        }
        //        else
        //            text.text = list.Results[i].MatchCompletedDate.ISO8601Date.Substring(0, 10) + "\n" + "Can't load";
        //    }
        //}
    }
}