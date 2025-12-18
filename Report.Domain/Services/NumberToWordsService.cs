using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Domain.Services;

public static class NumberToWordsService
{
    public static string Convertir(decimal number)
    {
        long integral = (long)Math.Truncate(number);
        int fractional = (int)((number - integral) * 100);

        // Lógica simplificada para el ejemplo
        string letras = integral switch
        {
            31 => "Treinta y Un",
            _ => integral.ToString()
        };

        return $"{letras} {fractional:00}/100 Bolivianos";
    }
}
