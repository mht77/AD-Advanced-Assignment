using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LetterSelection : MonoBehaviour
    {
        private Image BGImage;
        private Color beforeColor;
        [SerializeField] private Color SelectColor;
        [SerializeField] private Color CorrectColor;
        private void Awake()
        {
            BGImage = GetComponent<Image>();
            BGImage.color = Color.white;
            beforeColor = Color.white;
            GetComponent<Button>().onClick.AddListener(Select);
            GetComponent<Button>().onClick.AddListener(WordCheck.CheckWord);
        }

        private void Select()
        {
            if (BGImage.color == SelectColor)
            {
                WordCheck.UserSelectedLetters.Remove(GetComponent<LetterId>());
                BGImage.color = beforeColor;
                beforeColor = BGImage.color;
            }
            else
            {
                WordCheck.UserSelectedLetters.Add(GetComponent<LetterId>());
                BGImage.color = SelectColor;
            }
        }

        public void SetCorrectColor()
        {
            BGImage.color = CorrectColor;
        }
        
    }
}