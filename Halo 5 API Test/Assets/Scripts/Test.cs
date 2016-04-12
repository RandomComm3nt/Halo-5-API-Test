//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;
//using Newtonsoft.Json;
//using Assets.Scripts.Data;
//using UnityEngine.UI;
//using System.IO;
//using Unity.IO.Compression;
//using System.Text;

//namespace Assets.Scripts
//{
//    public class Test : MonoBehaviour
//    {
//        [SerializeField] private GameObject matchPrefab;
//        [SerializeField] private EventRenderer eventRenderer;


//        void Start()
//        {

//            LoadMaps();
//        }

//        public void LoadMaps()
//        {
//            StartCoroutine(MakeMapRequest());
//        }

//        private IEnumerator MakeMapRequest()
//        {
//            Dictionary<string, string> headers = new Dictionary<string, string>();
//            headers.Add("Ocp-Apim-Subscription-Key", "897acd51b3cd45acbeadeb07d0e72afb");
//            WWW www = new WWW("https://www.haloapi.com/metadata/h5/metadata/maps?", null, headers);
//            yield return www;
//            List<ApiMapData> list;

//        }

        
//    }
//}