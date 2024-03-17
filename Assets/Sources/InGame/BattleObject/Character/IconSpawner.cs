using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSpawner : MonoBehaviour
{
    [SerializeField] int rowCount = 2;
    [SerializeField] int columnCount = 4;
    [SerializeField] int index;
    public Button button;
    Button newButton;

    void Start()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                index = i * columnCount + j;

                newButton = Instantiate(button);

                newButton.transform.SetParent(button.transform.parent);

                float xPos = button.transform.position.x + (button.GetComponent<RectTransform>().rect.width + 10f) * j;
                float yPos = button.transform.position.y - (button.GetComponent<RectTransform>().rect.height + 10f) * i;
                newButton.transform.position = new Vector3(xPos, yPos, button.transform.position.z);

                newButton.gameObject.name = button.gameObject.name + " (Copy " + (index + 1) + ")";

                int buttonNumber = index + 1;
                newButton.onClick.AddListener(() => Click(buttonNumber));
            }
        }
    }

    public void Click(int buttonNumber)
    {
        Debug.Log("Button " + buttonNumber + " was clicked.");
    }
}
