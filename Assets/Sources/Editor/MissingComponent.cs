using UnityEditor;
using UnityEngine;


namespace Sources.Editor
{
    public class MissingComponent
    {
        [MenuItem("Tools/FindMissingComponents")]
        public static void Find()
        {
            Debug.Log("Start Find Missing Components.");
            bool isFind = false;
            GameObject[] instances = UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            foreach (var instance in instances)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(instance);
                if (count > 0)
                {
                    Debug.LogError("Failed!! Find Missing Component in Scene. name=" + instance.gameObject.name);
                    isFind = true;
                }
            }

            string[] prefabGUIDs = AssetDatabase.FindAssets("t:prefab");
            foreach (var prefabGUID in prefabGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(prefabGUID);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(prefab);
                if (count > 0)
                {
                    Debug.LogError("Failed!! Find Missing Component in Prefab. name=" + prefab.gameObject.name);
                    isFind = true;
                }
            }

            if (isFind == false)
            {
                Debug.Log("Success!! Not found missing components.");
            }
        }
    }
}