namespace BuscaHeuristica
{
    public class Instancia
    {
        public TipoDeBusca TipoDeBusca { get; private set; }
        public List<Maquina> Maquinas { get; private set; } = new List<Maquina>();

        public Instancia(TipoDeBusca tipoDeBusca)
        {
            var quantidadeDeMaquinas = EscolheUmAleatorio(entrada: new[] { 10.0, 20.0, 50.0 });
            var potencia = EscolheUmAleatorio(entrada: new[] { 1.5, 2.0 });
            var quantidadeDeTarefas = Math.Round(Math.Pow(quantidadeDeMaquinas, potencia));

            for (var i = 0; i < quantidadeDeMaquinas; i++)
                Maquinas.Add(new Maquina(numero: i + 1));

            var tarefas = new List<Tarefa>();
            for (var i = 0; i < quantidadeDeTarefas; i++)
                tarefas.Add(new Tarefa());

            if (tipoDeBusca == TipoDeBusca.MelhorMelhora)
                tarefas = tarefas.OrderBy(t => t.TempoDeExecucao).ToList();

            Maquinas.First().Tarefas.AddRange(tarefas);
            TipoDeBusca = tipoDeBusca;
        }


        public void ExecutaBuscaLocal()
        {
            if (TipoDeBusca == TipoDeBusca.PrimeiraMelhora)
                BuscaLocalPrimeiraMelhora();
            else
                BuscaLocalMelhorMelhora();
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

        private double EscolheUmAleatorio(double[] entrada)
        {
            var indexAleatorio = new Random().Next(0, entrada.Length - 1);
            return entrada[indexAleatorio];
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
            Console.WriteLine("[{0}]", string.Join(", ", Maquinas.Select(m => m.TempoDeExecucaoAtual)));
        }
    }
}
