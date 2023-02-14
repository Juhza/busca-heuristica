namespace BuscaHeuristica
{
    public class Instancia
    {
        private Relatorio _relatorio;
        private TipoDeBusca _tipoDeBusca;
        private double? _probabilidade;
        private List<Maquina> _maquinas = new List<Maquina>();

        public Instancia(TipoDeBusca tipoDeBusca, int numeroDeMaquinas, int numeroDeTarefas, double expoente, double? probabilidade = null)
        {
            for (var i = 0; i < numeroDeMaquinas; i++)
                _maquinas.Add(new Maquina(numero: i + 1));

            var tarefas = new List<Tarefa>();
            for (var i = 0; i < numeroDeTarefas; i++)
                tarefas.Add(new Tarefa());

            if (tipoDeBusca == TipoDeBusca.MelhorMelhora)
                tarefas = tarefas.OrderBy(t => t.TempoDeExecucao).ToList();

            _maquinas.First().Tarefas.AddRange(tarefas);
            _tipoDeBusca = tipoDeBusca;
            _probabilidade = probabilidade;
            _relatorio = new Relatorio(numeroDeMaquinas, numeroDeTarefas, expoente, probabilidade);
        }

        public Instancia(int numeroDeMaquinas, int numeroDeTarefas, double expoente, double probabilidade)
            : this(TipoDeBusca.BLMRandomizada, numeroDeMaquinas, numeroDeTarefas, expoente, probabilidade)
        {

        }

        public string ExecutaBuscaLocal()
        {
            switch (_tipoDeBusca)
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

            return _relatorio.FinalizaRelatorio(_tipoDeBusca, MaquinaComMaiorTempoDeExecucao());
        }

        private void BuscaLocalPrimeiraMelhora()
        {
            while (true)
            {
                var maquinaAtual = MaquinaComMaiorTempoDeExecucao();
                var proximaMaquina = _maquinas[maquinaAtual.Index + 1];

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
            while (true)
            {
                var maquinaAtual = MaquinaComMaiorTempoDeExecucao();
                var mudarParaVizinhoAleatorio = Math.Round((double)new Random().Next(1, 9) / 10, 1) <= _probabilidade;
                var proximaMaquina = mudarParaVizinhoAleatorio
                    ? MaquinaAleatoria(maquinaAtual.Index)
                    : MaquinaComMenorTempoDeExecucao();

                var encontrouBoaSolucao = (proximaMaquina.TempoDeExecucaoAtual + maquinaAtual.Tarefas.Last().TempoDeExecucao) >= maquinaAtual.TempoDeExecucaoAtual;
                if (encontrouBoaSolucao) break; // perguntar pro professor o que acontece quando é máquina aleatória e dá a sorte de encontrar justamente uma máquina
                                                // que dará um falso positivo aqui

                var tarefa = maquinaAtual.Tarefas.Last();
                proximaMaquina.AdicionaTarefa(tarefa);
                maquinaAtual.RemoveTarefa(tarefa);

                EscreveResultadoDoLoopAtual();
            }
        }

        private Maquina MaquinaComMenorTempoDeExecucao()
        {
            return _maquinas.OrderBy(m => m.TempoDeExecucaoAtual).ThenBy(m => m.Index).First();
        }

        private Maquina MaquinaComMaiorTempoDeExecucao()
        {
            return _maquinas.OrderByDescending(m => m.TempoDeExecucaoAtual).First();
        }

        private Maquina MaquinaAleatoria(int maquinaAtual)
        {
            var indexRandomizado = 0;
            do
                indexRandomizado = new Random().Next(0, _maquinas.Count - 1);
            while (indexRandomizado == maquinaAtual);

            return _maquinas.ElementAt(indexRandomizado);
        }

        private void EscreveResultadoDoLoopAtual()
        {
            _relatorio.QuantidadeDeIterações += 1;
            Console.WriteLine("[{0}]", string.Join(", ", _maquinas.Select(m => m.TempoDeExecucaoAtual)));
        }
    }
}
