using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Helpers
{
    public static class DateTimeExtension
    {
        public static int ObterIdade(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            var idade = dataAtual.Year - dateTime.Year;

            if (dataAtual < dateTime.AddYears(idade))
                idade--;

            return idade;
        }
    }
}
