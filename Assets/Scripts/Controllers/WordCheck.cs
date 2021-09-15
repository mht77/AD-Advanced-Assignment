using System.Collections.Generic;
using UnityEngine;
using Views;

namespace Controllers
{
    public static class WordCheck
    {
        public static readonly List<LetterId> UserSelectedLetters = new List<LetterId>();
        public static readonly Dictionary<string, List<LetterId>> WordsPosition = 
            new Dictionary<string, List<LetterId>>();
        
        /// <summary>
        /// check if the selected cells is a word
        /// </summary>
        public static void CheckWord()
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
                    foreach (var selectedLetter in UserSelectedLetters)
                    {
                        selectedLetter.transform.GetComponent<LetterSelection>().SetCorrectColor();
                    }
                }
            }
        }
    }
}