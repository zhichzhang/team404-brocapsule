using UnityEngine;
using UnityEngine.UI;

public class SpearPop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    public Animator barAnim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Pop();
        //}
    }
    public void Pop()
    {
        anim.SetTrigger("Pop");
        barAnim.SetTrigger("Pop");
    }
}
