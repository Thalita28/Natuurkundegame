using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField] MenuButtonManager menuButtonManager;
    [SerializeField] int thisIndex;
    public GameObject levelMenu;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonManager.index == thisIndex && menuButtonManager.AllowScrol)
        {
            if(!animator.GetBool("pressed")) animator.SetBool("selected", true);

            if (Input.GetAxis("Submit") == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("selected"))
            {
                animator.SetBool("pressed", true);
                menuButtonManager.AllowScrol = false;
               
               
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }

        }
        else animator.SetBool("selected", false);
    }
    
    

    public void ReturnToMenu()
    {
        levelMenu.SetActive(false);

        menuButtonManager.AllowScrol= true;
        animator.SetTrigger("exitPressed");
    }

 


    
}
