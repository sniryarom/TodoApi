using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Key { get; set; }
        public string Text { get; set; }
        public bool IsComplete { get; set; }
        public string User { get; set; }
        public string CreateDate { get; set; }
        public bool IsPrivate { get; set; }
    }
}