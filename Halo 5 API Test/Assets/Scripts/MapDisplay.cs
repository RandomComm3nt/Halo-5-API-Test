using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Data;

namespace Assets.Scripts
{
    public class MapDisplay : MonoBehaviour
    {
        private MapMatchResults results;
        public MapMatchResults Results
        {
            get { return results; }

            set
            {
                results = value;
                nameText.text = results.map.name;
                countText.text = "Matches: " + results.ResultCount.ToString();
                image.sprite = results.map.Image;
            }
        }
        
        [SerializeField] private Text nameText;
        [SerializeField] private Text countText;
        [SerializeField] private Image image;
    }
}
