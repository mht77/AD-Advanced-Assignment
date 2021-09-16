using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.SingleTones;
using Views;

namespace Controllers
{
    public class WordCheck: MonoSingleton<WordCheck>
    {
        public readonly List<LetterId> UserSelectedLetters = new List<LetterId>();
        public readonly Dictionary<string, List<LetterId>> WordsPosition = 
            new Dictionary<string, List<LetterId>>();

        private int noCorrectWords;
        public event Action TableComplete;
        [SerializeField] private GameObject FinishPanel;
        
        
        /// <summary>
        /// check if the selected cells is a word
        /// </summary>
        public void CheckWord()
        {
            foreach (var wordPosition in WordsPosition)
            {
                bool check = true;
                foreach (var letter in wordPosition.Value)
                {
                    if (!UserSelectedLetters.Contains(letter))
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    Debug.Log("Correct!");
                    noCorrectWords++;
                    WordController.WordImages[wordPosition.Key].color = new Color(0,1f,0,0.5f);
                    foreach (var selectedLetter in UserSelectedLetters)
                    {
                        selectedLetter.transform.GetComponent<LetterSelection>().SetCorrectColor();
                    }
                    UserSelectedLetters.Clear();
                    CheckFinish();
                }
            }
        }
        
        /// <summary>
        /// check if all words has been found!
        /// </summary>
        private void CheckFinish()
        {
            if (noCorrectWords == WordsPosition.Count)
            {
                OnTableComplete();
                FinishPanel.SetActive(true);
            }
        }

        protected virtual void OnTableComplete()
        {
            TableComplete?.Invoke();
        }
    }
}