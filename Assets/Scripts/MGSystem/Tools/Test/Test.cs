using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame.MGSystem
{
    public class Test : MonoBehaviour
    {
        [Tooltip("the level to load after the start screen")]
        public string NextLevel;

        public virtual void JumpToNextLevel()
        {
            SceneManager.LoadScene(NextLevel);
        }
    }
}
