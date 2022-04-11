namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int numero;
        public string NomeDaClasse
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public abstract string Validar();
    }
}
