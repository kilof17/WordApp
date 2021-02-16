namespace WordApp.Models
{
    public class DBcontrol
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Control_ID { get; set; }       
        public bool Checked { get; set; }

        public virtual User Users { get; set; }
    }

}
