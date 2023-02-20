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
                AudioManager.Instance.PlaySingleSound("Assets/ResHotfix/MainBundle/Audio/levelUp.wav");
                EventManager.Instance.Fire<LevelDeadEvent>(new LevelDeadEvent(true));
            }
        }
    }
}