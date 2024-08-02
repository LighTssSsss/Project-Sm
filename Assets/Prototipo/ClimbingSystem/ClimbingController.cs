using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    public CheckerEnviroment ec;
    private MoveAndAnimatorController mv;

    ClimbingPoint currentClimbPoint;

    public float inOutValue;
    public float upDownValue;
    public float leftRightValue;

    private void Awake()
    {
        mv = GetComponent<MoveAndAnimatorController>();
        ec = GetComponent<CheckerEnviroment>();
        

    }
    private void Update()
    {
        if (!mv.playerHaging)
        {
            if (mv.isJumpPressed && !mv.playerInAction && ec.obstacleCollision == false)
            {
                if (ec.CheckClimbing(transform.forward, out RaycastHit climbInfo))
                {
                    // Debug.Log("Climb Point Found");
                    currentClimbPoint = climbInfo.transform.GetComponent<ClimbingPoint>();



                    mv.SetControl(false);
                    inOutValue = 0.236f;
                    upDownValue = 0.137f;
                    leftRightValue = 0.2f;
                    mv.isClimbing = true;
                    StartCoroutine(ClimbToLedge("IdleToClimb", climbInfo.transform, 0.40f, 54f, playerHandOffset : new Vector3(inOutValue, upDownValue, leftRightValue)));
                }
            }

            if (mv.isLeavePressed && !mv.playerInAction)
            {
                if (ec.ChechkDropClimbPoint(out RaycastHit DropHit))
                {
                    currentClimbPoint = GetNearestClimbingPoint(DropHit.transform, DropHit.point);
                    mv.SetControl(false);

                    //SethHandOffset
                    inOutValue = 0.16f;
                    upDownValue = 0.088f;
                    leftRightValue = 0.16f;

                    StartCoroutine(ClimbToLedge("DropFreehang", currentClimbPoint.transform, 0.41f, 0.54f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }


                return;
            }

        }

        else
        {
            //Ledge to ledge parkour actions

            //Baja
            if (mv.isLeavePressed && !mv.playerInAction)
            {
                StartCoroutine(Droped());
                return;
            }


           

            float horizontal = Mathf.Round(Input.GetAxisRaw("Horizontal"));
            float vertical = Mathf.Round(Input.GetAxisRaw("Vertical"));


            var inputDirection = new Vector2(horizontal, vertical);

            if(mv.playerInAction || inputDirection == Vector2.zero)
            {
                return;
            }

            //Sube
            if (currentClimbPoint.mountPoint && inputDirection.y == 1 && mv.isJumpPressed)
            {
                StartCoroutine(ClimbToTop());
                Debug.Log("Sube");
                return;
            }

            var neighbour = currentClimbPoint.GetNeighbour(inputDirection);

           


            if(neighbour == null)
            {
                return;
            }

            if(neighbour.connectionType == ConnectionType.Jump && mv.isJumpPressed)
            {
                currentClimbPoint = neighbour.climbingPoint;

                if(neighbour.pointDirection.y == 1)
                {
                    inOutValue = 0.16f;
                    upDownValue = 0.088f;
                    leftRightValue = 0.16f;
                    StartCoroutine(ClimbToLedge("ClimbUp", currentClimbPoint.transform, 0.34f, 0.64f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }

               else if (neighbour.pointDirection.y == -1)
                {
                    inOutValue = 0.2f;
                    upDownValue = 0.2f;
                    leftRightValue = 0.2f;
                    StartCoroutine(ClimbToLedge("ClimbDrop", currentClimbPoint.transform, 0.31f, 0.68f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }


                else if (neighbour.pointDirection.x == 1)
                {
                    inOutValue = 0.16f;
                    upDownValue = 0.088f;
                    leftRightValue = 0.16f;
                    StartCoroutine(ClimbToLedge("ClimRight", currentClimbPoint.transform, 0.20f, 0.51f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }

                else if (neighbour.pointDirection.x == -1)
                {
                    inOutValue = 0.16f;
                    upDownValue = 0.088f;
                    leftRightValue = 0.16f;
                    StartCoroutine(ClimbToLedge("ClimbLeft", currentClimbPoint.transform, 0.20f, 0.51f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }
            }

            else if(neighbour.connectionType == ConnectionType.Move)
            {
                currentClimbPoint = neighbour.climbingPoint;

                if(neighbour.pointDirection.x == 1)
                {
                    inOutValue = 0.1f;
                    upDownValue = 0.02f;
                    leftRightValue = 0.2f;
                    StartCoroutine(ClimbToLedge("ShimmyRight", currentClimbPoint.transform, 0f, 0.30f, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }

                if (neighbour.pointDirection.x == -1)
                {
                    inOutValue = 0.1f;
                    upDownValue = 0.02f;
                    leftRightValue = 0.2f;
                    StartCoroutine(ClimbToLedge("Shimmyleft", currentClimbPoint.transform, 0f, 0.30f, AvatarTarget.LeftHand, playerHandOffset: new Vector3(inOutValue, upDownValue, leftRightValue)));
                }
            }


        }
        
    }

    IEnumerator ClimbToLedge(string animationName, Transform ledgePoint, float compareStartTime, float compareEndTime, AvatarTarget hand = AvatarTarget.RightHand
        , Vector3? playerHandOffset = null)
    {

        var compareParameters = new CompareTargetParameter()
        {
            position = SetHandPosition(ledgePoint, hand, playerHandOffset),
            bodyPart = hand,
            positionWeight = Vector3.one,
            startTime = compareStartTime,
            endTime = compareEndTime
        };

        var requiredRotation = Quaternion.LookRotation(-ledgePoint.forward);

        yield return mv.PerformAction(animationName, compareParameters, requiredRotation, true);

        mv.playerHaging = true;
    }

    Vector3 SetHandPosition(Transform ledge, AvatarTarget hand, Vector3 ? playerhandOffset)
    {
        var offSetValue = (playerhandOffset != null) ? playerhandOffset.Value : new Vector3(inOutValue, upDownValue, leftRightValue);


       // inOutValue = 0.236f;
        //upDownValue = 0.137f;
        //leftRightValue = 0.2f;

        var handDirection = (hand == AvatarTarget.RightHand) ? ledge.right : -ledge.right;

        return ledge.position + ledge.forward * inOutValue + Vector3.up * upDownValue - handDirection * leftRightValue;
    }


    IEnumerator Droped()
    {
        mv.playerHaging = false;
        yield return mv.PerformAction("JumpWall1");
        mv.isLeavePressed = false;
        mv.ResetRequiredRotation();
        mv.SetControl(true);
    }

    IEnumerator ClimbToTop()
    {
        mv.playerHaging = false;
        yield return mv.PerformAction("ClimbTop");

        mv.EnableCC(true);

        yield return new WaitForSeconds(0.5f);


        mv.ResetRequiredRotation();
        mv.SetControl(true);
    }

    ClimbingPoint GetNearestClimbingPoint(Transform dropClimbPoint, Vector3 hitPoint)
    {
        var points = dropClimbPoint.GetComponentsInChildren<ClimbingPoint>();

        ClimbingPoint nearestPoint = null;

        float nearestPointDistance = Mathf.Infinity;


        foreach(var point in points)
        {
            float distance = Vector3.Distance(point.transform.position, hitPoint);

            if(distance < nearestPointDistance)
            {
                nearestPoint = point;
                nearestPointDistance = distance;
            }

            
        }
        return nearestPoint;
    }

}
