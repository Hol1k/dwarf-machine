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
        private const string TestMvpScenePath = "Scenes/MVPLevel";
        
        public void OnStartButton()
        {
            StartTestLevel().Forget();
        }

        private async UniTask StartTestLevel()
        {
            DontDestroyOnLoad(this);

            await Addressables.LoadSceneAsync(GameplayScenePath);
            await Addressables.LoadSceneAsync(TestMvpScenePath, LoadSceneMode.Additive);
            
            Destroy(gameObject);
        }
    }
}