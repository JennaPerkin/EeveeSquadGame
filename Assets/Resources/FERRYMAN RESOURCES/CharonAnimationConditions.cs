using UnityEngine;

public class CharonAnimationConditions : MonoBehaviour
{
    [SerializeField] public Animator anim;
    public Move moveScript;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("isMovingLeft", false);
            anim.SetBool("IsMovingDown", false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isMovingRight", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", true);
            anim.SetBool("IsMovingDown", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("isMovingLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("IsMovingDown", false);
        }*/

        if(moveScript.moveHorizontal >= 0.1)
        {
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", false);
        }
        if(moveScript.moveHorizontal < 0)
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", true);
            anim.SetBool("IsMovingDown", false);
        }
        if(moveScript.moveVertical < 0)
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", true);
        }
        if(moveScript.moveHorizontal == 0 && moveScript.moveVertical == 0)
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", false);
        }
    }
}
