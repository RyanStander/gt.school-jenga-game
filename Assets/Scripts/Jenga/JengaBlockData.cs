using ApiInterpretation;
using UnityEngine;

namespace Jenga
{
    public class JengaBlockData : MonoBehaviour
    {
        public Standard Standard { get; set; }
        private Rigidbody rigidbody;

        public void SetData(Standard standard, Rigidbody rigidbody)
        {
            Standard = standard;
            this.rigidbody = rigidbody;
        }

        public void EnablePhysics()
        {
            rigidbody.isKinematic = false;
        }
    }
}
