using UnityEngine;

public class CharonAnimationCondiitions : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", false);
        }
       if(Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", true);
            anim.SetBool("IsMovingDown", false);
        }
       if(Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", true);
        }
    }
}
