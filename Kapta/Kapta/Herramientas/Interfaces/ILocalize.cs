namespace Kapta.Herramientas.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public interface ILocalize
    {
        //para saber en que idioma está el teléfono
        CultureInfo GetCurrentCultureInfo();

        //cambiar configuración al telefono
        void SetLocale(CultureInfo ci);
    }
}
