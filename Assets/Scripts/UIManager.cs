using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI fallenBlocksText;
    [SerializeField]
    private TextMeshProUGUI ballsShotText;
    public void UpdateFallenBlocks(int amountOfFallenBlocks) => fallenBlocksText.SetText(amountOfFallenBlocks.ToString());

    public void UpdateBallsShot(int amountOfBallsShot) => ballsShotText.SetText(amountOfBallsShot.ToString());
}
