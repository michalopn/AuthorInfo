using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Models
{
    public class BookForUpdateDto
    {
        [Required(ErrorMessage = "You must provide a title value.")]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string Synopsis { get; set; }
    }
}
