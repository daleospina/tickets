namespace cicloso.tickets.portal.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// 
    /// </summary>
    public class TheaterModel
    {
        public TheaterModel()
        {
            
        }

        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Cities")]
        public string IdCity { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "State")]
        public bool State { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

    }
}