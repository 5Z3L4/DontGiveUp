using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI Text;
    bool goDown=false;

    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.fontSize < 90 && !goDown)
            Text.fontSize += 0.5f * Time.deltaTime * 50;
        else if (Text.fontSize >= 90)
            goDown = true;
        if (Text.fontSize > 50 && goDown)
            Text.fontSize -= 0.5f * Time.deltaTime * 50;
        else if (Text.fontSize <= 50)
            goDown = false;

    }
}
