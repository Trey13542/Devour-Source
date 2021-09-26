using UnityEngine;

namespace DevourMono
{
    public class Loader
    {
        private static GameObject LoadObj = new GameObject();
        public static void Load()
        {
            LoadObj.AddComponent<Main>();
            LoadObj.AddComponent<Menu>();
            LoadObj.AddComponent<Esp>();
            Object.DontDestroyOnLoad(LoadObj);
        }
        public static void Unload()
        {
            Object.Destroy(LoadObj);
        }
    }
}