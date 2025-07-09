using UnityEngine;

public class Tutorial : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.Open(GameUIID.TutorialScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
