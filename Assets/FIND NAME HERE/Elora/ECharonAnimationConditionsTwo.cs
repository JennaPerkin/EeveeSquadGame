using UnityEngine;

public class ECharonAnimationConditionsTwo : MonoBehaviour
{
    [SerializeField] public Animator anim;
  
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("IsMovingDown", false);
            anim.SetBool("IsMovingLeft", false);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingDown", false);
            anim.SetBool("IsMovingLeft", true);
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsMovingDown", true);
            anim.SetBool("IsMovingLeft", false);
        }
    }
}
