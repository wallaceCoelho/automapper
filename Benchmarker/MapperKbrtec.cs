using System.Reflection;

namespace Benchmarker;

public class MapperKbrtec
{
    public static TDestino Map<TOrigem, TDestino>(TOrigem origem) where TDestino : new()
    {
        if (origem == null) throw new ArgumentNullException(nameof(origem));

        TDestino destino = new();

        PropertyInfo[] propriedadesOrigem = origem.GetType().GetProperties();
        PropertyInfo[] propriedadesDestino = typeof(TDestino).GetProperties();

        foreach (var propOrigem in propriedadesOrigem)
        {
            var propDestino = Array.Find(propriedadesDestino, p => p.Name == propOrigem.Name && p.PropertyType == propOrigem.PropertyType);

            if (propDestino != null && propDestino.CanWrite)
            {
                object? valor = propOrigem.GetValue(origem);
                propDestino.SetValue(destino, valor);
            }
        }

        return destino;
    }

    public static void Map<TOrigem, TDestino>(TOrigem origem, TDestino destino)
    {
        if (origem == null) throw new ArgumentNullException(nameof(origem));
        if (destino == null) throw new ArgumentNullException(nameof(destino));

        PropertyInfo[] propriedadesOrigem = typeof(TOrigem).GetProperties();
        PropertyInfo[] propriedadesDestino = typeof(TDestino).GetProperties();

        foreach (var propOrigem in propriedadesOrigem)
        {
            var propDestino = Array.Find(propriedadesDestino, p => p.Name == propOrigem.Name && p.PropertyType == propOrigem.PropertyType);

            if (propDestino != null && propDestino.CanWrite)
            {
                object? valor = propOrigem.GetValue(origem);
                if (valor != null) // Apenas atualiza se o valor não for nulo
                {
                    propDestino.SetValue(destino, valor);
                }
            }
        }
    }

    public static List<TDestino> Map<TOrigem, TDestino>(List<TOrigem> origemList) where TDestino : new()
    {
        return origemList.Select(origem => Map<TOrigem, TDestino>(origem!)).ToList();
    }

    public static IEnumerable<IEnumerable<TDestino>> Map<TOrigem, TDestino>(IEnumerable<IEnumerable<TOrigem>> origemLista) where TDestino : new()
    {
        return origemLista.Select(subLista => subLista.Select(origem => Map<TOrigem, TDestino>(origem!)));
    }
}