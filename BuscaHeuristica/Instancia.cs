namespace BuscaHeuristica
{
    public class Instancia
    {
        private Relatorio _relatorio;

        public TipoDeBusca TipoDeBusca { get; private set; }
        public List<Maquina> Maquinas { get; private set; } = new List<Maquina>();

        public Instancia(TipoDeBusca tipoDeBusca, int numeroDeMaquinas, int numeroDeTarefas, double? percentual = null)
        {
            for (var i = 0; i < numeroDeMaquinas; i++)
                Maquinas.Add(new Maquina(numero: i + 1));

            var tarefas = new List<Tarefa>();
            for (var i = 0; i < numeroDeTarefas; i++)
                tarefas.Add(new Tarefa());

            if (tipoDeBusca == TipoDeBusca.MelhorMelhora)
                tarefas = tarefas.OrderBy(t => t.TempoDeExecucao).ToList();

            Maquinas.First().Tarefas.AddRange(tarefas);
            TipoDeBusca = tipoDeBusca;
            _relatorio = new Relatorio(numeroDeMaquinas, numeroDeTarefas, percentual);
        }

        public Instancia(int numeroDeMaquinas, int numeroDeTarefas, double percentual)
            : this(TipoDeBusca.BLMRandomizada, numeroDeMaquinas, numeroDeTarefas, percentual)
        {
        
        }

        public string ExecutaBuscaLocal()
        {
            switch (TipoDeBusca)
            {
                case TipoDeBusca.MelhorMelhora:
                    BuscaLocalMelhorMelhora();
                    break;
                case TipoDeBusca.PrimeiraMelhora:
                    BuscaLocalPrimeiraMelhora();
                    break;
                case TipoDeBusca.BLMRandomizada:
                    BuscaLocalMonotonaRandomizada();
                    break;
            }

            return _relatorio.FinalizarRelatorio();
        }

        private void BuscaLocalPrimeiraMelhora()
        {
            while (true)
            {
                var maquinaAtual = MaquinaComMaiorTempoDeExecucao();
                var proximaMaquina = Maquinas[maquinaAtual.Index + 1];

                var encontrouPrimeiraMelhora = (maquinaAtual.Tarefas.Last().TempoDeExecucao + proximaMaquina.TempoDeExecucaoAtual) >= maquinaAtual.TempoDeExecucaoAtual;
                if (encontrouPrimeiraMelhora) break;

                if (proximaMaquina.TempoDeExecucaoAtual < maquinaAtual.TempoDeExecucaoAtual)
                {
                    var tarefa = maquinaAtual.Tarefas.Last();
                    proximaMaquina.AdicionaTarefa(tarefa);
                    maquinaAtual.RemoveTarefa(tarefa);
                }

                EscreveResultadoDoLoopAtual();
            }
        }

        private void BuscaLocalMelhorMelhora()
        {
            while (true)
            {
                var maquinaAtual = MaquinaComMaiorTempoDeExecucao();
                var proximaMaquina = MaquinaComMenorTempoDeExecucao();

                var encontrouMelhorMelhora = (proximaMaquina.TempoDeExecucaoAtual + maquinaAtual.Tarefas.Last().TempoDeExecucao) >= maquinaAtual.TempoDeExecucaoAtual;
                if (encontrouMelhorMelhora) break;

                var tarefa = maquinaAtual.Tarefas.Last();
                proximaMaquina.AdicionaTarefa(tarefa);
                maquinaAtual.RemoveTarefa(tarefa);

                EscreveResultadoDoLoopAtual();
            }
        }

        private void BuscaLocalMonotonaRandomizada()
        {

        }

        private Maquina MaquinaComMenorTempoDeExecucao()
        {
            return Maquinas.OrderBy(m => m.TempoDeExecucaoAtual).ThenBy(m => m.Index).First();
        }

        private Maquina MaquinaComMaiorTempoDeExecucao()
        {
            return Maquinas.OrderByDescending(m => m.TempoDeExecucaoAtual).First();
        }

        private void EscreveResultadoDoLoopAtual()
        {
            _relatorio.QuantidadeDeIterações += 1;
            Console.WriteLine("[{0}]", string.Join(", ", Maquinas.Select(m => m.TempoDeExecucaoAtual)));
        }
    }
}
