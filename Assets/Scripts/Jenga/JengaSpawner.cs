using System;
using System.Collections.Generic;
using ApiInterpretation;
using Cinemachine;
using Ui;
using UnityEngine;

namespace Jenga
{
    public class JengaSpawner : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private JengaTowerFocusControlls _jengaTowerFocusControlls;

        [SerializeField] private GameObject _jengaBlockGlass;
        [SerializeField] private GameObject _jengaBlockWood;
        [SerializeField] private GameObject _jengaBlockStone;
        [SerializeField] private GameObject _helveticaTextPrefab;

        [SerializeField] private float _newJengaSetHeight = 0.25f;
        [SerializeField] private float _JengaSetZPoint = 0.6f;
        [SerializeField] private float _newJengaTowerPoint = 5;
        [SerializeField] private float _helveticaXOffset = -0.5f;
        [SerializeField] private float _helveticaZOffset = -2;
        [SerializeField] private float _helveticaScale = 0.02f;

        #endregion

        #region Private Fields

        private float currentJengaSetHeight;
        private float currentJengaSetRotation;
        private float currentJengaTowerPoint;

        private List<GameObject> _jengaTowers = new();

        #endregion

        #region Public Properties

        public List<JengaBlockData> SixthGradeJengaBlockDatas { get; private set; } = new();
        public List<JengaBlockData> SeventhGradeJengaBlockDatas { get; private set; } = new();
        public List<JengaBlockData> EighthGradeJengaBlockDatas { get; private set; } = new();

        #endregion


        private void OnValidate()
        {
            if (_jengaTowerFocusControlls == null)
                _jengaTowerFocusControlls = FindObjectOfType<JengaTowerFocusControlls>();
        }

        private void Start()
        {
            ApiInterpreter apiInterpreter = new ApiInterpreter();
            apiInterpreter.SetupStandards();

            currentJengaTowerPoint = -_newJengaTowerPoint;

            CreateJenga(apiInterpreter.SixthGradeStandards);

            CreateJenga(apiInterpreter.SeventhGradeStandards);

            CreateJenga(apiInterpreter.EighthGradeStandards);

            _camera.Follow = _jengaTowers[1].transform;
            _camera.LookAt = _jengaTowers[1].transform;
        }

        private void CreateJenga(List<Standard> standards)
        {
            //enqueue the standards
            Queue<Standard> standardsQueue = new Queue<Standard>();
            foreach (var standard in standards)
            {
                standardsQueue.Enqueue(standard);
            }

            GameObject jengaTower = CreateJengaTower(standards[0]);
            _jengaTowers.Add(jengaTower);

            _jengaTowerFocusControlls.CreateJengaTowerButton(jengaTower, standards[0].Grade, _camera);

            List<JengaBlockData> tempJengaBlockDatas = new List<JengaBlockData>();
            while (standardsQueue.Count > 1)
            {
                GameObject jengaSet = CreateJengaSet(jengaTower);

                currentJengaSetHeight += _newJengaSetHeight;
                currentJengaSetRotation += 90;

                var zPos = -_JengaSetZPoint;
                for (int i = 0; i < 3; i++)
                {
                    if (standardsQueue.Count == 0)
                    {
                        continue;
                    }

                    var standard = standardsQueue.Dequeue();

                    CreateJengaBlock(standard, jengaSet, zPos, tempJengaBlockDatas);

                    zPos += _JengaSetZPoint;
                }
            }

            currentJengaSetHeight = 0;
            currentJengaSetRotation = 0;

            currentJengaTowerPoint += _newJengaTowerPoint;

            SetToGradeJengaBlockData(standards[0], tempJengaBlockDatas);
        }

        private GameObject CreateJengaTower(Standard standard)
        {
            GameObject helveticaText = Instantiate(_helveticaTextPrefab);
            helveticaText.transform.position =
                new Vector3(currentJengaTowerPoint + _helveticaXOffset, 0, _helveticaZOffset);
            helveticaText.transform.localScale *= _helveticaScale;
            var text = helveticaText.GetComponent<SimpleHelvetica>();
            text.Text = standard.Grade;
            text.GenerateText();

            GameObject jengaTower = new GameObject(standard.Grade + " Jenga Tower")
            {
                transform =
                {
                    position = new Vector3(currentJengaTowerPoint, 0, 0),
                }
            };

            return jengaTower;
        }

        private GameObject CreateJengaSet(GameObject jengaTower)
        {
            GameObject jengaSet = new GameObject("Jenga Set")
            {
                transform =
                {
                    parent = jengaTower.transform,
                    localPosition = new Vector3(0, currentJengaSetHeight, 0),
                    rotation = Quaternion.Euler(0, currentJengaSetRotation, 0)
                }
            };

            return jengaSet;
        }

        private void CreateJengaBlock(Standard standard, GameObject jengaSet, float zPos,
            List<JengaBlockData> tempJengaBlockDatas)
        {
            GameObject jengaBlock;
            switch (standard.Mastery)
            {
                case 0:
                    jengaBlock = Instantiate(_jengaBlockGlass, jengaSet.transform);
                    SetJengaBlockData(jengaBlock, standard, zPos, tempJengaBlockDatas);
                    break;
                case 1:
                    jengaBlock = Instantiate(_jengaBlockWood, jengaSet.transform);
                    SetJengaBlockData(jengaBlock, standard, zPos, tempJengaBlockDatas);
                    break;
                case 2:
                    jengaBlock = Instantiate(_jengaBlockStone, jengaSet.transform);
                    SetJengaBlockData(jengaBlock, standard, zPos, tempJengaBlockDatas);
                    break;
            }
        }

        private void SetToGradeJengaBlockData(Standard standard, List<JengaBlockData> tempJengaBlockDatas)
        {
            if (standard.Grade.Contains("6"))
            {
                SixthGradeJengaBlockDatas = tempJengaBlockDatas;
            }
            else if (standard.Grade.Contains("7"))
            {
                SeventhGradeJengaBlockDatas = tempJengaBlockDatas;
            }
            else if (standard.Grade.Contains("8"))
            {
                EighthGradeJengaBlockDatas = tempJengaBlockDatas;
            }
        }

        private void SetJengaBlockData(GameObject jengaBlock, Standard standard, float zPos,
            List<JengaBlockData> jengaBlockDatas)
        {
            jengaBlock.transform.localPosition = new Vector3(0, 0, zPos);
            JengaBlockData jengaBlockData = jengaBlock.AddComponent<JengaBlockData>();

            Rigidbody jengaRigidbody = jengaBlock.AddComponent<Rigidbody>();
            jengaRigidbody.isKinematic = true;
            jengaBlockData.SetData(standard, jengaRigidbody);

            jengaBlockDatas.Add(jengaBlockData);
        }
    }
}