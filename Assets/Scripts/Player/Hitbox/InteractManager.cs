using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;
    public Rigidbody2D rb;
    public Collider2D triggerCollider;
    public LayerMask contactLayer;
    private ContactFilter2D contactFilter;
    private List<Collider2D> detectedColliders = new List<Collider2D>();
    private enum State{
        Fail,
        Success,
    }
    State state;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(contactLayer);
    }

    void Start()
    {
        state = State.Fail;
    }

    // Update is called once per frame
    void Update()
    {
        checkAllColliders();
    }

    public void checkAllColliders()
    {
        switch (state)
        {
            case State.Fail:
                
                // update
                //player.ladderCheck = false;

                // check for transition
                detectedColliders.Clear();
                Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);

                //check for available ladder
                foreach (var collider in detectedColliders)
                {
                    if (collider.gameObject.CompareTag("Ladder"))
                    {
                        //if found, go to success state
                        player.currentInteractingSpear = collider.GetComponent<DrpSpearVertical>();
                        if (player.currentInteractingSpear != null)
                        {
                            state = State.Success;
                            player.ladderCheck = true;
                            return;

                        }
                       
                        
                    }
                }
                return;
            case State.Success:

                //update
                //player.ladderCheck = true;

                //check for transition
                detectedColliders.Clear();
                Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);
                bool found = false;

                //search if player in current ladder
                foreach (var collider in detectedColliders)
                {
                    if (collider.gameObject.CompareTag("Ladder"))
                    {
                        // Player Interating with the same spear
                        if( player.currentInteractingSpear == collider.GetComponent<DrpSpearVertical>() ) { found = true; }
                    }
                }

                // if found, stay in current ladder
                if ( found) { return; }

                // if not search for another ladder
                detectedColliders.Clear();
                Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);

                foreach (var collider in detectedColliders)
                {
                    if (collider.gameObject.CompareTag("Ladder"))
                    {
                        
                        player.currentInteractingSpear = collider.GetComponent<DrpSpearVertical>();
                        
                        
                        return;
                    }
                }

               
                player.ladderCheck = false;
                player.currentInteractingSpear = null;
                state = State.Fail;
                return;
        }
    }
}
