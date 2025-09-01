using UnityEngine;

public class EnemyHitBoxBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int result;
    
    public virtual void playerDestroy(int _param)
    {
        //Debug.Log(gameObject.name);
        gameObject.SetActive(false);
    }

  


}
