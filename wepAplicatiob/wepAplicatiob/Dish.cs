namespace wepAplicatiob
{
    public class Dish
    {
/*        public Dish(string title, double price)
        {
            this.title = title;
            this.price = price;
        }*/
        public Guid Id { get { return id; } }
        private readonly Guid id = Guid.NewGuid();
        
        public string title { get; set; }
        public string description { get; set; }
        public string imageFilePath;

        public double price { get; set; }
    }
}
