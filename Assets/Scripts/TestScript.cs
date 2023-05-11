using System.Collections;
using System.Collections.Generic;
using ApiInterpretation;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ApiInterpreter apiInterpreter = new ApiInterpreter();
        IEnumerable<Standard> standards = apiInterpreter.GetStandards();
    }
}
