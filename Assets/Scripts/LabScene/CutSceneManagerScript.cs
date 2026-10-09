using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManagerScript : MonoBehaviour
{
    public GameObject HotBar;
    public MicroscopeScript microscopeScript;
    public VendingMachineScript vendingMachineScript;
    public UpgradeSystemScript upgradeSystemScript;
    public DataManager dataManager;
    public GameObject Dialogue;
    public DialogueScript dialogueScript;
    public AudioSource typing;
    private bool shouldBeTyping = false;
    private int mouseClicks = 0;
    void Start()
    {
        dataManager.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldBeTyping && !typing.isPlaying)
        {
            typing.Play();
        }
    }

    void StartTyping()
    {
        HotBar.SetActive(false);
        microscopeScript.on = false;
        vendingMachineScript.on = false;
        upgradeSystemScript.on = false;
    }

    void StopTyping()
    {
        HotBar.SetActive(true);
        microscopeScript.on = true;
        vendingMachineScript.on = true;
        upgradeSystemScript.on = true;
    }
}
