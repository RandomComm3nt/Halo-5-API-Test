using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class MapDisplayScreen : MonoBehaviour
    {
        [SerializeField] private GameObject mapDisplayPrefab;
        [SerializeField] private RectTransform scrollPaneContent;

        private List<MapDisplay> mapDisplays;
      

        public void Initialise(Main main)
        {
            if (mapDisplays != null)
            {

            }
            mapDisplays = new List<MapDisplay>();
            foreach (var map in main.mapMatchResults.Values)
            {
                MapDisplay mapDisp = Instantiate(mapDisplayPrefab).GetComponent<MapDisplay>();
                mapDisp.Results = map;
                mapDisplays.Add(mapDisp);
                mapDisp.transform.SetParent(scrollPaneContent, false);
            }
        }
    }
}