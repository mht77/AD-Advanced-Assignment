using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Controllers
{
    public class LetterController : MonoBehaviour
    {
        public GameObject LetterHolder;
        public WordsModel WordsModel;
        private List<Text> lettersTexts; 
        [SerializeField] private int TableSize;
        public Dictionary<string, List<LetterId>> WordsPosition = new Dictionary<string, List<LetterId>>();
        private void Awake()
        {
            lettersTexts = new List<Text>(LetterHolder.GetComponentsInChildren<Text>());
            int counter = 0;
            foreach (var letterId in LetterHolder.transform.GetComponentsInChildren<LetterId>())
            {
                letterId.Id = counter;
                counter++;
            }
            PutWords();
        }

        private void PutWords()
        {
            foreach (var word in WordsModel.Words)
            {
                var vOrh = Random.Range(0, 2);
                if (vOrh == 0) // means horizontal
                    HorizontalSet(word);
                else if (vOrh==1)
                    VerticalSet(word);
            }
            SetOtherLetters();
        }

        private void SetOtherLetters()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            foreach (var letter in lettersTexts)
            {
                if (letter.text == (-1).ToString())
                    letter.text = chars[Random.Range(0, chars.Length)].ToString();
            }
        }

        private void VerticalSet(string word)
        {
            int wordSize = word.Length;
            int startColumn = Random.Range(1,TableSize+1);
            int startRow = Random.Range(0+wordSize, TableSize+2-wordSize);
            int cellId = GetSellId(startRow, startColumn);
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponent<LetterId>());
                cellId += TableSize;
            }
            WordsPosition.Add(word, selectCellsId);
        }

        private void HorizontalSet(string word)
        {
            int wordSize = word.Length;
            int startColumn = Random.Range(0+wordSize, TableSize+2-wordSize); 
            int startRow = Random.Range(1,TableSize+1); //random numbers are between 1, TableSize
            int cellId = GetSellId(startRow, startColumn);
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponent<LetterId>());
                cellId++;
            }
            WordsPosition.Add(word, selectCellsId);
        }

        private int GetSellId(int row, int column)
        {
            int res = 0;
            res += (row-1) * TableSize;
            res += column - 1;
            return res;
        }
    }
}