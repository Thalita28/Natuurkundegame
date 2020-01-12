using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField] int blockIndex;
    [SerializeField] int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
       
       // if (thisIndex <= PlayerPrefs.GetInt("levelProgress" + blockIndex, 3)) animator.SetBool("start", true);
    }

    private void OnEnable()
    {
        Debug.Log("levelProgress" + blockIndex + " = " + PlayerPrefs.GetInt("levelProgress0") );
        animator = GetComponent<Animator>();
        if (thisIndex <= PlayerPrefs.GetInt("levelProgress" + blockIndex, 1)) animator.SetBool("start", true);
    }
    // Update is called once per frame  

    public void StartLevelx()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("wait"))
        {
            SceneManager.LoadScene("Level_" + blockIndex + "_" + thisIndex);
            Debug.Log("Starting Level_" + blockIndex + "_" + thisIndex);
        }
    }


}
