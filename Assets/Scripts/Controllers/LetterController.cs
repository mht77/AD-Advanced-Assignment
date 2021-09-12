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
        private readonly List<int> filledRows = new List<int>();
        private readonly List<int> filledColumns = new List<int>();
        private readonly List<int> filledIds = new List<int>();
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
                bool res = false;
                do
                {
                    var vOrh = Random.Range(0, 2);
                    if (vOrh == 0) // means horizontal
                        res = HorizontalSet(word);
                    else if (vOrh==1)
                        res = VerticalSet(word);
                } while (!res);
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

        private bool VerticalSet(string word)
        {
            int wordSize = word.Length;
            var cellId = FindCellVertically(wordSize);
            if (cellId == -1)
                return false;
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                filledIds.Add(cellId);
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponentInParent<LetterId>());
                cellId += TableSize;
            }
            foreach (var cell in selectCellsId)
            {
                Debug.Log(cell.Id);
            }
            WordCheck.WordsPosition.Add(word, selectCellsId);
            return true;
        }

        private int FindCellVertically(int wordSize)
        {
            bool available = true;
            var startColumn = Random.Range(1, TableSize + 1);
            while (filledColumns.Contains(startColumn))
            {
                startColumn = Random.Range(1, TableSize + 1);
            }
            int startRow = Random.Range(0 + wordSize, TableSize + 2 - wordSize);
            var cellId = GetSellId(startRow, startColumn);
            int startCell = cellId;
            for (int i = 0; i < wordSize; i++)
            {
                if (filledIds.Contains(startCell))
                {
                    available = false;
                }
                startCell += TableSize;
            }
            if (!available)
                return -1;
            filledColumns.Add(startColumn);
            return cellId;
        }

        private bool HorizontalSet(string word)
        {
            int wordSize = word.Length;
            var cellId = FindCellHorizontally(wordSize);
            if (cellId == -1)
                return false;
            List<LetterId> selectCellsId = new List<LetterId>();
            for (int i = 0; i < wordSize; i++)
            {
                lettersTexts[cellId].text = word[i].ToString();
                filledIds.Add(cellId);
                selectCellsId.Add(lettersTexts[cellId].transform.GetComponentInParent<LetterId>());
                cellId++;
            }
            WordCheck.WordsPosition.Add(word, selectCellsId);
            return true;
        }

        private int FindCellHorizontally(int wordSize)
        {
            bool available = true;
            int startRow, cellId;
            int startColumn = Random.Range(0 + wordSize, TableSize + 2 - wordSize);
                startRow = Random.Range(1, TableSize + 1); //random numbers are between 1, TableSize
            while (filledRows.Contains(startRow))
            {
                startRow = Random.Range(1, TableSize + 1);
            }
            cellId = GetSellId(startRow, startColumn);
            int startCell = cellId;
            for (int i = 0; i < wordSize; i++)
            {
                if (filledIds.Contains(startCell))
                {
                    available = false;
                }
                startCell++;
            }
            if (!available)
                return -1;
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