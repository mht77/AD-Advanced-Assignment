using UnityEngine;

namespace Views
{
    public class LetterId:MonoBehaviour
    {
        [HideInInspector]
        public int Id;

        [ContextMenu("Log Id")]
        private void LogId()
        {
            Debug.Log(Id);
        }
    }
}