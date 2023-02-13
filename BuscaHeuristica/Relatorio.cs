namespace BuscaHeuristica
{
    public class Relatorio
    {
        public int NumeroDeMaquinas { get; private set; }
        public int NumeroDeTarefas { get; private set; }
        public int Expoente { get; private set; }
        public double? Percentual { get; private set; }
        public int QuantidadeDeIterações { get; set; }
        public DateTime InicioDaExecucao { get; private set; }

        public Relatorio(int numeroDeMaquinas, int numeroDeTarefas, int expoente, double? percentual)
        {
            NumeroDeMaquinas = numeroDeMaquinas;
            NumeroDeTarefas = numeroDeTarefas;
            Expoente = expoente;
            Percentual = percentual;
            InicioDaExecucao = DateTime.Now;
        }

        public string FinalizaRelatorio(TipoDeBusca tipoDeBusca, Maquina maquinaComMaiorTempoDeExecucao)
        {
            var tempoDeExecucao = (DateTime.Now - InicioDaExecucao).TotalSeconds;

            // heuristica, n, m, replicacao, tempo, iteracoes, valor, parametro
            return $"{tipoDeBusca.ToString()}; {NumeroDeTarefas}; {NumeroDeMaquinas}; {Expoente}; {tempoDeExecucao.ToString("0.#####")}; {QuantidadeDeIterações}; {maquinaComMaiorTempoDeExecucao.TempoDeExecucaoAtual}; {Percentual};";
        }
    }
}
