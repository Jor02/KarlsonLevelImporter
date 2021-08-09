using BepInEx;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using KarlsonLevelImporter.Core.Menu;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;

namespace KarlsonLevelImporter
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Base : BaseUnityPlugin
    {
        void Awake()
        {
            new GameObject("Level Loader", typeof(Core.LevelLoader));

            SceneManager.sceneLoaded += OnSceneLoaded;

            //Patch
            Harmony harmony = new Harmony("com.jor02.karlsonlevelimporter");
            harmony.PatchAll();
        }

        private void OnSceneLoaded(Scene target, LoadSceneMode mode)
        {
            if (target.name == "MainMenu") // If we loaded the main menu
            {
                Core.LevelLoader.Instance.Reset();

                SetupMenu();
            } else if (Core.LevelLoader.Playing && mode != LoadSceneMode.Additive) // If we restarted
            {
                StartCoroutine(Core.LevelLoader.Instance.BeginLoadLevel("", 0));
            }
        }

        private void SetupMenu()
        {
            Transform UI = GameObject.Find("/UI").transform;
            Transform menu = UI.Find("Menu");

            Destroy(menu.Find("Map").gameObject);

            Transform customLevelButton = Instantiate(menu.Find("Play"), menu);
            Transform levelSelect = Instantiate(UI.Find("Play"), UI);

            #region Setup Custom Button
            //Set custom level button transform
            customLevelButton.localPosition = new Vector3(-152.3003f, 77.7556f, 0);
            customLevelButton.localScale = Vector3.one * 1.1707f;

            //Setup text
            TextMeshProUGUI customLevelButtonText = customLevelButton.GetChild(0).GetComponent<TextMeshProUGUI>();
            customLevelButtonText.text = "custom";
            customLevelButtonText.fontSize = 36f;
            customLevelButtonText.enableWordWrapping = false;
            customLevelButtonText.transform.localPosition = new Vector3(17.0441f, 0);

            //Setup button
            Button b = customLevelButton.GetComponent<Button>();
            b.onClick = new Button.ButtonClickedEvent();
            b.onClick.AddListener(() => { levelSelect.gameObject.SetActive(true); });
            b.onClick.AddListener(() => { menu.gameObject.SetActive(false); });
            b.onClick.AddListener(FindObjectOfType<MenuCamera>().Play);
            b.onClick.AddListener(FindObjectOfType<Lobby>().ButtonSound);

            //Move other buttons
            menu.Find("Options").localPosition -= Vector3.up * 50.6211f;
            menu.Find("About").localPosition -= Vector3.up * 48.6453f;
            #endregion

            #region Setup Level Selection
            foreach (Transform child in levelSelect)
            {
                if (child.name != "Back" && child.name != "Tutorial")
                    Destroy(child.gameObject);
            }

            //Set button alignment transform
            RectTransform buttonAlignment = new GameObject("Align").AddComponent<RectTransform>();
            buttonAlignment.SetParent(levelSelect, false);
            buttonAlignment.localPosition = new Vector3(-149.4695f, -90.8f, 0f);
            buttonAlignment.localRotation = Quaternion.identity;
            buttonAlignment.sizeDelta = new Vector2(627.31f, 437.6f);

            //Setup button alignment GridLayoutGroup
            GridLayoutGroup buttonAlignmentLayout = buttonAlignment.gameObject.AddComponent<GridLayoutGroup>();
            buttonAlignmentLayout.cellSize = new Vector2(96f, 54f);
            buttonAlignmentLayout.spacing = new Vector2(72.2f, 58.7f);
            buttonAlignmentLayout.childAlignment = TextAnchor.UpperCenter;
            buttonAlignmentLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            buttonAlignmentLayout.constraintCount = 4;

            //Create level buttons
            LevelButtons ButtonGenerator = new GameObject("Button Generator").AddComponent<LevelButtons>();
            ButtonGenerator.Init(levelSelect.Find("Tutorial"), buttonAlignment);

            //Setup Loading Error
            new GameObject("LoadingError", typeof(LoadingError));

            LoadingError.Instance.Init(levelSelect.Find("Back").GetChild(0), levelSelect);
            #endregion

            #region Check Time
            Core.LevelTimes levelTimes = new Core.LevelTimes();
            #endregion

            #region Load Levels
            Core.LevelLoader.Response[] responses = Core.LevelLoader.Instance.FetchLevels();
            Core.LevelLoader.Response[] succesfulResponses;

            if (!Core.LevelLoader.Instance.ParseResponses(responses, out succesfulResponses)) return;

            foreach (Core.LevelLoader.Response response in succesfulResponses)
            {
                //Create button
                ButtonGenerator.AddButton(response.LevelName, levelTimes[response.LevelPath], response.Thumbnail, response.LevelPath, response.HeaderSize);
            }
            #endregion
        }
    }
}
