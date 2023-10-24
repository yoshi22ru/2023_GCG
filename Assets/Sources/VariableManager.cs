using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VariableManager
{
    static List<Ident_Charactor> player_charactors {
        get;
    }

    public static void AddCharactor(Ident_Charactor ident_Charactor) {
        player_charactors.Add(ident_Charactor);
    }

    public enum Ident_Charactor {
        saru,
        tori,
        inu,
        i,
    }
}
