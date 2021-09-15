using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LetterSelection : MonoBehaviour
    {
        private Image bgImage;
        private Color beforeColor;
        [SerializeField] private Color SelectColor;
        [SerializeField] private Color CorrectColor;
        
        /// <summary>
        /// 1) add listener actions for btn click event
        /// 2) set color and component obj
        /// </summary>
        private void Awake()
        {
            bgImage = GetComponent<Image>();
            bgImage.color = Color.white;
            beforeColor = Color.white;
            GetComponent<Button>().onClick.AddListener(Select);
            GetComponent<Button>().onClick.AddListener(WordCheck.Instance.CheckWord);
        }
        
        /// <summary>
        /// change color of the cell and update list of user selected cells
        /// </summary>
        private void Select()
        {
            if (bgImage.color == SelectColor)
            {
                WordCheck.Instance.UserSelectedLetters.Remove(GetComponent<LetterId>());
                bgImage.color = beforeColor;
                beforeColor = bgImage.color;
            }
            else
            {
                WordCheck.Instance.UserSelectedLetters.Add(GetComponent<LetterId>());
                bgImage.color = SelectColor;
            }
        }

        /// <summary>
        /// change cell color to correct color 
        /// </summary>
        public void SetCorrectColor()
        {
            bgImage.color = CorrectColor;
        }
        
    }
}