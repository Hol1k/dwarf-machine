using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private const string GameplayScenePath = "Scenes/Gameplay";
        private const string TestLevelScenePath = "Scenes/TestLevel";
        
        public void OnStartButton()
        {
            StartTestLevel().Forget();
        }

        private async UniTask StartTestLevel()
        {
            DontDestroyOnLoad(this);

            await Addressables.LoadSceneAsync(GameplayScenePath);
            await Addressables.LoadSceneAsync(TestLevelScenePath, LoadSceneMode.Additive);
            
            Destroy(gameObject);
        }
    }
}