using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Adventure
{
    public class SaveLoad : MonoBehaviour
    {
        private const string fileName = "TransformSave.txt";
        [SerializeField] private bool doLoad;
        private Vector3 savedPlayerPosition;

        private string savedSceneName;

        private string SaveDirectory => Application.persistentDataPath + "/Saves/";

        public static SaveLoad Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            doLoad = StaticContext.DoLoad;
        }

        
        private void Start()
        {
            print("loading");
            if (doLoad)
                LoadAndMove();
            else
                MoveToStartPoint();
        }

        private void Update()
        {
            // Save player position if Ctrl+F3 is pressed
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F3))
            {
                SavePlayer();
                print("Saved from keyboard shortcut");
            }
        }

        private void OnDisable()
        {
            SavePlayer();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

            SceneManager.sceneLoaded -= OnSceneTransferred;
        }

        private void MoveToStartPoint()
        {
            var enterPoint = FindObjectOfType<StartPoint>().transform.position;
            StaticPlayer.Instance.transform.position = enterPoint;
            SceneManager.sceneLoaded += OnSceneTransferred;
        }

        private void LoadAndMove()
        {
            try
            {
                LoadPlayer();
            }
            catch (Exception e)
            {
                // Handle if no file exists
                Console.WriteLine(e);
                return;
            }

            if (savedSceneName != SceneManager.GetActiveScene().name)
            {
                // Load other scene, set position once loaded
                SceneManager.LoadScene(savedSceneName);
                SceneManager.sceneLoaded += OnSceneLoaded;
                return;
            }

            StaticPlayer.Instance.transform.position = savedPlayerPosition;
            SceneManager.sceneLoaded += OnSceneTransferred;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            StaticPlayer.Instance.transform.position = savedPlayerPosition;
            SceneManager.sceneLoaded += OnSceneTransferred;
        }

        private void OnSceneTransferred(Scene scene, LoadSceneMode mode)
        {
                var enterPoint = FindObjectOfType<StartPoint>().transform.position;
                    
                StaticPlayer.Instance.transform.position = enterPoint;
        }

        public void SavePlayer()
        {
            var player = StaticPlayer.Instance;
            if (player == null) return;

            if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

            using var streamWriter = new StreamWriter(SaveDirectory + fileName);
            streamWriter.WriteLine(SceneManager.GetActiveScene().name);
            var position = StaticPlayer.Instance.transform.position;
            streamWriter.WriteLine(position.x);
            streamWriter.WriteLine(position.y);
            streamWriter.WriteLine(position.z);
        }


        private void LoadPlayer()
        {
            if (!File.Exists(SaveDirectory + fileName))
                throw new Exception("Cannot load player position. File does not exist.");

            using var streamReader = new StreamReader(SaveDirectory + fileName);
            var line = "";
            line = streamReader.ReadLine();
            savedSceneName = line;
            line = streamReader.ReadLine();
            savedPlayerPosition.x = float.Parse(line);
            line = streamReader.ReadLine();
            savedPlayerPosition.y = float.Parse(line);
            line = streamReader.ReadLine();
            savedPlayerPosition.z = float.Parse(line);
        }
    }
}