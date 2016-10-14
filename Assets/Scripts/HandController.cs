using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {
    private Light Torch;
    private Animator anim;
	// Use this for initialization
	private void Start () {
        Torch = (Light)GameObject.Find("FlashLight").GetComponent("Light");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	private void FixedUpdate() {
        if (Torch.enabled == true)
        {
            anim.SetBool("torch", true);
        }
        else
        {
            anim.SetBool("torch", false);
        }
        float move = Input.GetAxis("Horizontal");


        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("speed", Mathf.Abs(move));
    }
}
