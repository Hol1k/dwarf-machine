using System;
using Cysharp.Threading.Tasks;
using ScenesManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private const string GameplayScenePath = "Scenes/Gameplay";
        private const string TestLevelScenePath = "Scenes/TestLevel";
        private const string TestMvpScenePath = "Scenes/MVPLevel";
        
        public void OnStartButton()
        {
            StartTestLevel().Forget();
        }

        private async UniTask StartTestLevel()
        {
            try
            {
                DontDestroyOnLoad(this);

                await Addressables.LoadSceneAsync(GameplayScenePath);
                await Addressables.LoadSceneAsync(TestMvpScenePath, LoadSceneMode.Additive);
                Bootstrap mvpLevelBootstrap = FindAnyObjectByType<MvpLevelBootstrap>();
                mvpLevelBootstrap.Init(new MvpLevelArgs());
            
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}