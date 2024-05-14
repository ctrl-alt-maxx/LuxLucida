using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAnimationVirtualJoystick
{
    //Contourne une limitation de Unity au niveau des �v�nements
    //De cette mani�re, on �vite de spammer des �v�nements pour toutes les animations qui ont un facteur
    //d'interpolation non nul.
    //Appeler cette fonction sur une direction normalis�e ou non pour obtenir un des 4 vecteurs
    //suivant: (1,0) (-1,0) (0,1) (0,-1)
    public static Vector2 ForceDirectionAxe(Vector2 InDirection)
    {
        //Trouve l'index du vecteur de l'axe dominant
        //Index 0 est x
        //Index 1 est y
        int indexAxeDominant = Mathf.Abs(InDirection.x) > Mathf.Abs(InDirection.y) ? 0 : 1;
        //Signe de l'axe dominant
        float signeAxeDominant = InDirection[indexAxeDominant] > 0.0f ? 1.0f : -1.0f;
        Vector2 resultat = Vector2.zero;
        resultat[indexAxeDominant] = signeAxeDominant;
        return resultat;
    }
}
