using UnityEngine;

public class AttackInfo
{
    // first digit 0/1   =>   is up pressed
    // second digit 0/1  =>   is down pressed
    // use mod2 and /2 to get info, right most digit is the first digit
    public int userInput;

    public AttackInfo(int userInput)
    {
        this.userInput = userInput;
    }

    public bool isUpPressed()
    {
        return userInput % 2 == 1;
    }
    public bool isDownPressed()
    {
        return userInput / 2 == 1;
    }
}
