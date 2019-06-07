using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{       
    public Text helpText;
    void Start()
    {
        helpText.text = "- Найти одну из указанного количества фигур одного размера. \n\n- Фигуры могут быть разных цветов и расположены под разным углом. \n\n- Если фигуры разных типов, то искать нужно только среди фигур одного типа. "; 
    }
}
