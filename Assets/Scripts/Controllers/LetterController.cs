using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Views;

namespace Controllers
{
    public class LetterController : MonoBehaviour
    {
        public GameObject LetterHolder;
        public WordsModel WordsModel;
        private List<Text> lettersTexts; 
        [SerializeField] private IntVar TableSize;
        private readonly List<int> filledRows = new List<int>();
        private readonly List<int> filledColumns = new List<int>();
        private readonly List<int> filledIds = new List<int>();
        
        /// <summary>
        /// get components and raise put words method
        /// </summary>
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

        /// <summary>
        /// randomize words in 2 groups of vertical and horizontal
        /// </summary>
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
        /// <summary>
        /// set a char for remaining cells
        /// </summary>
        private void SetOtherLetters()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            foreach (var letter in lettersTexts)
            {
                if (letter.text == (-1).ToString())
                    letter.text = chars[Random.Range(0, chars.Length)].ToString();
            }
        }
        /// <summary>
        /// set chars of a word in the table vertically
        /// </summary>
        /// <param name="word"> the word to be put in table</param>
        /// <returns>whether if vertical placement is not possible or not (for a randomize column number)</returns>
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
                cellId += TableSize.Value;
            }
            WordCheck.Instance.WordsPosition.Add(word, selectCellsId);
            return true;
        }
        /// <summary>
        /// find a suitable cell for starting a word placement vertically
        /// </summary>
        /// <param name="wordSize"> length of the word</param>
        /// <returns>the suitable cell id or -1 if it is not possible(for that random column number)</returns>
        private int FindCellVertically(int wordSize)
        {
            bool available = true;
            var startColumn = Random.Range(1, TableSize.Value + 1);
            while (filledColumns.Contains(startColumn))
            {
                startColumn = Random.Range(1, TableSize.Value + 1);
            }
            int startRow = Random.Range(0 + wordSize, TableSize.Value + 2 - wordSize);
            var cellId = GetSellId(startRow, startColumn);
            int startCell = cellId;
            for (int i = 0; i < wordSize; i++)
            {
                if (filledIds.Contains(startCell))
                {
                    available = false;
                }
                startCell += TableSize.Value;
            }
            if (!available)
                return -1;
            filledColumns.Add(startColumn);
            return cellId;
        }
        /// <summary>
        /// set chars of a word in the table horizontally
        /// </summary>
        /// <param name="word"> the word to be put in table</param>
        /// <returns>whether if horizontal placement is not possible or not</returns>
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
            WordCheck.Instance.WordsPosition.Add(word, selectCellsId);
            return true;
        }
        /// <summary>
        /// find a suitable cell for starting a word placement horizontally
        /// </summary>
        /// <param name="wordSize"> length of the word</param>
        /// <returns>the suitable cell id or -1 if it is not possible(for that random row number)</returns>
        private int FindCellHorizontally(int wordSize)
        {
            bool available = true;
            int startColumn = Random.Range(0 + wordSize, TableSize.Value + 2 - wordSize);
                var startRow = Random.Range(1, TableSize.Value + 1);
            while (filledRows.Contains(startRow))
            {
                startRow = Random.Range(1, TableSize.Value + 1);
            }
            var cellId = GetSellId(startRow, startColumn);
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
            filledRows.Add(startRow);
            return cellId;
        }
        
        /// <summary>
        /// calculate cell id of given row and column number
        /// </summary>
        /// <param name="row"> the row number</param>
        /// <param name="column"> the column number</param>
        /// <returns> cell id </returns>
        private int GetSellId(int row, int column)
        {
            int res = 0;
            res += (row-1) * TableSize.Value;
            res += column - 1;
            return res;
        }
    }
}