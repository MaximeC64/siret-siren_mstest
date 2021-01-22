using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppSirenSiret.Models;
using WpfAppSirenSiret.Services;

namespace WpfAppSirenSiret.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SirenSiret SirenSiret { get; }

        //Constructeur
        public MainWindowViewModel()
        {
            SirenSiret = new SirenSiret();
        }

        public string SirenSiretValue
        {
            get
            {
                return SirenSiret.Value;
            }
            // Permet d'insérer directe la valeur renseigné dans l'attribut "value" de l'objet "SirenSiret"
            set
            {
                SirenSiret.Value = value;
                OnPropertyChanged();
                OnPropertyChanged("Value");
            }
        }

        private string result;
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        // Commande liée au bouton "Vérifier" de la vue
        private ICommand commandCheck;
        public ICommand CommandCheck
        {
            get
            {
                if (commandCheck == null)
                {
                    commandCheck = new RelayCommand((object sender) =>
                    {
                        // Condition permettant de définir si la valeur renseigné est potentiellement un SIREN ou un SIRET par son nombre de caractères 
                        if (SirenSiret.Value.Length == 14 || SirenSiret.Value.Length == 9)
                        {
                            // Condition permettant de définir si c'est un SIREN ou un SIRET et d'ajouter le résultat dans l'attribut "type" de l'objet
                            if (SirenSiret.Value.Length == 14)
                            {
                                SirenSiret.Type = "SIRET";
                            }
                            else
                            {
                                SirenSiret.Type = "SIREN";
                            }

                            // Je stocke dans la variable IsCorrect le résultat de la fonction qui se trouve dans la classe SirenSiretService
                            // bool IsCorrect = SirenSiretService.Instance.SirenSiretCheck(SirenSiret.Value);
                            bool IsCorrect = SirenSiretCheck(SirenSiret.Value);

                            // Condition permettant d'envoyer différent messagebox suivant si le Siren ou le Siret et correct ou non
                            if (IsCorrect)
                            {
                                Result = "Le " + SirenSiret.Type + " est correct. Il comprend bien " + SirenSiret.Value.Length + " chiffres et est bien un multiple de 10.";
                                //MessageBox.Show("Le " + SirenSiret.Type + " est correct. Il comprend bien " + SirenSiret.Value.Length + " chiffres et est bien un multiple de 10.", "Message d'information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                Result = "Erreur : Le " + SirenSiret.Type + " : " + SirenSiret.Value + ", est incorrect.";
                                //MessageBox.Show("Erreur : Le " + SirenSiret.Type + " : " + SirenSiret.Value + ", est incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        } else
                        {
                            // Si le nombre de caractères ne correspond pas, envoie un message d'erreur
                            Result = "Erreur : Un SIRET doit avoir 14 chiffres et un SIREN doit avoir 9 chiffres.";
                            // Si le nombre de caractères ne correspond pas, envoie un messagebox d'erreur
                            //MessageBox.Show("Erreur : Un SIRET doit avoir 14 chiffres et un SIREN doit avoir 9 chiffres.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                }
                return commandCheck;
            }
        }

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
