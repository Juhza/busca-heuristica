using BuscaHeuristica;

for (var i = 0; i < 10; i++)
{
    Console.WriteLine($"---> Instancia {i + 1}:");

    var instancia = new Instancia(tipoDeBusca: TipoDeBusca.MelhorMelhora);
    instancia.ExecutaBuscaLocal();

    Console.WriteLine("\n\n");
}

Console.ReadKey();