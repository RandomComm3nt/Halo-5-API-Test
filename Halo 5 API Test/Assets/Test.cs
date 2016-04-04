using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using Assets;

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        StartCoroutine(MakeRequest());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator MakeRequest()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
        WWW www = new WWW("https://www.haloapi.com/stats/h5/players/RandomComm3nt/matches?", null, headers);
        yield return www;

        JsonSerializerSettings isoDateSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };


        PlayerMatchList list = JsonConvert.DeserializeObject<PlayerMatchList>(www.text);
        Debug.Log(www.text);
    }
    

}