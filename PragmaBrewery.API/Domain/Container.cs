namespace PragmaBrewery.API.Domain
{
    public class Container
    {
        public Container()
        {
        }

        public Container(string name, long idBeer)
        {
            Name = name;
            IdBeer = idBeer;
        }

        public Container(long id, string name, long idBeer)
        {
            Id = id;
            Name = name;
            IdBeer = idBeer;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long IdBeer { get; set; }

        public Beer Beer { get; set; }
    }
}
