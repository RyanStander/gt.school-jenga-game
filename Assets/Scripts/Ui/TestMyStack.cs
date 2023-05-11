using System;
using System.Collections.Generic;
using Jenga;
using UnityEngine;

namespace Ui
{
    public class TestMyStack : MonoBehaviour
    {
        [SerializeField] private JengaSpawner _jengaSpawner;
        [SerializeField] private JengaTowerFocusControlls _jengaTowerFocusControlls;

        private void OnValidate()
        {
            if (_jengaSpawner == null)
                _jengaSpawner = FindObjectOfType<JengaSpawner>();

            if (_jengaTowerFocusControlls == null)
                _jengaTowerFocusControlls = FindObjectOfType<JengaTowerFocusControlls>();
        }

        public void TestMyStackButton()
        {
            switch (_jengaTowerFocusControlls.FocussedJengaTowerIndex)
            {
                case 6:
                    EnablePhysicsAndRemoveGlass(_jengaSpawner.SixthGradeJengaBlockDatas);
                    break;
                case 7:
                    EnablePhysicsAndRemoveGlass(_jengaSpawner.SeventhGradeJengaBlockDatas);
                    break;
                case 8:
                    EnablePhysicsAndRemoveGlass(_jengaSpawner.EighthGradeJengaBlockDatas);
                    break;
            }
        }

        private void EnablePhysicsAndRemoveGlass(List<JengaBlockData> jengaBlockDatas)
        {
            foreach (var jengaBlockData in jengaBlockDatas)
            {
                if (jengaBlockData.Standard.Mastery == 0)
                {
                    Destroy(jengaBlockData.gameObject);
                }
                else
                {
                    jengaBlockData.EnablePhysics();
                }
            }
        }
    }
}