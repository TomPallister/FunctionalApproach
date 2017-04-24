namespace FunctionalApproach.Model
{
    public class Value
    {
        public Value(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
