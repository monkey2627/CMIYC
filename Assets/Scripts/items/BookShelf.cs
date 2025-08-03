using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : ItemBase
{
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("“How to Make Your Cauldron Stop Screaming”\n“Why Your Potted Plants Are Always Staring at You: An Introduction to Phytopsychology”\n" +
                                       "“Modern Household Sorcery: Making Your Broom Move on Its Own”\n......and “Feline Conspiracy Theories: From Ruling the Household to World Domination” …… Meow?");
    }

    
}
