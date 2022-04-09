using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{


    [SerializeField] private GameObject planeElev;
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;

    [SerializeField] private ObjectCollector objClass;

    [SerializeField] Image stageImage;

    private bool oneTimeWork = true;

    private bool waitForCollectables = false;

    [SerializeField] private bool isLastStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.tag== "FirstStage")
        {
            LevelSystem.instance.gameActive = false;
            StartCoroutine(WaitForBoxes());
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.tag == "FirstStage")
        {
          
            // Elevator up 
            if (objClass.boxCount >= objClass.MinCount)
            {
                if (oneTimeWork)
                {
                    oneTimeWork = false;
                    if (isLastStep)
                    {
                        gameObject.tag = "Untagged";
                        LevelSystem.instance.FinishGame();
                        NextLevelGo();
                        stageImage.color = Color.green;
                    }
                    else
                    {
                        // UI First part finished
                        stageImage.color = Color.green;
                        gameObject.tag = "Untagged";
                        StartCoroutine(Elevator());
                    }
                }
            } 
            else if (waitForCollectables && objClass.boxCount < objClass.MinCount)
            {
                LevelSystem.instance.GameOver();
                gameObject.tag = "Untagged";
            }

        }
    }

    IEnumerator Elevator()
    {
        planeElev.transform.DOMove(new Vector3(planeElev.transform.position.x, 0, 0), 2f, false);
        LeftDoor.transform.DOLocalMove(new Vector3(0.915905f,- 2.53f, 8.68f), 2f);
        RightDoor.transform.DOLocalMove(new Vector3(0.915905f,- 2.53f, -6.39f), 2f);

        yield return new WaitForSeconds(3f);
        LevelSystem.instance.gameActive = true;
        objClass.boxCount = 0;
    }
    private void NextLevelGo()
    {
        planeElev.transform.DOMove(new Vector3(planeElev.transform.position.x, 0, 0), 2f, false);
        LeftDoor.transform.DOLocalMove(new Vector3(0.915905f, -2.53f, 8.68f), 2f);
        RightDoor.transform.DOLocalMove(new Vector3(0.915905f, -2.53f, -6.39f), 2f);

    }

    IEnumerator WaitForBoxes()
    {
        yield return new WaitForSeconds(6f);
        waitForCollectables = true;
    }

}
