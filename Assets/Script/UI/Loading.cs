using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation op;
    int process;//加载的进度
    int time = 0;
    // Use this for initialization
    void Start()
    {
        WindowManager.instance.CloseAllWindows();
        StartCoroutine(loadScene());
    }
    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator loadScene()
    {
        int displayProgress = 0;
        //int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(GlobalData.nextScene);
        op.allowSceneActivation = false;
        while (displayProgress < 100)
        {
            ++displayProgress;
            time++;
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
    }
}
