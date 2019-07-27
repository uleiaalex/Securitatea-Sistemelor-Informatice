using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneratePassword
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        public int selectedOptions = 0;

        //Strings
        private string chars = "!@#$%^&*?()~`-=_+{}[]:'<>,./|;";
        private string letters = "abcdefghijklmnopqrstuvwxyz";
        private string letterS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string numbers = "0123456789";


        private string charsBasic = "!@#$%^&*?";
        private string similiarLetters = "iol";
        private string similiarLetterS = "IO";
        private string similiarNumber = "10";

        private string auxChars = "";
        private string auxNumbers = "";
        private string auxLetterS = "";
        private string auxLetters = "";

        private string thePassword = "";

        public MainWindow()
        {
            InitializeComponent();
            Awake();
        }
        private void Awake()
        {
            for (int i = 6; i <= 15; i++)
                lengthComboPass.Items.Add(i);
            for (int i = 16; i <= 128; i++)
                lengthComboPass.Items.Add(i);
            for (int i = 8; i <= 11; i++)
                lengthComboPass.Items.Add(Math.Pow(2,i));
            lengthComboPass.SelectedIndex = 0;

        }
        private void AddCharInPassword(string strV, int lengthCategoty = 1)
        {
            for (int i = 1; i <= lengthCategoty; i++)
            {
                int indexChar = rnd.Next(0, strV.Length);
                thePassword += strV[indexChar];
            }
        }
        private string ShuffleFisherYates(string pass)
        {
            StringBuilder p = new StringBuilder(pass);
            for (int i = 0; i < p.Length - 1; i++)
            {
                int j = rnd.Next(i, p.Length);
                char aux = p[i];
                p[i] = p[j];
                p[j] = aux;
            }
            return p.ToString();
        }
        private void GeneratePassword()
        {
            if (selectedOptions == 0)
            {
                passTextBox.Text = "Hey, I can't generate a password! Check some options!";
                return;
            }
            else
            {
                int selectedLength = int.Parse(lengthComboPass.SelectionBoxItem.ToString());

                thePassword = "";
                int localHowSelectFromEveryCategory = selectedLength / selectedOptions;

                if (checkBoxChar.IsChecked == true)
                {
                    AddCharInPassword(auxChars, localHowSelectFromEveryCategory);
                }
                if (checkBoxNumber.IsChecked == true)
                {
                    AddCharInPassword(auxNumbers, localHowSelectFromEveryCategory);
                }
                if (checkBoxLetterS.IsChecked == true)
                {
                    AddCharInPassword(auxLetterS, localHowSelectFromEveryCategory);
                }
                if (checkBoxLetters.IsChecked == true)
                {
                    AddCharInPassword(auxLetters, localHowSelectFromEveryCategory);
                }

                if (localHowSelectFromEveryCategory * selectedOptions < selectedLength)
                {

                    for (int restIndex = 1; restIndex <= selectedLength - localHowSelectFromEveryCategory * selectedOptions; restIndex++)
                    {
                        int randomSelect = 0;
                        bool isUnChecked = false;
                        while (!isUnChecked)
                        {
                            randomSelect = rnd.Next(1, selectedOptions + 1);
                            switch (randomSelect)
                            {
                                case 1:
                                    if (checkBoxChar.IsChecked == true)
                                    {
                                        AddCharInPassword(auxChars);
                                        isUnChecked = true;
                                    }
                                    break;
                                case 2:
                                    if (checkBoxNumber.IsChecked == true)
                                    {
                                        AddCharInPassword(auxNumbers);
                                        isUnChecked = true;
                                    }
                                    break;
                                case 3:
                                    if (checkBoxLetterS.IsChecked == true)
                                    {
                                        AddCharInPassword(auxLetterS);
                                        isUnChecked = true;
                                    }
                                    break;
                                case 4:
                                    if (checkBoxLetters.IsChecked == true)
                                    {
                                        AddCharInPassword(auxLetters);
                                        isUnChecked = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
                passTextBox.Text = ShuffleFisherYates(thePassword);
            }
        }

        #region Buttons And CheckBoxes
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeneratePassword();
        }

        private void CheckBoxChar_Checked(object sender, RoutedEventArgs e)
        {
            selectedOptions++;
            auxChars = chars;
            if (checkBoxAmbChar.IsChecked == true)
                auxChars = charsBasic;
        }

        private void CheckBoxNumber_Checked(object sender, RoutedEventArgs e)
        {
            selectedOptions++;
            auxNumbers = numbers;
            if (checkBoxSimilar.IsChecked == true)
                SimilarChars();
        }

        private void CheckBoxLetters_Checked(object sender, RoutedEventArgs e)
        {
            selectedOptions++;
            auxLetters = letters;
            if (checkBoxSimilar.IsChecked == true)
                SimilarChars();
        }

        private void CheckBoxLetters_Chk(object sender, RoutedEventArgs e)
        {
            selectedOptions++;
            auxLetterS = letterS;
            if (checkBoxSimilar.IsChecked == true)
                SimilarChars();
        }

        private void CheckBoxChar_UnChecked(object sender, RoutedEventArgs e)
        {
            selectedOptions--;
            auxChars = "";
        }

        private void CheckBoxNumber_UnChecked(object sender, RoutedEventArgs e)
        {
            selectedOptions--;
            auxNumbers = "";
        }

        private void CheckBoxLetters_UnChecked(object sender, RoutedEventArgs e)
        {
            selectedOptions--;
            auxLetters = "";
        }

        private void CheckBoxLetters_UnChk(object sender, RoutedEventArgs e)
        {
            selectedOptions--;
            auxLetterS = "";
        }

        #region similar Char Option
        private void SimilarChars()
        {
            for (int i = 0; i < similiarLetters.Length; i++)
            {
                for (int j = 0; j < auxLetters.Length; j++)
                {
                    if (similiarLetters[i] == auxLetters[j])
                        auxLetters = auxLetters.Remove(j, 1);
                }
            }
            for (int i = 0; i < similiarLetterS.Length; i++)
            {
                for (int j = 0; j < auxLetterS.Length; j++)
                {
                    if (similiarLetterS[i] == auxLetterS[j])
                        auxLetterS = auxLetterS.Remove(j, 1);
                }
            }
            for (int i = 0; i < similiarNumber.Length; i++)
            {
                for (int j = 0; j < auxNumbers.Length; j++)
                {
                    if (similiarNumber[i].ToString() == auxNumbers[j].ToString())
                    {
                        auxNumbers = auxNumbers.Remove(j, 1);
                    }
                }
            }
        }

        private void checkBoxSimilar_Checked(object sender, RoutedEventArgs e)
        {
            SimilarChars();
        }

        private void checkBoxSimilar_Unchecked(object sender, RoutedEventArgs e)
        {
            auxLetters = letters;
            auxLetterS = letterS;
            auxNumbers = numbers;
        }
        #endregion

        #region Ambiguu Char Option
        private void CheckBoxAmbChar_Checked(object sender, RoutedEventArgs e)
        {
            auxChars = charsBasic;
        }
        private void CheckBoxAmbChar_Unchecked(object sender, RoutedEventArgs e)
        {
            auxChars = chars;
        }

        #endregion
        #endregion
    }
}
