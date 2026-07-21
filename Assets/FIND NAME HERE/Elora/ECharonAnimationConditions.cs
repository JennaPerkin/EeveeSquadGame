using UnityEngine;

public class ECharonAnimationConditions : MonoBehaviour
{
    [SerializeField] public Animator anima;
  
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            anima.SetBool("IsMovingRight", true);
            anima.SetBool("IsMovingDown", false);
            anima.SetBool("IsMovingLeft", false);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D))
        {
            anima.SetBool("IsMovingRight", false);
            anima.SetBool("IsMovingDown", false);
            anima.SetBool("IsMovingLeft", true);
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            anima.SetBool("IsMovingRight", false);
            anima.SetBool("IsMovingDown", true);
            anima.SetBool("IsMovingLeft", false);
        }
    }
}
