using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeRegular : MonoBehaviour {

    public void onPointerEnter()
    {
        //gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Outlined/Uniform");
        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Outlined/Regular");
    }

    public void onPointerExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
    }
}
