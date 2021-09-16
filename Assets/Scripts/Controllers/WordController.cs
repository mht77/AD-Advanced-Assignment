using System.Collections.Generic;
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
        public static readonly Dictionary<string, Image> WordImages = new Dictionary<string, Image>();
        [SerializeField] private GameObject BackToMenuBtn;
        /// <summary>
        /// set text of sidebar for words in the table
        /// </summary>
        private void Awake()
        {
            WordCheck.Instance.TableComplete += OnTableComplete;
            WordImages.Clear();
            foreach (var word in WordsModel.Words)
            {
                var wordText = Instantiate(WordText, WordsTextHolder.transform);
                wordText.GetComponent<Text>().text = word;
                WordImages.Add(word, wordText.transform.GetComponentInChildren<Image>());
            }
        }

        private void OnDisable()
        {
            WordCheck.Instance.TableComplete -= OnTableComplete;
        }

        private void OnTableComplete()
        {
            BackToMenuBtn.SetActive(false);
        }
    }
}