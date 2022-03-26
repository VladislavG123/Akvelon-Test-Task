using System.ComponentModel.DataAnnotations;

namespace Akvelon.TestTask.DAL.Models;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}