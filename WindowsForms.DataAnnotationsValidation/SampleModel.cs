using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms.DataAnnotationsValidation
{
    public class SampleModel : BaseModel
    {
        [Required]
        [Range(1, 100)]
        public int? Id { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("w+")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Price { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}
