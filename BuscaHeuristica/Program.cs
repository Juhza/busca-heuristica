using BuscaHeuristica;

var relatorioFinal = new StringBuilder();
var opcoesDeNumerosDeMaquinas = new[] { 10.0, 20.0, 50.0 };
var expoentesDasTarefas = new[] { 1.5, 2.0 };

// Run Busca Local Melhor Melhora
foreach (var numeroDeMaquinas in opcoesDeNumerosDeMaquinas)
{
    Console.WriteLine($"------> NÚMERO DE MÁQUINAS SELECIONADO => {numeroDeMaquinas}");

    foreach (var expoente in expoentesDasTarefas)
    {
        var numeroDeTarefas = Math.Round(Math.Pow(numeroDeMaquinas, expoente));

        Console.WriteLine($"----> QUANTIDADE DE TAREFAS => {numeroDeTarefas}");

        var instancia = new Instancia(
            tipoDeBusca: TipoDeBusca.MelhorMelhora, 
            numeroDeMaquinas: numeroDeMaquinas, 
            numeroDeTarefas: numeroDeTarefas
        );

        var linhaDoRelatorio = instancia.ExecutaBuscaLocal();
        relatorioFinal.AppendLine(linhaDoRelatorio); // verificar

        Console.WriteLine("\n\n");
    }
}

var percentuais = new[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };

// Run Busca Monótona Randomizada
foreach (var numeroDeMaquinas in opcoesDeNumerosDeMaquinas)
{
    Console.WriteLine($"------> QUANTIDADE DE MÁQUINAS => {numeroDeMaquinas}");

    foreach (var expoente in expoentesDasTarefas)
    {
        var numeroDeTarefas = Math.Round(Math.Pow(numeroDeMaquinas, expoente));
        Console.WriteLine($"----> QUANTIDADE DE TAREFAS => {numeroDeTarefas}");

        foreach (var percentual in percentuais)
        {
            Console.WriteLine($"--> PERCENTUAL => {percentual}");

            var instancia = new Instancia(
                numeroDeMaquinas: numeroDeMaquinas, 
                numeroDeTarefas: numeroDeTarefas, 
                percentual: percentual
            );

            var linhaDoRelatorio = instancia.ExecutaBuscaLocal();
            relatorioFinal.AppendLine(linhaDoRelatorio); // verificar

            Console.WriteLine("\n\n");
        }
    }
}

Console.ReadKey();