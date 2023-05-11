using ApiInterpretation;
using UnityEngine;

namespace Jenga
{
    public class JengaBlockData : MonoBehaviour
    {
        public Standard Standard { get; set; }
        
        public void SetStandard(Standard standard)
        {
            Standard = standard;
        }
    }
}
