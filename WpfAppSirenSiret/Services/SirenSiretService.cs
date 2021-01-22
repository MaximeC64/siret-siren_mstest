using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppSirenSiret.Models;

namespace WpfAppSirenSiret.Services
{
    public class SirenSiretService
    {
        #region Singleton

        private static SirenSiretService instance = null;
        public static SirenSiretService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SirenSiretService();
                }
                return instance;
            }
        }

        // EMPECHE L'INSTANCIATION DE LA CLASSE
        private SirenSiretService()
        {
        }

        #endregion

        // Fonction permettant de vérifier le SIREN ou le SIRET (même code que dans le PrimeService)
        public bool SirenSiretCheck(string value)
        {
            value = Reverse(value);
            double total = 0;
            for (int i = 0; i < value.Length; i++)
            {
                double nb = Char.GetNumericValue(value[i]);
                if ((i + 1) % 2 == 0)
                {
                    if (nb >= 5)
                    {
                        nb = nb * 2;
                        total += 1;
                        total += (nb - 10);
                    }
                    else
                    {
                        total += (nb * 2);
                    }
                }
                else
                {
                    total += nb;
                }
            }
            if (total % 10 == 0)
            {
                return true;
            }
            return false;
        }

        // Fonction permettant d'inverser une chaine de caractères
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
