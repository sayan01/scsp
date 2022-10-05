using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace scsp.Models
{
    public class Photo{
        [Key]
        [Required]
        public int PhotoID {get; set; }
        [Required]
        public byte[] PhotoBLOB {get; set; } = {};
    }
}