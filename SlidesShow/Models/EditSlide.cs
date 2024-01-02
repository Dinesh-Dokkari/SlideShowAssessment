using System.ComponentModel.DataAnnotations;

namespace SlidesShow.Models
{
    public class EditSlide
    {
        public int Id { get; set; }

        public IFormFile? Image { get; set; }



        public int PropertyId { get; set; }

        public string? ImagePath { get; set; }



        [Required]
        public string? Name { get; set; }


        [Required]

        public int? Duration { get; set; }

        [Required]

        public int? BackgroundColor { get; set; }

        [Required]

        public bool? Footer { get; set; }

        public bool? IsImage { get; set; }

    }
}
