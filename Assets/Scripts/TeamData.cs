using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PenaltyKick
{
    [Serializable]
    public class TeamData
    {
        public int TeamId;
        public string Name;
        public GameObject PlayerPrefab;
        public GameObject AIPrefab;
        public Text ScoreText;

        [NonSerialized]
        [ReadOnly]
        public int Score;
        [NonSerialized]
        [ReadOnly]
        public int TotalShots;
    }
}

