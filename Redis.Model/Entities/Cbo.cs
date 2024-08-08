namespace Redis.Model.Entities
{
    public class Cbo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
