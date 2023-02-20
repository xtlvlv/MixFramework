using System;
using UnityEngine;

namespace ScriptsHotfix
{
    public class LevelTrigger: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("TagPlayer"))
            {
                EventManager.Instance.Fire<LevelDeadEvent>(new LevelDeadEvent(false));
            }
        }
    }
}