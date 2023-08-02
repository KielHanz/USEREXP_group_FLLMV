using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHolder : MonoBehaviour
{
    public TMP_Text Objective;

    public void ChangeObjective1Color()
    {
        
        Objective.color = Color.green;
    }
}
