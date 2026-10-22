using Verkoopanalyse;

namespace Verkoopanalyse.Tests;

public class AnalyseTests
{
    // Vaste testdataset die door alle tests gedeeld wordt.
    private static readonly List<Bestelling> Bestellingen =
    [
        new("Ana", "Toetsenbord", "Randapparatuur", 2, 45.00m),   // 90
        new("Ana", "Muis", "Randapparatuur", 1, 25.00m),          // 25
        new("Bram", "Laptop", "Computers", 1, 999.00m),           // 999
        new("Bram", "Muis", "Randapparatuur", 3, 25.00m),         // 75
        new("Cem", "Laptop", "Computers", 2, 999.00m),            // 1998
        new("Cem", "Monitor", "Randapparatuur", 1, 189.00m),      // 189
    ];

    [Fact]
    public void Totaalomzet_TeltAlleBedragenOp()
    {
        // 90 + 25 + 999 + 75 + 1998 + 189
        Assert.Equal(3376.00m, Analyse.Totaalomzet(Bestellingen));
    }

    [Fact]
    public void Totaalomzet_LegeReeks_IsNul()
    {
        Assert.Equal(0m, Analyse.Totaalomzet([]));
    }

    [Fact]
    public void ProductenInCategorie_GeeftUniekeNamenGesorteerd()
    {
        IReadOnlyList<string> producten =
            Analyse.ProductenInCategorie(Bestellingen, "Randapparatuur");

        // Muis komt tweemaal voor in de data, maar mag maar een keer terugkomen.
        Assert.Equal(["Monitor", "Muis", "Toetsenbord"], producten);
    }

    [Fact]
    public void OmzetPerCategorie_SomtPerGroep()
    {
        Dictionary<string, decimal> omzet = Analyse.OmzetPerCategorie(Bestellingen);

        Assert.Equal(2997.00m, omzet["Computers"]);      // 999 + 1998
        Assert.Equal(379.00m, omzet["Randapparatuur"]);  // 90 + 25 + 75 + 189
    }

    [Fact]
    public void BesteKlant_IsDieMetHoogsteOmzet()
    {
        // Cem: 1998 + 189 = 2187, hoger dan Bram (1074) en Ana (115).
        Assert.Equal("Cem", Analyse.BesteKlant(Bestellingen));
    }

    [Fact]
    public void BesteKlant_LegeReeks_GooitException()
    {
        Assert.Throws<InvalidOperationException>(() => Analyse.BesteKlant([]));
    }

    [Theory]
    [InlineData(1000, true)]   // de regel van 1998 ligt hierboven
    [InlineData(2000, false)]  // geen enkele regel ligt boven 2000
    public void HeeftBestellingBoven_KijktNaarRegelbedrag(decimal drempel, bool verwacht)
    {
        Assert.Equal(verwacht, Analyse.HeeftBestellingBoven(Bestellingen, drempel));
    }
}
