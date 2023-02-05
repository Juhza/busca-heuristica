namespace BuscaHeuristica
{
    public class Maquina
    {
        public int Index { get; private set; }
        public string Identificador { get; private set; }
        public List<Tarefa> Tarefas { get; private set; }
        public int TempoDeExecucaoAtual => Tarefas.Sum(t => t.TempoDeExecucao);

        public Maquina(int numero)
        {
            Index = numero - 1;
            Identificador = $"Máquina {numero}";
            Tarefas = new List<Tarefa>();
        }

        public void AdicionaTarefa(Tarefa tarefa)
        {
            Tarefas.Add(tarefa);
        }

        public void RemoveTarefa(Tarefa tarefa)
        {
            Tarefas.Remove(tarefa);
        }
    }
}
