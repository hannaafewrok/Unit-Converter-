using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hello_World
{
   
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void lengthConverter(object sender, RoutedEventArgs e)
        {
            Mass.Visibility = Visibility.Hidden;
            Length.Visibility = Visibility.Visible;
            
        }

        private void convertL(object sender, RoutedEventArgs e)
        {
            try
            {
                double input = double.Parse(input_value.Text);
                int from = selector1.SelectedIndex;
                int to = selector2.SelectedIndex;

                var answer = convertLength(from, to, input);
                output_value.Content = ToLongString(answer);

            }
            catch (Exception ex)
            {
              
                input_value.Text = "0";
                output_value.Content = "0";
            }
        }

        private double convertLength(int from, int to, double value)
        {

            double fromMilesToMili = 1609344.0;
            double fromMilesToCenti = 160934.4;
            double fromMilesToMeter = 1609.34;
            double fromMilesToKilo = 1.609344;
            double fromMilesToInches = 63360.0;
            double fromMilesToFeets = 5280.0;
            double fromMilesToYards = 1760.0;
            double fromMilesToMiles = 1;

            if (from == -1 || to == -1)
            {
                return value;
            }

            switch (from)
            {
                case 0:
                    value /= fromMilesToMili;
                    break;
                case 1:
                    value /= fromMilesToCenti;
                    break;
                case 2:
                    value /= fromMilesToMeter;
                    break;
                case 3:
                    value /= fromMilesToKilo;
                    break;
                case 4:
                    value /= fromMilesToMiles;
                    break;
                case 5:
                    value /= fromMilesToYards;
                    break;
                case 6:
                    value /= fromMilesToFeets;
                    break;
                case 7:
                    value /= fromMilesToInches;
                    break;

            }

            switch (to)
            {
                case 0:
                    value *= fromMilesToMili;
                    break;
                case 1:
                    value *= fromMilesToCenti;
                    break;
                case 2:
                    value *= fromMilesToMeter;
                    break;
                case 3:
                    value *= fromMilesToKilo;
                    break;
                case 4:
                    value *= fromMilesToMiles;
                    break;
                case 5:
                    value *= fromMilesToYards;
                    break;
                case 6:
                    value *= fromMilesToFeets;
                    break;
                case 7:
                    value *= fromMilesToInches;
                    break;

            }

            return value;


        }

        private void massConverter(object sender, RoutedEventArgs e)
        {
            Length.Visibility = Visibility.Hidden;
            Mass.Visibility = Visibility.Visible;
           
        }

        private void convertM(object sender, RoutedEventArgs e)
        {
            try
            {
                double input = double.Parse(input_value1.Text);
                int from = selector11.SelectedIndex;
                int to = selector22.SelectedIndex;

                var answer = convertMass(from, to, input);
                output_value1.Content = ToLongString(answer);

            }
            catch (Exception ex)
            {
                input_value1.Text = "0";
                output_value1.Content = "0";
            }
        }

        private double convertMass(int from, int to, double value)
        {
            double fromTonToMilig = 907184740;
            double fromTonToCentig = 90718474;
            double fromTonToGram = 907184.7;
            double fromTonToKilog = 907.184;
            double fromTonToOunce = 32000;
            double fromTonToPound = 2000;
            double fromTonToTon = 1;

            if (from == -1 || to == -1)
            {
                return value;
            }

            switch (from)
            {
                case 0:
                    value /= fromTonToMilig;
                    break;
                case 1:
                    value /= fromTonToCentig;
                    break;
                case 2:
                    value /= fromTonToGram;
                    break;
                case 3:
                    value /= fromTonToKilog;
                    break;
                case 4:
                    value /= fromTonToOunce;
                    break;
                case 5:
                    value /= fromTonToPound;
                    break;
                case 6:
                    value /= fromTonToTon;
                    break;

            }

            switch (to)
            {
                case 0:
                    value *= fromTonToMilig;
                    break;
                case 1:
                    value *= fromTonToCentig;
                    break;
                case 2:
                    value *= fromTonToGram;
                    break;
                case 3:
                    value *= fromTonToKilog;
                    break;
                case 4:
                    value *= fromTonToOunce;
                    break;
                case 5:
                    value *= fromTonToPound;
                    break;
                case 6:
                    value *= fromTonToTon;
                    break;

            }

            return value;
        }

        private static string ToLongString(double input)
        {
            string strOrig = input.ToString();
            string str = strOrig.ToUpper();

            // if string representation was collapsed from scientific notation, just return it:
            if (!str.Contains("E")) return strOrig;

            bool negativeNumber = false;

            if (str[0] == '-')
            {
                str = str.Remove(0, 1);
                negativeNumber = true;
            }

            string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            char decSeparator = sep.ToCharArray()[0];

            string[] exponentParts = str.Split('E');
            string[] decimalParts = exponentParts[0].Split(decSeparator);

            // fix missing decimal point:
            if (decimalParts.Length == 1) decimalParts = new string[] { exponentParts[0], "0" };

            int exponentValue = int.Parse(exponentParts[1]);

            string newNumber = decimalParts[0] + decimalParts[1];

            string result;

            if (exponentValue > 0)
            {
                result =
                    newNumber +
                    GetZeros(exponentValue - decimalParts[1].Length);
            }
            else // negative exponent
            {
                result =
                    "0" +
                    decSeparator +
                    GetZeros(exponentValue + decimalParts[0].Length) +
                    newNumber;

                result = result.TrimEnd('0');
            }

            if (negativeNumber)
                result = "-" + result;

            return result;
        }

        private static string GetZeros(int zeroCount)
        {
            if (zeroCount < 0)
                zeroCount = Math.Abs(zeroCount);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < zeroCount; i++) sb.Append("0");

            return sb.ToString();
        }
    }
}
