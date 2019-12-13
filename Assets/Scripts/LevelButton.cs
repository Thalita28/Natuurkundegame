using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField] LevelButtonManager levelButtonManager;
    [SerializeField] int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("start"))
        {
            if (thisIndex <= PlayerPrefs.GetInt("levelProgress" + levelButtonManager.MenuIndex, 1)) animator.SetBool("start", true);
        }

        if (levelButtonManager.index == thisIndex)
        {
            if (!animator.GetBool("pressed")) animator.SetBool("selected", true);

            if (Input.GetAxis("Submit") == 1)
            {
                Debug.Log("ALLOW SCROL = FALSE");
                animator.SetBool("pressed", true);


            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }

        }
        else animator.SetBool("selected", false);
    }

    public void StartLevelx()
    {
        //SceneManager.LoadScene("Level_" + thisIndex);
        Debug.Log("Starting Level_" + thisIndex);
    }


    public void ReturnToMenu()
    {
        animator.SetTrigger("exitPressed");
    }





}
