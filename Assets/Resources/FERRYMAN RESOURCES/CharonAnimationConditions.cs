using UnityEngine;

public class CharonAnimationConditions : MonoBehaviour
{
    [SerializeField] public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("isMovingLeft", false);
            anim.SetBool("IsMovingDown", false);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", true);
            anim.SetBool("IsMovingDown", false);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingDown", true);
        }
    }
}
