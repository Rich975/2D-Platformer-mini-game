using UnityEngine;

public class nozzleFlashBehaviour : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isFired", true);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}