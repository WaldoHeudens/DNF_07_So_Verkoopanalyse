namespace Verkoopanalyse;

/// <summary>
/// Analyse-logica over een reeks bestellingen, volledig met LINQ.
/// De methods werken op elke <see cref="IEnumerable{T}"/>, zodat we ze
/// los van Console kunnen testen.
/// </summary>
public static class Analyse
{
    /// <summary>De totale omzet: de som van alle bestelbedragen.</summary>
    public static decimal Totaalomzet(IEnumerable<Bestelling> bestellingen)
        => bestellingen.Sum(b => b.Bedrag);

    /// <summary>
    /// De unieke productnamen binnen een categorie, alfabetisch gesorteerd.
    /// </summary>
    public static IReadOnlyList<string> ProductenInCategorie(
        IEnumerable<Bestelling> bestellingen, string categorie)
        => bestellingen
            .Where(b => b.Categorie == categorie)
            .Select(b => b.Product)
            .Distinct()
            .OrderBy(product => product)
            .ToList();

    /// <summary>De totale omzet per categorie.</summary>
    public static Dictionary<string, decimal> OmzetPerCategorie(
        IEnumerable<Bestelling> bestellingen)
        => bestellingen
            .GroupBy(b => b.Categorie)
            .ToDictionary(groep => groep.Key, groep => groep.Sum(b => b.Bedrag));

    /// <summary>
    /// De klant met de hoogste totale omzet.
    /// Gooit <see cref="InvalidOperationException"/> als er geen bestellingen zijn.
    /// </summary>
    public static string BesteKlant(IEnumerable<Bestelling> bestellingen)
        => bestellingen
            .GroupBy(b => b.Klant)
            .OrderByDescending(groep => groep.Sum(b => b.Bedrag))
            .ThenBy(groep => groep.Key)
            .First()
            .Key;

    /// <summary>Is er minstens een bestelling met een bedrag boven de drempel?</summary>
    public static bool HeeftBestellingBoven(
        IEnumerable<Bestelling> bestellingen, decimal drempel)
        => bestellingen.Any(b => b.Bedrag > drempel);
}
