using UnityEngine;

public class panel : MonoBehaviour
{
    public PauseManager_Joycelyn pauseScreen;

    public void HideScreen()
    {
        pauseScreen.ResumeAnimationEnd();
    }
}
