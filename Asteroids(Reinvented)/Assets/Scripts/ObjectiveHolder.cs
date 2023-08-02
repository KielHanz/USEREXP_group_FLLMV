using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHolder : MonoBehaviour
{
    public TMP_Text Objective;

    public void ChangeObjectiveColor()
    {
        if (Objective != null)
        {
            Objective.color = Color.green;
        }
    }
}
