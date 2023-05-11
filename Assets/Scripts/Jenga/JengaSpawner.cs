using System.Collections.Generic;
using ApiInterpretation;
using UnityEngine;

namespace Jenga
{
    public class JengaSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _jengaBlockGlass;
        [SerializeField] private GameObject _jengaBlockWood;
        [SerializeField] private GameObject _jengaBlockStone;

        [SerializeField] private float _newJengaSetHeight = 0.25f;
        [SerializeField] private float _JengaSetZPoint = 0.6f;
        [SerializeField] private float _newJengaTowerPoint = 5;

        private float currentJengaSetHeight;
        private float currentJengaSetRotation;
        private float currentJengaTowerPoint;

        private void Awake()
        {
            ApiInterpreter apiInterpreter = new ApiInterpreter();
            apiInterpreter.SetupStandards();
            
            currentJengaTowerPoint = -_newJengaTowerPoint;
            
            CreateJengaTower(apiInterpreter.SixthGradeStandards);
            
            CreateJengaTower(apiInterpreter.SeventhGradeStandards);
            
            CreateJengaTower(apiInterpreter.EighthGradeStandards);
        }

        private void CreateJengaTower(List<Standard> standards)
        {
            //enqueue the standards
            Queue<Standard> standardsQueue = new Queue<Standard>();
            foreach (var standard in standards)
            {
                standardsQueue.Enqueue(standard);
            }

            GameObject jengaTower = new GameObject( standards[0].Grade+" Jenga Tower")
            {
                transform =
                {
                    position = new Vector3(currentJengaTowerPoint, 0, 0),
                }
            };

            while (standardsQueue.Count > 1)
            {
                //create a new jengaSet as a child of the jengaTower
                GameObject jengaSet = new GameObject("Jenga Set")
                {
                    transform =
                    {
                        parent = jengaTower.transform,
                        localPosition = new Vector3(0, currentJengaSetHeight, 0),
                        rotation = Quaternion.Euler(0, currentJengaSetRotation, 0)
                    }
                };

                //set the parent of the jengaSet to the jengaTower
                currentJengaSetHeight += _newJengaSetHeight;
                currentJengaSetRotation += 90;
            
                var zPos = -_JengaSetZPoint;
                for (int i = 0; i < 3; i++)
                {
                    var standard = standardsQueue.Dequeue();
                    GameObject jengaBlock;
                    switch (standard.Mastery)
                    {
                        case 0:
                            jengaBlock=Instantiate(_jengaBlockGlass,jengaSet.transform);
                            jengaBlock.transform.localPosition = new Vector3(0, 0, zPos);
                            break;
                        case 1:
                            jengaBlock=Instantiate(_jengaBlockWood,jengaSet.transform);
                            jengaBlock.transform.localPosition = new Vector3(0, 0, zPos);
                            break;
                        case 2:
                            jengaBlock=Instantiate(_jengaBlockStone,jengaSet.transform);
                            jengaBlock.transform.localPosition = new Vector3(0, 0, zPos);
                            break;
                    }
                    zPos += _JengaSetZPoint;
                }
            }
            currentJengaSetHeight = 0;
            currentJengaSetRotation = 0;
            
            currentJengaTowerPoint +=_newJengaTowerPoint;
        }
    }
}