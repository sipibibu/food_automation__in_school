namespace wepAplication
{
    public class Dish
    {
        public Guid Id { get { return _id; } }
        private readonly Guid _id = Guid.NewGuid();
        
        public string title { get; set; }
        public string description { get; set; }
        public string imageFilePath;

        public double price { get; set; }//to decimal
    }
}
