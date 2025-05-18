using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

public class NumericValidation
{
    private static readonly Regex _regex = new Regex(@"^(0(\.\d*)?|[1-9]\d*(\.\d*)?)$");
    private static readonly Regex _regexWithNegative = new Regex(@"^-?(0(\.\d*)?|[1-9]\d*(\.\d*)?)$");

    public static void OnlyNumbers(object sender, TextCompositionEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text); // Simulate user input
            e.Handled = !_regex.IsMatch(newText); // Validate
        }
    }

    public static void OnlyNumbersWithNegative(object sender, TextCompositionEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text); // Simulate user input

            // Special case: Allow single '-' at the start
            if (newText == "-")
            {
                e.Handled = false; // Allow input
                return;
            }

            e.Handled = !_regexWithNegative.IsMatch(newText); // Validate with negative support
        }
    }

    public static void PreventSpace(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }
}
