using System.ComponentModel.DataAnnotations;

namespace iTechArt.Survey.Domain
{
    public class Pagination
    {
        [Required]
        [Range(1, 100)]
        [Display(Name = "Items count per page")]
        public int ItemCountPerPage { get; set; }

        [Required]
        public int PageIndex { get; set; }

        public Pagination()
        {
            ItemCountPerPage = 5;
            PageIndex = 0;
        }
    }
}
