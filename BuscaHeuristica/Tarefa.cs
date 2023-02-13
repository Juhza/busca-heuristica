namespace BuscaHeuristica
{
    public class Tarefa
    {
        public int TempoDeExecucao { get; private set; }

        public Tarefa()
        {
            TempoDeExecucao = new Random().Next(1, 101);
        }
    }
}
