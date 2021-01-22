using Microsoft.VisualBasic;
using System;

namespace PrimeService
{
    public class PrimeService
    {
        public bool SirenSiretIsCorrect(string value)
        {
            // Renverse l'ordre des caractères dans la chaine de caractères
            value = Strings.StrReverse(value);

            // Création de la variable qui contiendra l'addition des nombres
            double total = 0;

            // Boucle permettant de parcourir la chaine de caractère
            for (int i = 0; i < value.Length; i++)
            {
                // Je stocke, après l'avoir transformé en double, le chiffre trouvé dans la chaine de caractères à l'index i
                double nb = Char.GetNumericValue(value[i]);

                // Condition controlant si la position sur la chaine de caractères est pair
                if ((i+1) % 2 == 0)
                {
                    // Condition controlant si le chifftre précédemment récupérer est sup ou égale à 5 et dont la multiplication de ce chiffre par 2 donnera un nombre à 2 chiffre
                    if (nb >= 5)
                    {
                        nb = nb * 2;
                        // Ajout directement de 1 dans le total car nb * 2 ne dépassera jamais 18
                        total += 1;
                        // Ajout de la différence entre le nombre et 10 dans le total, cela correspond au deuxième chiffre
                        total += (nb - 10);
                    } else
                    {
                        total += (nb * 2);
                    }
                }
                else
                {
                    total += nb;
                }
            }

            // Condition controlant si le total est un multiple de 10
            if (total % 10 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
