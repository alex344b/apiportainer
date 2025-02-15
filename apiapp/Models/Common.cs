using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiapp.Models;
public class Common
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}