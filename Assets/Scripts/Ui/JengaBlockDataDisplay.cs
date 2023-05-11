using System;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class JengaBlockDataDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI informationText;

        private void FixedUpdate()
        {
            //When the user right clicks on a block, the block displays data
            if (Input.GetMouseButtonDown(1))
            {
                //Get the block that the user clicked on
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    //Get the block's data
                    var jengaBlockData = hit.collider.gameObject.GetComponent<Jenga.JengaBlockData>();
                    if (jengaBlockData != null)
                    {
                        //Display the block's data
                        informationText.text = jengaBlockData.Standard.Grade + ": " +
                                               jengaBlockData.Standard.DomainName + "\n" +
                                               jengaBlockData.Standard.ClusterDescription + "\n" +
                                               jengaBlockData.Standard.StandardId + ": " +
                                               jengaBlockData.Standard.StandardDescription;
                    }
                }
            }
        }
    }
}