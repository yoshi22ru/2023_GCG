using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sources.Sync;
using UnityEngine.UI;

public class btnFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip HighlightedFX;
    public AudioClip ClickFX;
    public GameObject InputPanels;

    public void HighlightedSound()
    {
        myFX.PlayOneShot(HighlightedFX);
    }
    public void ClickSound()
    {
        myFX.PlayOneShot(ClickFX);
    }

    public static void SceneToModeSelect()
    {
        FadeManager.Instance.LoadScene("ModeSelectScene", 0.3f);
    }

    public void SceneToTraining()
    {
        FadeManager.Instance.LoadScene("TrainingScene", 0.3f);
    }

    public void SceneToOptionChange()
    {
        FadeManager.Instance.LoadScene("OptionChangeScene", 0.3f);
    }

    public void SceneToCredit()
    {
        FadeManager.Instance.LoadScene("CreditScene", 0.3f);
    }

    public void SceneToCharaSelectFreeMatch()
    {
        VariableManager.RoomOption = RoomOption.Public;
        FadeManager.Instance.LoadScene("CharacterSelectSceneFree", 0.3f);
    }

    public void SceneToCharaSelectPrivateMatch()
    {
        VariableManager.RoomOption = RoomOption.PrivateHost;
        FadeManager.Instance.LoadScene("CharacterSelectSceneFree", 0.3f);
    }

    public static void SceneToMatchingWaitFree()
    {
        FadeManager.Instance.LoadScene("MatchingWaitingSceneFree", 0.3f);
    }

    public void SceneToMatchingWaitPrivate()
    {
        FadeManager.Instance.LoadScene("MatchingWaitingScenePrivate", 0.3f);
    }

    public static void SceneToBattleScene()
    {
        FadeManager.Instance.LoadScene("Stage1", 0.3f);
    }

    public void ActiveInputPanel()
    {
        InputPanels.SetActive(true);
    }

    public void PassiveInputPanel()
    {
        InputPanels.SetActive(false);
    }
}