namespace WordApp.Models
{
    public class DBcontrol : BaseControl
    {
        public int Id { get; set; }

        public virtual User Users { get; set; }
    }

}
