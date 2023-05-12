using System;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class JengaBlockDataDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _informationText;
        [SerializeField] private Camera _camera;

        private void OnValidate()
        {
            if (_camera == null)
                _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit))
                {
                    //Get the block's data
                    var jengaBlockData = hit.collider.gameObject.GetComponent<Jenga.JengaBlockData>();
                    if (jengaBlockData != null)
                    {
                        //Display the block's data
                        _informationText.text = jengaBlockData.Standard.Grade + ": " +
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