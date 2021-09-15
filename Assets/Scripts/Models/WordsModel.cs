using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Words", menuName = "Models/Words")]
    public class WordsModel: ScriptableObject
    {
        public List<string> Words = new List<string>();
        [SerializeField] private bool isNumber;
        public event Action ModeChange;
        public bool IsNumber
        {
            get => isNumber;
            set
            {
                isNumber = value;
                OnModeChange();
            }
        }

        /// <summary>
        /// limit the number of total words
        /// </summary>
        private void OnEnable()
        {
            if (!File.Exists("Words.csv"))
                GetCSVFile();
            ModeChange += ReadCSV;
            IsNumber = false;
        }

        /// <summary>
        /// read words from a csv file with the specific name ("Words.csv")!
        /// </summary>
        [ContextMenu("Read CSV")]
        public void ReadCSV()
        {
            if (IsNumber)
                return;
            Words.Clear();
            try
            {
                var reader = new StreamReader("Words.csv");
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
                reader.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }

        /// <summary>
        /// get the csv file of words from github
        /// </summary>
        [ContextMenu("Get CSV file")]
        private void GetCSVFile()
        {
            HttpWebRequest request = 
                WebRequest.CreateHttp("https://raw.githubusercontent.com/mht77/AD-Advanced-Assignment/master/Words.csv");
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    StreamReader reader = new StreamReader(dataStream);
                    StreamWriter writer = new StreamWriter("Words.csv");
                    writer.WriteLine(reader.ReadLine());
                    reader.Close();
                    writer.Close();
                }
                response.Close();
            }
        }

        private void OnDisable()
        {
            ModeChange -= ReadCSV;
        }

        protected virtual void OnModeChange()
        {
            ModeChange?.Invoke();
        }
    }
}