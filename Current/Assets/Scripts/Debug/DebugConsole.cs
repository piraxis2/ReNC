using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    bool console = false;

    InputField inputfield;
    SkillRange skilltest;
    Skill testskill;

    // Start is called before the first frame update
    void Start()
    {
        inputfield = GetComponentInChildren<InputField>(true);
        skilltest = GetComponentInChildren<SkillRange>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.BackQuote))
        {

            console = !console;

            inputfield.gameObject.SetActive(console);

            if (console)
                inputfield.Select();
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {

            switch (inputfield.text)
            {
                case "teststart": skilltest.gameObject.SetActive(true); inputfield.text = "";
                    break;
                case "testend": skilltest.gameObject.SetActive(false); inputfield.text = "";
                    break;

            }

            if(skilltest.gameObject.activeInHierarchy)
            {
                string text = inputfield.text;
                if(text!="")
                    skilltest.skill = SkillContainer.Instance.FindSkill(text);
            }

            inputfield.text = "";

        }

 
    }
}
