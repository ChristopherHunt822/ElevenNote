using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryDetail
    {
        public int CategoryID { get; set; }
        [Display(Name = "Name of Category")]
        public string Name { get; set; }

        public List<NoteListItem> Notes { get; set; } = new List<NoteListItem>();
    }
}
