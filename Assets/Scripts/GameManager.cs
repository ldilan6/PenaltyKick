using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PenaltyKick
{
    public class GameManager : Singleton<GameManager>
    {
        public AudioClip ScoreSound;
        public AudioClip EndGameSound;
        public Transform PlayerStartPosition;
        public Transform AIStartPosition;
        public bool UseJoystick;
        public FixedJoystick Joystick;
        public GameObject SuddenDeathText;
        public GameObject EndGameText;

        public TeamData[] TeamDatas;
        public float CheckForGoalSecs = 5;
        public int AllowedShots = 5;
        public float EndGamePauseSecs = 10;

        int numberOfPlayers = 1;
        bool IsShotTaken = false;
        bool IsGoal = false;
        public bool useAI = false;
        int CurrentTeam = 1;

        public float KickPower = 11;
        public float KickPowerUp = 4;
        public float KickRange = 1;

        bool isGameOver;
        bool isSuddenDeath;

        float timeSinceShot;
        AudioSource audioSource;


        GameObject currentPlayer;

        

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
#if UNITY_IOS || UNITY_ANDROID
            //if (MobileInputs)
                //MobileInputs.SetActive(true);
#endif
        }

        // Start is called before the first frame update
        void Start()
        {
            if (UseJoystick)
                Joystick.gameObject.SetActive(true);
            else
                Joystick.gameObject.SetActive(false);

            OnResetGame();
        }

        public void SetNumberOfPlayers(int num)
        {
            numberOfPlayers = num;
        }

        void OnResetGame()
        {
            numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");

            foreach (var team in TeamDatas)
            {
                team.Score = 0;
                team.TotalShots = 0;
            }

            SuddenDeathText.SetActive(false);
            EndGameText.SetActive(false);
            CurrentTeam = 1;
            isSuddenDeath = false;
            IsGoal = false;
            IsShotTaken = false;
            SoccerBall.Instance.CanScoreGoal = true;
            CreatePlayer();
            UpdateScoreText();

            if (numberOfPlayers == 1)
                useAI = true;
        }

        void CreatePlayer()
        {
            if (currentPlayer)
                GameObject.Destroy(currentPlayer);

            if (IsAITurn())
            {
                var aiPos = AIStartPosition.position;
                aiPos.x = aiPos.x + (Random.insideUnitSphere * 2).x;
                aiPos.z = aiPos.y + (Random.insideUnitSphere * 2).z;
                currentPlayer = GameObject.Instantiate(TeamDatas[CurrentTeam - 1].AIPrefab, aiPos, AIStartPosition.rotation);
            }
            else
            {
                currentPlayer = GameObject.Instantiate(TeamDatas[CurrentTeam - 1].PlayerPrefab, PlayerStartPosition.position, PlayerStartPosition.rotation);
            }
            var spc = currentPlayer.GetComponent<SoccerPlayerCharacter>();
            spc.m_KickPower = KickPower;
            spc.m_KickPowerUp = KickPowerUp;
            spc.m_KickRange = KickRange;
        }

        bool IsAITurn()
        {
            if (!useAI)
                return false;

            return (CurrentTeam - 1 == 1);
        }

        // Update is called once per frame
        void Update()
        {
            if (IsShotTaken)
            {
                timeSinceShot += Time.deltaTime;
                if (timeSinceShot >= CheckForGoalSecs)
                {
                    NextKicker();
                }
            }
        }

        void NextKicker()
        {
            if (IsGoal)
                TeamDatas[CurrentTeam - 1].Score++;

            UpdateScoreText();

            GoalKeeper.Instance.OnNextKicker();
            SoccerBall.Instance.OnNextKicker();

            // Reset our variables
            IsGoal = false;
            IsShotTaken = false;
            timeSinceShot = 0;

            CurrentTeam++;
            if (CurrentTeam - 1 >= TeamDatas.Length)
            {
                NextRound();
            }
            if (!isGameOver)
                CreatePlayer();

            if (isSuddenDeath)
                SuddenDeathText.SetActive(true);
        }

        void NextRound()
        {
            isGameOver = true;
            foreach (var team in TeamDatas)
            {
                if (team.TotalShots < AllowedShots)
                    isGameOver = false;
            }
            if (isGameOver || isSuddenDeath)
            {
                if (TeamDatas[0].Score == TeamDatas[1].Score)
                    isSuddenDeath = true;
                else
                   EndGame();
            }

            if (isSuddenDeath)
                isGameOver = false;

            CurrentTeam = 1;
        }

        public void UpdateScoreText()
        {
            foreach(var team in TeamDatas)
            {
                team.ScoreText.text = "Team " + team.TeamId + " Score: " + team.Score;
            }
        }

        public void OnGoal()
        {
            Debug.Log("Goal!");
            GoalKeeper.Instance.OnGoal();
            SoccerBall.Instance.CanScoreGoal = false;

            IsGoal = true;
            if (ScoreSound)
               audioSource.PlayOneShot(ScoreSound);
        }

        public void OnKickButton()
        {
            var player = GameObject.FindObjectOfType<SoccerPlayerUserControl>();
            player.m_Kick = true;
        }

        public void OnShotTaken()
        {
            IsShotTaken = true;
            timeSinceShot = 0;
            TeamDatas[CurrentTeam - 1].TotalShots++;

            GoalKeeper.Instance.OnShotTaken();
        }

        public void EndGame()
        {
            isGameOver = true;
            isSuddenDeath = false;

            var maxScore = TeamDatas.Max(x => x.Score);
            TeamData winner = TeamDatas.FirstOrDefault(x => x.Score == maxScore);

            SuddenDeathText.SetActive(false);
            EndGameText.GetComponent<Text>().text = "Team " + winner.TeamId + " Won!";
            EndGameText.SetActive(true);

            if (EndGameSound)
                audioSource.PlayOneShot(EndGameSound);

            Invoke("RestartGame", EndGamePauseSecs);
        }
        public void RestartGame()
        {
            SceneManager.LoadScene("GameUI");
        }

    }
}
