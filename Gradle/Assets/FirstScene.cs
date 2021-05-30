using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour {

    private const string TapsellPlusKey = "alsoatsrtrotpqacegkehkaiieckldhrgsbspqtgqnbrrfccrtbdomgjtahflchkqtqosa";

    void Start () {

        TapsellPlus.TapsellPlus.Initialize(TapsellPlusKey,
            adNetworkName => Debug.Log(adNetworkName + " Initialized Successfully."),
            error => Debug.Log(error.ToString()));
        TapsellPlus.TapsellPlus.SetGdprConsent(true);
    }

    public void ChangeScenes (string sceneName) {
        SceneManager.LoadScene (sceneName);
    }
}