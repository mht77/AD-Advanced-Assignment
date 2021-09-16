using System;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "PrimaryTypes/Int")]
    public class IntVar : ScriptableObject
    {
        public int Value;
        private const int defaultSize = 10;
        private const int minSize = 7;
        
        /// <summary>
        /// change table size on input value change event ( this is listener )
        /// </summary>
        /// <param name="value"> input value </param>
        public void SetValue(string value)
        {
            int size = defaultSize; 
            if (!String.IsNullOrEmpty(value))
                size = Convert.ToInt32(value);
            Value = size < minSize ? 10 : size;

        }
    }
}