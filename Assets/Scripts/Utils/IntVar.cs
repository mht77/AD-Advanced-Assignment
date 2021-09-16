using System;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "PrimaryTypes/Int")]
    public class IntVar : ScriptableObject
    {
        public int Value;

        public void SetValue(string value)
        {
            if (!String.IsNullOrEmpty(value))
                Value = Convert.ToInt32(value);
        }
    }
}