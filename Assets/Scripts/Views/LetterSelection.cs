using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LetterSelection : MonoBehaviour
    {
        private Image BGImage;
        [SerializeField] private Color SelectColor;
        private void Awake()
        {
            BGImage = GetComponent<Image>();
            BGImage.color = Color.white;
            GetComponent<Button>().onClick.AddListener(SetColor);
        }

        private void SetColor()
        {
            if (BGImage.color==Color.white)
                BGImage.color = SelectColor;
            else if (BGImage.color==SelectColor)
                BGImage.color = Color.white;
        }
    }
}