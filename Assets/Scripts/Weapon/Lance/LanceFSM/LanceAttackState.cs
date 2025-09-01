using JetBrains.Annotations;
using UnityEngine;

public class LanceAttackState : LanceState
{
    public LanceAttackState(LanceStateMachine stateMachine, Lance lance) : base(stateMachine, lance)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lance.Disappear();
        GameObject throwingLance;
        
        Vector3 shootPoint;
        BoxCollider2D lanceColliderH = lance.throwableLancePrefabVerticalUp.GetComponent<BoxCollider2D>();
        float lanceHeight = lanceColliderH.size.y / 2 - lanceColliderH.offset.y;
        //Debug.Log(lanceHeight);

        if (stateMachine.attackInfo.isUpPressed())
        {
            // Display UI Spear pop
            lance.player.spearPopUp.Pop();

            shootPoint = new Vector3(lance.shootingVPosition.position.x, lance.shootingVPosition.position.y, 0);
            RaycastHit2D hitH = Physics2D.Raycast(lance.shootingVPosition.position, Vector2.up, lanceHeight, lance.ground);
            if (hitH.collider != null)
            {
                shootPoint = hitH.point - Vector2.up * new Vector2(0, lanceHeight);
            }
            
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVerticalUp, shootPoint, lance.throwableLancePrefabVerticalUp.transform.rotation);
            //throwingLance.transform.Rotate(0, 0, 180);
            return;
        }
        if (stateMachine.attackInfo.isDownPressed())
        {
            // Display UI Spear pop
            lance.player.spearPopDown.Pop();


            shootPoint = new Vector3(lance.shootingVPosition.position.x, lance.shootingVPosition.position.y, 0);
            RaycastHit2D hitH = Physics2D.Raycast(lance.shootingVPosition.position, Vector2.down, lanceHeight, lance.ground);
            if (hitH.collider != null)
            {
                shootPoint = hitH.point + Vector2.up * new Vector2(0, lanceHeight);
            }
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVerticalDown, shootPoint, lance.throwableLancePrefabVerticalDown.transform.rotation);
            return;
        }
        // move shoot location back if too close to wall
        shootPoint = new Vector3(lance.shootingHPosition.position.x, lance.shootingHPosition.position.y, 0);
        BoxCollider2D lanceCollider = lance.throwableLancePrefabHorizontal.GetComponent<BoxCollider2D>();
        float lanceLength = lanceCollider.size.x / 2 + lanceCollider.offset.x;
        RaycastHit2D hit = Physics2D.Raycast(lance.shootingHPosition.position, Vector2.right * lance.player.facingDir, lanceLength, lance.ground);
        if (hit.collider != null)
        {
            shootPoint = hit.point - lance.player.facingDir * new Vector2(lanceLength, 0);
        }
        
        throwingLance = Lance.Instantiate(lance.throwableLancePrefabHorizontal, shootPoint, Quaternion.identity);
        if (lance.player.facingDir == -1)
        {
            throwingLance.transform.right = Vector2.left;
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Set Disappear immediately
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(lance.disappearState);
    }
}
