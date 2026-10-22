using Verkoopanalyse;

// Een kleine dataset. In een echt programma komt die uit een bestand of database.
List<Bestelling> bestellingen =
[
    new("Ana", "Toetsenbord", "Randapparatuur", 2, 45.00m),
    new("Ana", "Muis", "Randapparatuur", 1, 25.00m),
    new("Bram", "Laptop", "Computers", 1, 999.00m),
    new("Bram", "Muis", "Randapparatuur", 3, 25.00m),
    new("Cem", "Laptop", "Computers", 2, 999.00m),
    new("Cem", "Monitor", "Randapparatuur", 1, 189.00m),
];

Console.WriteLine($"Totaalomzet: {Analyse.Totaalomzet(bestellingen):C}");
Console.WriteLine($"Beste klant: {Analyse.BesteKlant(bestellingen)}");

Console.WriteLine("\nOmzet per categorie:");
foreach ((string categorie, decimal omzet) in Analyse.OmzetPerCategorie(bestellingen))
{
    Console.WriteLine($"  {categorie}: {omzet:C}");
}

Console.WriteLine("\nRandapparatuur-producten:");
foreach (string product in Analyse.ProductenInCategorie(bestellingen, "Randapparatuur"))
{
    Console.WriteLine($"  {product}");
}
