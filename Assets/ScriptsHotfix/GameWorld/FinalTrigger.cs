using System;
using UnityEngine;

namespace ScriptsHotfix
{
    public class FinalTrigger: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("TagPlayer"))
            {
                AudioManager.Instance.StopSingleSound();
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySingleSound("MainBundle/Audio/levelUp");
                EventManager.Instance.Fire<LevelDeadEvent>(new LevelDeadEvent(true));
            }
        }
    }
}