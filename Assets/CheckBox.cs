using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    Animator anim;
    private Button checkBox;
    [SerializeField] private Sprite checkT;
    [SerializeField] private Sprite checkF;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        checkBox = gameObject.GetComponent<Button>();
    }

    public void Clicked()
    {
        anim.Play("CheckBox");
    }

    public void ChangeSprite()
    {
        anim.enabled = false;
        checkBox.image.sprite = checkT;
    }
}
