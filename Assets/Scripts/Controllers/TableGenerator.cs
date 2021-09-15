using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Controllers
{
    public class TableGenerator : MonoBehaviour
    {
        [SerializeField] private IntVar TableSize;
        [SerializeField] private GameObject Cell;
        [SerializeField] private GameObject TableParent;
        
        private void Awake()
        {
            GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            GetComponent<GridLayoutGroup>().constraintCount = TableSize.Value;
            for (int i = 0; i < (int)Mathf.Pow(TableSize.Value,2); i++)
            {
                Instantiate(Cell, TableParent.transform);
            }
        }
    }
}