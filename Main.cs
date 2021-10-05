using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Horror;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace DevourMono
{
    public class Main : MonoBehaviour
    {
        #region Vars
        public static Camera Cam;
        public static PlayerCharacterBehaviour LocalPlayer;

        public static List<PlayerCharacterBehaviour> Players = new List<PlayerCharacterBehaviour>();
        public static List<SurvivalDemonBehaviour> Demons = new List<SurvivalDemonBehaviour>();
        public static List<SpiderBehaviour> Spiders = new List<SpiderBehaviour>();
        public static List<GoatBehaviour> Goats = new List<GoatBehaviour>();
        public static List<SurvivalEggInteractable> Eggs = new List<SurvivalEggInteractable>();
        public static List<SurvivalInteractable> Items = new List<SurvivalInteractable>();
        public static List<KeyBehaviour> Keys = new List<KeyBehaviour>();
        public static List<CollectableInteractable> Collectibles = new List<CollectableInteractable>();
        public static SurvivalAzazelBehaviour Azazel;
        public static GameObject Ritual;
        
        IEnumerator MainUpdate;
        #endregion

        public void Start()
        {
            MainUpdate = MainUpdateFunc(0f);
            StartCoroutine(MainUpdate);
        }
        public void Update()
        {
            InputManager();
        }
        public void InputManager()
        {
            if (Input.GetKeyDown(KeyCode.Delete)) Loader.Unload();
            if (Input.GetKeyDown(KeyCode.Insert)) Menu.MenuOpen = !Menu.MenuOpen;

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                AchievementHelpers ah = FindObjectOfType<AchievementHelpers>();

                string[] names = { "hasAchievedFusesUsed", "hasAchievedGasolineUsed", "hasAchievedNoKnockout", "hasCollectedAllPatches", "hasCollectedAllRoses",
                "hasCompletedHardAsylumGame", "hasCompletedHardGame", "hasCompletedNightmareAsylumGame", "hasCompletedNightmareGame", "hasCompletedNormalGame",
                "isStatsValid", "isStatsFetched" };
                for (int i = 0; i < names.Length; i++)
                {
                    ah.GetType().GetField(names[i], BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ah, true);
                }

                string[] achievments = { "ACH_ALL_ROSES", "ACH_BURNT_GOAT", "ACH_SURVIVED_TO_3_GOATS", "ACH_SURVIVED_TO_5_GOATS", "ACH_SURVIVED_TO_7_GOATS", "ACH_WON_SP", "ACH_WON_COOP",
                "ACH_LOST", "ACH_LURED_20_GOATS", "ACH_REVIVED_20_PLAYERS", "ACH_ALL_NOTES_READ", "ACH_KNOCKED_OUT_BY_ANNA", "ACH_KNOCKOUT_OUT_BY_DEMON", "ACH_KNOCKED_OUT_20_TIMES",
                "ACH_NEVER_KNOCKED_OUT", "ACH_ONLY_ONE_KNOCKED_OUT", "ACH_UNLOCKED_CAGE", "ACH_UNLOCKED_ATTIC_CAGE", "ACH_BEAT_GAME_5_TIMES", "ACH_100_GASOLINE_USED",
                "ACH_FRIED_20_DEMONS", "ACH_STAGGERED_ANNA_20_TIMES", "ACH_CALMED_ANNA_10_TIMES", "ACH_CALMED_ANNA", "ACH_WIN_NIGHTMARE", "ACH_BEAT_GAME_5_TIMES_IN_NIGHTMARE_MODE",
                "ACH_WON_NO_KNOCKOUT_COOP", "ACH_WIN_NIGHTMARE_SP", "ACH_WON_HARD", "ACH_WON_HARD_SP", "ACH_100_FUSES_USED", "ACH_ALL_CLIPBOARDS_READ", "ACH_ALL_PATCHES",
                "ACH_FRIED_RAT", "ACH_FRIED_100_INMATES", "ACH_LURED_20_RATS", "ACH_STAGGERED_MOLLY_20_TIMES", "ACH_WON_MOLLY_SP", "ACH_WON_MOLLY_HARD_SP", "ACH_WON_MOLLY_NIGHTMARE_SP",
                "ACH_WON_MOLLY_COOP", "ACH_WON_MOLLY_HARD", "ACH_WON_MOLLY_NIGHTMARE", "ACH_20_TRASH_CANS_KICKED", "ACH_CALM_MOLLY_10_TIMES" };
                for (int i = 0; i < achievments.Length; i++)
                {
                    ah.Unlock(achievments[i]);
                }
            } // Unlock All
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                foreach (DoorBehaviour db in FindObjectsOfType<DoorBehaviour>())
                    db.Unlock();
            } // Unlock Doors
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Settings.Flashlight = !Settings.Flashlight;
                Light l = LocalPlayer.GetComponent<NolanBehaviour>().flashlightSpot;
                if (Settings.Flashlight)
                {
                    l.intensity = 4f;
                    l.range = 60f;
                }
                else
                {
                    l.intensity = 2;
                    l.range = 20f;
                }
            } // Strong Flashlight
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Settings.FullBright = !Settings.FullBright;
                Transform head = LocalPlayer.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);

                Light light = null;
                foreach (Light l in head.GetComponentsInChildren<Light>())
                    if (l.name == "BrightLight") light = l;
                if (light == null) head.gameObject.AddComponent<Light>();

                if (Settings.FullBright)
                {
                    light.name = "BrightLight";
                    light.color = Color.white;
                    light.type = LightType.Spot;
                    light.shadows = LightShadows.None;
                    light.range = 10000f;
                    light.spotAngle = 9999f;
                    light.intensity = 0.5f;
                }
                else
                {
                    Destroy(light);
                }
            } // Fullbright
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                foreach (SurvivalDemonBehaviour d in Demons)
                    d.Despawn();
            } // Despawn Demons
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                foreach (KeyBehaviour k in Keys)
                    k.transform.position = LocalPlayer.transform.position + LocalPlayer.transform.forward * 2.5f;
            } // TP Keys
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                foreach (GoatBehaviour g in Goats)
                    g.GetComponent<UltimateCharacterLocomotion>().SetPosition(LocalPlayer.transform.position + LocalPlayer.transform.forward * 2.5f);
            } // TP Goats
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                foreach (SurvivalInteractable i in Items)
                    i.transform.position = LocalPlayer.transform.position + LocalPlayer.transform.forward * 2.5f;
            } // TP Items
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                LocalPlayer.GetComponent<UltimateCharacterLocomotion>().SetPosition(LocalPlayer.transform.position + LocalPlayer.transform.forward * 2.5f);
            } // TP Ahead
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Settings.Speed = !Settings.Speed;
                if (Settings.Speed)
                    LocalPlayer.GetComponent<UltimateCharacterLocomotion>().TimeScale = 5f;
                else
                    LocalPlayer.GetComponent<UltimateCharacterLocomotion>().TimeScale = 1f;
            } // 5x Speed
        }

        IEnumerator MainUpdateFunc(float time)
        {
            yield return new WaitForSeconds(time);

            try
            {
                if (!Cam) Cam = Camera.main;

                #region Add To Lists
                Players = FindObjectsOfType<PlayerCharacterBehaviour>().ToList();
                Demons = FindObjectsOfType<SurvivalDemonBehaviour>().ToList();
                Spiders = FindObjectsOfType<SpiderBehaviour>().ToList();
                Goats = FindObjectsOfType<GoatBehaviour>().ToList();
                Eggs = FindObjectsOfType<SurvivalEggInteractable>().ToList();
                
                Items = FindObjectsOfType<SurvivalInteractable>().ToList();
                Keys = FindObjectsOfType<KeyBehaviour>().ToList();
                Collectibles = FindObjectsOfType<CollectableInteractable>().ToList();
                #endregion
                #region Get Local, Azazel, Zara, Ritual
                foreach (PlayerCharacterBehaviour p in Players) if (Vector3.Distance(Cam.transform.position, p.transform.position) < 3f) LocalPlayer = p; Players.Remove(LocalPlayer);
                Ritual = GameObject.Find("SM_RitualBowl");
                Azazel = FindObjectOfType<SurvivalAzazelBehaviour>();
                #endregion
            }
            catch { }

            MainUpdate = MainUpdateFunc(3f);
            StartCoroutine(MainUpdate);
        }

    }
}