using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "PrimaryTypes/Int")]
    public class IntVar : ScriptableObject
    {
        public int Value;
    }
}