namespace SlidesShow.Models
{
    public class Addnew
    {
        public IEnumerable<createslide> AllSelected { get; set; }

        public int clubId { get; set; }

        public createslide? NewAdd { get; set; }
    }
}
