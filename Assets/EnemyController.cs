using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region attack_variables
    private bool isAttacking;
    #endregion

    #region animation_variables
    Animator anim;
    public float endAnimationTiming;
    #endregion

    #region Unity_Functions
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
            return;
        }

        anim.SetBool("Attacking", false);
    }
    #endregion

    #region attack_functions
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(endAnimationTiming);
        isAttacking = false;
    }
    #endregion

}
