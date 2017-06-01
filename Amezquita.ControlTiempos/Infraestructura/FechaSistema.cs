using System;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public static class FechaSistema
    {
        public static Func<DateTime> Actual = () => DateTime.Now;

        public static Func<DateTime> UtcActual = () => DateTime.UtcNow;

        public static void Reestablecer()
        {
            Actual = () => DateTime.Now;
            UtcActual = () => DateTime.UtcNow;
        }

        public static DateTime ZonaHoraria(this DateTime fecha, string zonaId = "SA Pacific Standard Time")
        {
            var zona = TimeZoneInfo.FindSystemTimeZoneById(zonaId);
            return fecha.ZonaHoraria(zona);
        }

        private static DateTime ZonaHoraria(this DateTime fecha, TimeZoneInfo zona)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(fecha, zona);
        }
    }
}