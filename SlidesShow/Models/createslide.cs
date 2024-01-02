using System.ComponentModel.DataAnnotations.Schema;

namespace SlidesShow.Models
{
    public class createslide
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public string? ImagePath { get; set; }

        public string? Name { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public long? FileSize { get; set; }

        public int? Duration { get; set; }

        public int? BackgroundColor { get; set; }

        public bool? Footer { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [NotMapped]
        public int createPropId { get; set; }

        [NotMapped]
        public bool? IsImage { get; set; }


    }
}
