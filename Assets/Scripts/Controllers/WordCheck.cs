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

        public static void CheckWord()
        {
            foreach (var word in WordsPosition)
            {
                Debug.Log(word.Key);
            }
            foreach (var letter in UserSelectedLetters)
            {
                Debug.Log(letter.Id);
            }
            foreach (var wordPosition in WordsPosition)
            {
                if (wordPosition.Value == UserSelectedLetters)
                {
                    foreach (var letter in UserSelectedLetters)
                    {
                        letter.transform.GetComponent<LetterSelection>().SetCorrectColor();
                    }
                }
            }
        }
    }
}