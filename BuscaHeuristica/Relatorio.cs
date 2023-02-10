namespace BuscaHeuristica
{
    public class Relatorio
    {
        public int NumeroDeMaquinas { get; private set; }
        public int NumeroDeTarefas { get; private set; }
        public double? Percentual { get; private set; }
        public int QuantidadeDeIterações { get; set; }
        public DateTime InicioDaExecucao { get; private set; }

        public Relatorio(int numeroDeMaquinas, int numeroDeTarefas, double? percentual)
        {
            NumeroDeMaquinas = numeroDeMaquinas;
            NumeroDeTarefas = numeroDeTarefas;
            Percentual = percentual;
            InicioDaExecucao = DateTime.Now;
        }

        public string FinalizaRelatorio()
        {
            var tempoDeExecucao = (DateTime.Now - InicioDaExecucao).TotalSeconds;
            return $"{NumeroDeMaquinas};{NumeroDeTarefas};{Percentual};{QuantidadeDeIterações};{tempoDeExecucao.ToString("0.##")}";
        }
    }
}
