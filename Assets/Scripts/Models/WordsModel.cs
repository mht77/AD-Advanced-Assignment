using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Words", menuName = "Models/Words")]
    public class WordsModel: ScriptableObject
    {
        public List<string> Words = new List<string>();
        
        /// <summary>
        /// limit the number of total words
        /// </summary>
        private void Awake()
        {
            if (Words.Count>20) Words.RemoveRange(20,Words.Count-20);
        }
    }
}