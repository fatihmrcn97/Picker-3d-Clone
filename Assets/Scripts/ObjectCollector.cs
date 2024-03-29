using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCollector : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] public int boxCount;

    [SerializeField] public int MinCount;

  

    private void Start()
    {
        countText.text = boxCount + "/" + MinCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            boxCount++;
            countText.text = boxCount + "/" + MinCount;
            Destroy(other.gameObject);

        }

    }





}
