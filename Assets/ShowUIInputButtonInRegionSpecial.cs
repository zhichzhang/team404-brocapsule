using System.Collections.Generic;
using UnityEngine;

public class ShowUIInputButtonInRegionSpecial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<UIInputButtonFadeInOut> uiButtons;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerInfo.instance.player.Mana > 0)
        {
            
            if (collision.CompareTag("Player"))
            {
                
                foreach (var button in uiButtons)
                {
                    button.Show();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var button in uiButtons)
            {
                button.Hide();
            }
        }
    }
}
