using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PenaltyKick
{
    public class UIManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPlayerButton(int numberOfPlayers)
        {
            SceneManager.LoadScene("PenaltyKick");
            PlayerPrefs.SetInt("NumberOfPlayers", numberOfPlayers);
        }

    }
}
