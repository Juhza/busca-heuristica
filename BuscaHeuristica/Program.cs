using BuscaHeuristica;
using System.Diagnostics;
using System.Text;

var relatorioFinal = new StringBuilder();
var opcoesDeNumerosDeMaquinas = new[] { 10.0, 20.0, 50.0 };
var expoentesDasTarefas = new[] { 1.5, 2.0 };
var probabilidades = new[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };

relatorioFinal.AppendLine("heuristica, n, m, replicacao, tempo, iteracoes, valor, parametro");

foreach (var numeroDeMaquinas in opcoesDeNumerosDeMaquinas)
{
    Console.WriteLine($"------> NÚMERO DE MÁQUINAS SELECIONADO => {numeroDeMaquinas}");

    foreach (var expoente in expoentesDasTarefas)
    {
        var numeroDeTarefas = Math.Round(Math.Pow(numeroDeMaquinas, expoente));

        Console.WriteLine($"----> QUANTIDADE DE TAREFAS => {numeroDeTarefas}");

        var instancia = new Instancia(
            tipoDeBusca: TipoDeBusca.MelhorMelhora,
            numeroDeMaquinas: (int)numeroDeMaquinas,
            numeroDeTarefas: (int)numeroDeTarefas,
            expoente: (double)expoente
        );

        var linhaDoRelatorio = instancia.ExecutaBuscaLocal();
        relatorioFinal.AppendLine(linhaDoRelatorio);

        Console.WriteLine("\n\n");
    }
}

foreach (var numeroDeMaquinas in opcoesDeNumerosDeMaquinas)
{
    Console.WriteLine($"------> QUANTIDADE DE MÁQUINAS => {numeroDeMaquinas}");

    foreach (var expoente in expoentesDasTarefas)
    {
        var numeroDeTarefas = Math.Round(Math.Pow(numeroDeMaquinas, expoente));
        Console.WriteLine($"----> QUANTIDADE DE TAREFAS => {numeroDeTarefas}");

        foreach (var probabilidade in probabilidades)
        {
            Console.WriteLine($"--> PERCENTUAL => {probabilidade}");

            var instancia = new Instancia(
                numeroDeMaquinas: (int)numeroDeMaquinas,
                numeroDeTarefas: (int)numeroDeTarefas,
                expoente: (double)expoente,
                probabilidade: probabilidade
            );

            var linhaDoRelatorio = instancia.ExecutaBuscaLocal();
            relatorioFinal.AppendLine(linhaDoRelatorio);

            Console.WriteLine("\n\n");
        }
    }
}

var nomeDoArquivo = $"{Guid.NewGuid()}.txt";
var caminho = $"../../../RelatoriosGerados/{nomeDoArquivo}";
await File.WriteAllTextAsync(caminho, relatorioFinal.ToString());

Console.WriteLine($"Arquivo gerado: {nomeDoArquivo}");
Console.WriteLine($"O .txt pode ser transformado em arquivo .xlsx para a análise dos dados.");

var processo = new Process();
processo.StartInfo = new ProcessStartInfo()
{
    UseShellExecute = true,
    WorkingDirectory = "../../../RelatoriosGerados",
    FileName = nomeDoArquivo
};
processo.Start();
processo.WaitForExit();

Console.ReadKey();