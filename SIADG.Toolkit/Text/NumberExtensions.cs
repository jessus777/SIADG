namespace SIADG.Toolkit.Text;

public static class NumberExtensions
{
    public static string ToWords(this int number, string? currencyCode = null, string? cultureName = null)
        => ((decimal) number).ToWords(currencyCode, cultureName);

    public static string ToWords(this long number, string? currencyCode = null, string? cultureName = null)
        => ((decimal) number).ToWords(currencyCode, cultureName);

    public static string ToWords(this float number, string? currencyCode = null, string? cultureName = null)
        => ((decimal) number).ToWords(currencyCode, cultureName);

    public static string ToWords(this double number, string? currencyCode = null, string? cultureName = null)
        => ((decimal) number).ToWords(currencyCode, cultureName);

    public static string ToWords(this decimal number, string? currencyCode = null, string? cultureName = null)
        => (cultureName == "en" ? number.ToEnglishWords(currencyCode) : number.ToSpanishWords(currencyCode)).ToUpper();
    
    public static string ToSpanishWords(this decimal number, string? currencyCode = null)
    {
        // Dividir el número en parte entera y parte decimal
        var integerPart = (long)Math.Floor(number);
        var decimalPart = (int)((number - integerPart) * 100);

        // Convertir la parte entera a palabras
        var integerPartWords = ConvertIntegerToWordsSpanish(integerPart);

        // Formatear la parte decimal según si hay un código de moneda o no
        string decimalPartWords;
        if (currencyCode != null)
        {
            decimalPartWords = $"{decimalPart:00}/100 {currencyCode}";
        }
        else
        {
            decimalPartWords = decimalPart > 0 ? $"punto {ConvertIntegerToWordsSpanish(decimalPart)}" : string.Empty;
        }

        // Unir la parte entera y la parte decimal
        return string.IsNullOrWhiteSpace(decimalPartWords)
            ? integerPartWords
            : $"{integerPartWords} {decimalPartWords}".Trim();
    }

    private static string ConvertIntegerToWordsSpanish(long number)
    {
        switch (number)
        {
            case 0:
                return "cero";
            case < 0:
                return $"menos {ConvertIntegerToWordsSpanish(Math.Abs(number))}";
        }

        var unitsMap = new[] { "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", 
                               "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
        var tensMap = new[] { "cero", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
        var hundredsMap = new[] { "cero", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };

        var words = "";

        if (number / 1000000 > 0)
        {
            words += ConvertIntegerToWordsSpanish(number / 1000000) + " millones ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += ConvertIntegerToWordsSpanish(number / 1000) + " mil ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            if (number == 100) 
                words += "cien ";
            else 
                words += hundredsMap[number / 100] + " ";
            number %= 100;
        }

        if (number > 0)
        {
            if (number < 20)
                words += unitsMap[number];
            else
            {
                if (number is > 20 and < 30)
                {
                    words += "veinti" + unitsMap[number % 10];
                }
                else
                {
                    words += tensMap[number / 10];
                    if (number % 10 > 0)
                        words += " y " + unitsMap[number % 10];
                }
            }
        }

        return words.Trim();
    }
    
    public static string ToEnglishWords(this decimal number, string? currencyCode = null)
    {
        // Dividir el número en parte entera y parte decimal
        var integerPart = (long)Math.Floor(number);
        var decimalPart = (int)((number - integerPart) * 100);

        // Convertir la parte entera a palabras
        var integerPartWords = ConvertIntegerToWordsEnglish(integerPart);

        // Formatear la parte decimal según si hay un código de moneda o no
        string decimalPartWords;
        if (currencyCode != null)
        {
            decimalPartWords = $"{decimalPart:00}/100 {currencyCode}";
        }
        else
        {
            decimalPartWords = decimalPart > 0 ? $"point {ConvertIntegerToWordsEnglish(decimalPart)}" : string.Empty;
        }

        // Unir la parte entera y la parte decimal
        return string.IsNullOrWhiteSpace(decimalPartWords)
            ? integerPartWords
            : $"{integerPartWords} {decimalPartWords}".Trim();
    }

    private static string ConvertIntegerToWordsEnglish(long number)
    {
        if (number == 0) return "zero";
        if (number < 0) return $"minus {ConvertIntegerToWordsEnglish(Math.Abs(number))}";

        var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                               "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        var words = "";

        if (number / 1000000 > 0)
        {
            words += ConvertIntegerToWordsEnglish(number / 1000000) + " million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += ConvertIntegerToWordsEnglish(number / 1000) + " thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += ConvertIntegerToWordsEnglish(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if (number % 10 > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words.Trim();
    }
}