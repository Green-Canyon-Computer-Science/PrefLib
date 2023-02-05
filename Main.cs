using MelonLoader;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Protobot.InputEvents;
using UnityEngine.Events;

namespace PrefLib
{
    public static class BuildInfo
    {
        public const string Name = "PrefLib"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Allows modders to add to the preference tab"; // Description for the Mod.  (Set as null if none)
        public const string Author = "InvertedOwl"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "0.0.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class PrefLib : MelonMod
    {

        private static GameObject menu;
        private static float currentY = 155;
        public static Dictionary<string, Dictionary<string, string>> modData = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, string> workingData = new Dictionary<string, string>();
        private static string workingTitle;

        public static void CreatePrefMenu(string modName)
        {
            workingData = new Dictionary<string, string>();
            workingTitle = modName;
            CreateNewTitle(modName);
        }

        public static void CreateData(string name, string def, UnityAction<string> action = null)
        {
            GameObject shor = GameObject.Instantiate(FindInActiveObjectByName("Shortcut Container (1)"), menu.transform);

            GameObject.Destroy(shor.GetComponent<RebindUI>());
            GameObject.Destroy(shor.transform.GetChild(3).gameObject);
            shor.transform.GetChild(0).GetComponent<Text>().text = name;
            shor.GetComponent<Image>().enabled = true;
            shor.transform.GetChild(0).GetComponent<Text>().enabled = true;
            shor.transform.GetChild(0).GetComponent<Shadow>().enabled = true;
            shor.transform.GetChild(2).GetComponent<Image>().enabled = true;
            shor.transform.GetChild(2).GetComponent<Image>().color = new Color(0.21f, 0.21f, 0.21f);


            GameObject.Destroy(shor.transform.GetChild(2).GetComponent<Button>());
            InputField f = shor.AddComponent<InputField>();
            f.textComponent = shor.transform.GetChild(2).GetChild(0).GetComponent<Text>();
            f.text = def;


            string titleClone = (string) workingTitle.Clone();
            f.onEndEdit.AddListener(delegate
            {
                OnDataChange(titleClone, name, f);
            });
            if (action != null)
            {
                f.onEndEdit.AddListener(action);
            }
            shor.transform.localPosition = new Vector2(0, currentY);
            currentY -= 35;
            //menu.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.transform.GetComponent<RectTransform>().sizeDelta.x, menu.transform.GetComponent<RectTransform>().sizeDelta.y + 35);

            workingData.Add(name, def);
            modData[workingTitle] = workingData;
        }

        private static void OnDataChange(string title, string dataName, InputField f)
        {
            modData[title][dataName] = f.text;
        }

        private static void CreateNewTitle(string title)
        {
            GameObject titleEx = FindInActiveObjectByName("Keybinds Title");
            GameObject newTitle = GameObject.Instantiate(titleEx, menu.transform);
            newTitle.transform.localPosition = new Vector2(-165, currentY);
            newTitle.GetComponent<Text>().enabled = true;
            newTitle.GetComponent<Text>().text = title;
            newTitle.GetComponent<Shadow>().enabled = true;

            currentY -= 35;
            //menu.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.transform.GetComponent<RectTransform>().sizeDelta.x, menu.transform.GetComponent<RectTransform>().sizeDelta.y + 35);
        }
        private static void CreateNewValue()
        {

        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName) // Runs after Game Initialization.
        {
            GameObject SelectionToggle = FindInActiveObjectByName("Selection Toggle");
            GameObject newToggle = GameObject.Instantiate(SelectionToggle, SelectionToggle.transform.parent);
            newToggle.transform.localPosition = new Vector2(75, 53.5f);
            newToggle.name = "Mods Toggle";
            newToggle.transform.GetChild(1).GetComponent<Text>().text = "Mods";

            GameObject SelectionMenu = FindInActiveObjectByName("Selection Prefs Menu");
            menu = GameObject.Instantiate(SelectionMenu, SelectionMenu.transform.parent);
            menu.transform.localPosition = new Vector2(-175, 0);
            menu.name = "Mods Prefs Menu";
            foreach (Transform g in menu.transform) {
                GameObject.Destroy(g.gameObject);
            }

            ScrollRect sr = menu.transform.parent.gameObject.AddComponent<ScrollRect>();
            sr.viewport = menu.transform.parent.GetComponent<RectTransform>();
            sr.content = menu.transform.GetComponent<RectTransform>();
            sr.horizontal = false;
            sr.elasticity = 0;
            sr.inertia = false;

            menu.transform.parent.GetComponent<Image>().enabled = true;
            menu.transform.parent.gameObject.AddComponent<Mask>();
            menu.transform.parent.GetComponent<Image>().color = new Color(0.22f, 0.22f, 0.22f, 1f);

            Toggle t = newToggle.GetComponent<Toggle>();
            t.onValueChanged.RemoveAllListeners();
            t.onValueChanged.AddListener(delegate
            {
                OnModsToggle(t, menu, SelectionMenu);
            });


        }
        bool firstFame = false;
        public override void OnUpdate()
        {
            if (!firstFame)
            {
                MelonLogger.Msg("First Frame");
                firstFame = true;
                menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, (-currentY));
                menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, currentY/2);
            }
        }

        public void OnModsToggle(Toggle t, GameObject menu, GameObject se)
        {
            if (t.isOn)
            {
                menu.SetActive(true);
                se.SetActive(false);
            }
            else
            {
                menu.SetActive(false);
            }
        }
        static GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }
    }
}