using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Words", menuName = "Models/Words")]
    public class WordsModel: ScriptableObject
    {
        public List<string> Words = new List<string>();

        public bool IsNumber;
        
        /// <summary>
        /// limit the number of total words
        /// </summary>
        private void Awake()
        {
            if (Words.Count>20) Words.RemoveRange(20,Words.Count-20);
        }

        /// <summary>
        /// read words from a csv file with the specific name!
        /// </summary>
        [ContextMenu("Read CSV")]
        public void ReadCSV()
        {
            Words.Clear();
            var reader = new StreamReader("Words.csv");
            try
            {
                char[] comma = {','};
                string[] splitValues = null;
                var values = reader.ReadLine();
                if (values != null) splitValues = values.Split(comma, StringSplitOptions.None);
                if (splitValues != null)
                    foreach (var value in splitValues)
                    {
                        var valueWithoutSpace = value.Replace(" ", "");
                        Words.Add(valueWithoutSpace.ToLower());
                    }
                Debug.Log("CSV Done");
                IsNumber = false;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                reader.Close();
            }

        }
    }
}