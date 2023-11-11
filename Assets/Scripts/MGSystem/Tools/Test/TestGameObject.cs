using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class TestGameObject : MonoBehaviour
    {
        [Header("Debug")]
        [RGInspectorButton("TriggerGameOver")]
        public bool TriggerGameOverButton;
        [RGInspectorButton("TriggerPauseGame")]
        public bool TriggerPauseGameButton;

        protected virtual void TriggerGameOver()
        {
            RGGameEvent.Trigger("GameOver");
        }
        protected virtual void TriggerPauseGame()
        {
            RGGameEvent.Trigger("PauseGame");
        }
    }
}
