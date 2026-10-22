namespace Verkoopanalyse;

/// <summary>
/// Een verkochte regel: een klant kocht een aantal stuks van een product
/// uit een categorie, tegen een bepaalde stukprijs.
/// </summary>
public record Bestelling(
    string Klant,
    string Product,
    string Categorie,
    int Aantal,
    decimal StukPrijs)
{
    /// <summary>Het bedrag van deze bestelregel (aantal keer stukprijs).</summary>
    public decimal Bedrag => Aantal * StukPrijs;
}
