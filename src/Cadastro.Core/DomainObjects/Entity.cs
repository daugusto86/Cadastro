using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cadastro.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        
        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
