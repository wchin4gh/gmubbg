using System.ComponentModel.DataAnnotations;

namespace VOAprototype.Models
{
    public class ITFunction
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        public string Unigram { get; set; }
    }
}
