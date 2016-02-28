using UnityEngine;

namespace Assets.Custom_Assets.Scripts.Static
{
    public static class GameObjectExtensions
    {
        public static GameObject getChildGameObject(GameObject fromGameObject, string withName)
        {
            Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
            return null;
        }
    }
}
