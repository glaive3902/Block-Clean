using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSquareAnim : MonoBehaviour
{
    public Image activeImage;
    
	public void Disable()
    {
        activeImage.gameObject.SetActive(false);
    }

    public void DisableAnim()
    {
        GetComponent<Animator>().enabled = false;
    }
}
