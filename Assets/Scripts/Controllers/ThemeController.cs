using Models;
using UnityEngine;

namespace Controllers
{
    public class ThemeController : MonoBehaviour
    {
        [SerializeField] private WordsModel WordsModel;
        [SerializeField] private GameObject Tick;
        
        /// <summary>
        /// change to numbers instead of words and reverse!
        /// </summary>
        /// <param name="wordsNumber"> No. numbers in the table </param>
        public void ChangeToNumbers(int wordsNumber)
        {
            if (WordsModel.IsNumber)
            {
                WordsModel.ReadCSV();
                Tick.SetActive(false);
            }
            else
            {
                WordsModel.Words.Clear();
                for (int i = 0; i < wordsNumber; i++)
                {
                    var number = Random.Range(1000, 999999);
                    WordsModel.Words.Add(number.ToString());
                }
                WordsModel.IsNumber = true;
                Tick.SetActive(true);
            }
        }
    }
}