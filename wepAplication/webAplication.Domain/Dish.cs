namespace wepAplication
{
    public class Dish
    {
        private readonly string _id = Guid.NewGuid().ToString();
        public string Id { get { return _id; } }
        
        public string title { get; set; }
        public string description { get; set; }
        public string imageFilePath;

        public double price { get; set; }//to decimal
    }
}
