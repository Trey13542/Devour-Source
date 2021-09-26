using UnityEngine;

namespace DevourMono
{
    public class Menu : MonoBehaviour
    {
        public static bool MenuOpen = true;
        public static int WindowId = 0;
        public static Rect MenuRect = new Rect(0, 0, 350, 300);

        void OnGUI()
        {
            if (MenuOpen)
            {
                MenuRect = GUI.Window(WindowId, MenuRect, SwitchWindow, GUIContent.none);
            }
        }
        void SwitchWindow(int id)
        {
            if (GUI.Button(new Rect(1, 20, 175, 25), "Visuals")) WindowId = 0;
            if (GUI.Button(new Rect(175, 20, 175, 25), "Player Editor (BROKE)")) WindowId = 1;

            if (id == 0)
            {
                Settings.AzazelEsp = GUI.Toggle(new Rect(5, 45, 150, 25), Settings.AzazelEsp, "Azazel Esp");
                Settings.PlayerEsp = GUI.Toggle(new Rect(5, 70, 150, 25), Settings.PlayerEsp, "Player Esp");
                Settings.DemonEsp = GUI.Toggle(new Rect(5, 95, 150, 25), Settings.DemonEsp, "Demon Esp");
                Settings.GoatEsp = GUI.Toggle(new Rect(5, 120, 150, 25), Settings.GoatEsp, "Goat Esp");
                Settings.ItemEsp = GUI.Toggle(new Rect(5, 145, 150, 25), Settings.ItemEsp, "Item Esp");
                Settings.KeyEsp = GUI.Toggle(new Rect(5, 170, 150, 25), Settings.KeyEsp, "Key Esp");
                Settings.CollectibleEsp = GUI.Toggle(new Rect(5, 195, 150, 25), Settings.CollectibleEsp, "Collectible Esp");
                Settings.RitualEsp = GUI.Toggle(new Rect(5, 220, 150, 25), Settings.RitualEsp, "Ritual Bowl Esp");

                GUI.Label(new Rect(125, 45, 300, 100), $"Numpad0-Unlock Robes & Ach.");
                GUI.Label(new Rect(125, 60, 300, 100), $"Numpad1-Unlock Doors");
                GUI.Label(new Rect(125, 75, 300, 100), $"Numpad2-Good Flashlight");
                GUI.Label(new Rect(125, 90, 300, 100), $"Numpad3-Full Bright");
                GUI.Label(new Rect(125, 105, 300, 100), $"Numpad4-Kill Demons");
                GUI.Label(new Rect(125, 122, 300, 100), $"Numpad5-Tp Keys 2.5m Ahead");
                GUI.Label(new Rect(125, 135, 300, 100), $"Numpad6-Tp Goats 2.5m Ahead");
                GUI.Label(new Rect(125, 150, 300, 100), $"Numpad7-Tp Fuses/Gas 2.5m & Hay/Food 5m");
                GUI.Label(new Rect(125, 165, 300, 100), $"Numpad8-Tp 2.5m Ahead");
                GUI.Label(new Rect(125, 180, 300, 100), $"Numpad9-5x Speed");
            }

            GUI.DragWindow();
        }
    }
}