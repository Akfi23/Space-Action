﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Image TargetIndicatorImage;
    public Image OffScreenTargetIndicator;
    public Image CurrentHP;
    public Image FakeHP;
    public float OutOfSightOffset = 20f;
    public float yPos;
    public TMP_Text DamageText;
    public bool isRotate = true;
    public bool isHealth = true;

    private float outOfSightOffest { get { return OutOfSightOffset /* canvasRect.localScale.x*/; } }
    private GameObject target;
    private Camera mainCamera;
    private RectTransform canvasRect;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void InitialiseTargetIndicator(Camera mainCamera, Canvas canvas, GameObject target = null)
    {
        this.target = target;
        this.mainCamera = mainCamera;
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    public void UpdateTarget(GameObject target)
    {
        this.target = target;
    }

    public void UpdateTargetIndicator()
    {
        SetIndicatorPosition();
    }

    protected void SetIndicatorPosition()
    {
        //Get the position of the target in relation to the screenSpace 
        Vector3 indicatorPosition = mainCamera.WorldToScreenPoint(target.transform.position + Vector3.up * yPos);
        //Debug.Log("GO: "+ gameObject.name + "; slPos: " + indicatorPosition + "; cvWidt: " + canvasRect.rect.width + "; cvHeight: " + canvasRect.rect.height);

        //In case the target is both in front of the camera and within the bounds of its frustrum
        if (indicatorPosition.z >= 0f & indicatorPosition.x <= canvasRect.rect.width * canvasRect.localScale.x
         & indicatorPosition.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPosition.x >= 0f & indicatorPosition.y >= 0f)
        {
            //Set z to zero since it's not needed and only causes issues (too far away from Camera to be shown!)
            indicatorPosition.z = 0f;

            //Target is in sight, change indicator parts around accordingly
            targetOutOfSight(false, indicatorPosition);
        }

        //In case the target is in front of the ship, but out of sight
        else if (indicatorPosition.z >= 0f)
        {
            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
        }
        else
        {
            //Invert indicatorPosition! Otherwise the indicator's positioning will invert if the target is on the backside of the camera!
            indicatorPosition *= -1f;

            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);

        }

        //Set the position of the indicator
        rectTransform.position = indicatorPosition;

    }

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPosition)
    {
        //Set indicatorPosition.z to 0f; We don't need that and it'll actually cause issues if it's outside the camera range (which easily happens in my case)
        indicatorPosition.z = 0f;

        //Calculate Center of Canvas and subtract from the indicator position to have indicatorCoordinates from the Canvas Center instead the bottom left!
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        //Calculate if Vector to target intersects (first) with y border of canvas rect or if Vector intersects (first) with x border:
        //This is required to see which border needs to be set to the max value and at which border the indicator needs to be moved (up & down or left & right)
        float divX = (canvasRect.rect.width / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.x);
        float divY = (canvasRect.rect.height / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.y);

        //In case it intersects with x border first, put the x-one to the border and adjust the y-one accordingly (Trigonometry)
        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width * 0.5f - outOfSightOffest) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }

        //In case it intersects with y border first, put the y-one to the border and adjust the x-one accordingly (Trigonometry)
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);

            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOfSightOffest) * canvasRect.localScale.y;
            indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }

        //Change the indicator Position back to the actual rectTransform coordinate system and return indicatorPosition
        indicatorPosition += canvasCenter;
        return indicatorPosition;
    }

    private void targetOutOfSight(bool oos, Vector3 indicatorPosition)
    {
        //In Case the indicator is OutOfSight
        if (oos)
        {
            //Activate and Deactivate some stuff
            if (OffScreenTargetIndicator.gameObject.activeSelf == false) OffScreenTargetIndicator.gameObject.SetActive(true);

            if (TargetIndicatorImage.isActiveAndEnabled == true)
            {
                TargetIndicatorImage.enabled = false;

                if (isHealth)
                {
                    CurrentHP.enabled = false;
                    FakeHP.enabled = false;
                }
            }

            //Set the rotation of the OutOfSight direction indicator
            if (isRotate)
                OffScreenTargetIndicator.rectTransform.rotation = Quaternion.Euler(rotationOutOfSightTargetindicator(indicatorPosition));

            //outOfSightArrow.rectTransform.rotation  = Quaternion.LookRotation(indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f)) ;
            /*outOfSightArrow.rectTransform.rotation = Quaternion.LookRotation(indicatorPosition);
            viewVector = indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f);
            
            //Debug.Log("CanvasRectCenter: " + canvasRect.rect.center);
            outOfSightArrow.rectTransform.rotation *= Quaternion.Euler(0f,90f,0f);*/
        }

        //In case that the indicator is InSight, turn on the inSight stuff and turn off the OOS stuff.
        else
        {
            if (OffScreenTargetIndicator.gameObject.activeSelf == true) OffScreenTargetIndicator.gameObject.SetActive(false);

            if (TargetIndicatorImage.isActiveAndEnabled == false)
            {
                TargetIndicatorImage.enabled = true;
                if (isHealth)
                {
                    CurrentHP.enabled = true;
                    FakeHP.enabled = true;
                }
            }
        }
    }

    private Vector3 rotationOutOfSightTargetindicator(Vector3 indicatorPosition)
    {
        //Calculate the canvasCenter
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;

        //Calculate the signedAngle between the position of the indicator and the Direction up.
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        //return the angle as a rotation Vector
        return new Vector3(0f, 0f, angle);
    }
}
