using UnityEditor;
using UnityEditor.SceneManagement;

namespace Project.Editor
{
    [InitializeOnLoad]
    public static class EntryPointSceneAutoLoader
    {
        private const string MenuPath = "PlayFromBootstrap/Enabled";
        private const string PlatFromBootstrapKey = "PlatFromBootstrapKey";
        private const int BootSceneIndex = 0;
        
        static EntryPointSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        [MenuItem(MenuPath)]
        private static void Toggle()
        {
            bool result = EditorPrefs.GetBool(PlatFromBootstrapKey);
            EditorPrefs.SetBool(PlatFromBootstrapKey, !result);
        }

        [MenuItem(MenuPath, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MenuPath, EditorPrefs.GetBool(PlatFromBootstrapKey));
            return true;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (EditorPrefs.GetBool(PlatFromBootstrapKey) == false)
                {
                    EditorSceneManager.playModeStartScene = null;
                    return;
                }
                
                if (EditorBuildSettings.scenes.Length == 0)
                    return;
                
                EditorSceneManager.playModeStartScene = AssetDatabase
                    .LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[BootSceneIndex].path);
            }
        }
    }
}