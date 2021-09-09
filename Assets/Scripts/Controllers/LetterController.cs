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
        private List<int> filledRows = new List<int>();
        private List<int> filledColumns = new List<int>();
        private List<int> filledIds = new List<int>();
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

        private void Start()
        {
            SetOtherLetters();
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
            var cellId = FindCellVertically(wordSize);
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                filledIds.Add(cellId);
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponent<LetterId>());
                cellId += TableSize;
            }
            WordCheck.WordsPosition.Add(word, selectCellsId);
        }

        private int FindCellVertically(int wordSize)
        {
            bool available = true;
            int cellId, startColumn;
            while (true)
            {
                startColumn = Random.Range(1, TableSize + 1);
                while (filledColumns.Contains(startColumn))
                {
                    startColumn = Random.Range(1, TableSize + 1);
                }
                int startRow = Random.Range(0 + wordSize, TableSize + 2 - wordSize);
                cellId = GetSellId(startRow, startColumn);
                int startCell = cellId;
                for (int i = 0; i < wordSize; i++)
                {
                    if (filledIds.Contains(startCell))
                    {
                        available = false;
                    }
                    startCell += TableSize;
                }
                if (available)
                    break;
            }
            filledColumns.Add(startColumn);
            return cellId;
        }

        private void HorizontalSet(string word)
        {
            int wordSize = word.Length;
            var cellId = FindCellHorizontally(wordSize);
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                filledIds.Add(cellId);
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponent<LetterId>());
                cellId++;
            }
            WordCheck.WordsPosition.Add(word, selectCellsId);
        }

        private int FindCellHorizontally(int wordSize)
        {
            bool available = true;
            int startRow, cellId;
            while (true)
            {
                int startColumn = Random.Range(0 + wordSize, TableSize + 2 - wordSize);
                startRow = Random.Range(1, TableSize + 1); //random numbers are between 1, TableSize
                while (filledRows.Contains(startRow))
                {
                    startRow = Random.Range(1, TableSize + 1);
                }
                cellId = GetSellId(startRow, startColumn);
                Debug.Log(cellId);
                int startCell = cellId;
                for (int i = 0; i < wordSize; i++)
                {
                    if (filledIds.Contains(startCell))
                    {
                        available = false;
                    }
                    startCell++;
                }
                if (available)
                    break;
            }
            Debug.Log(cellId);
            filledRows.Add(startRow);
            return cellId;
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