using Models;
using UnityEngine;

namespace Controllers
{
    public class ThemeController : MonoBehaviour
    {
        [SerializeField] private WordsModel WordsModel;
        [SerializeField] private GameObject Tick;

        private void OnEnable()
        {
            WordsModel.ModeChange += TickActivation;
        }

        private void OnDisable()
        {
            WordsModel.ModeChange -= TickActivation;
        }

        
        /// <summary>
        /// activate or deactivate the tick obj according to user input
        /// </summary>
        private void TickActivation()
        {
            Tick.SetActive(WordsModel.IsNumber);
        }

        /// <summary>
        /// change to numbers instead of words and reverse!
        /// </summary>
        /// <param name="wordsNumber"> No. numbers in the table </param>
        public void ChangeToNumbers(int wordsNumber)
        {
            if (WordsModel.IsNumber)
            {
                WordsModel.IsNumber = false;
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
            }
        }
    }
}