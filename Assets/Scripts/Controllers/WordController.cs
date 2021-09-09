using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class WordController : MonoBehaviour
    {
        public WordsModel WordsModel;
        [SerializeField] private GameObject WordsTextHolder;
        [SerializeField] private GameObject WordText;
        private void Awake()
        {
            foreach (var word in WordsModel.Words)
            {
                var wordText = Instantiate(WordText, WordsTextHolder.transform);
                wordText.GetComponent<Text>().text = word;
            }
        }
        
    }
}