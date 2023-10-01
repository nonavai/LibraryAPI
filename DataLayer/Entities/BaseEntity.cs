using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}