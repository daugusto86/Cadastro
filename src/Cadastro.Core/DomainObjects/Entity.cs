using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cadastro.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// Está como virtual para ser reescrita nos testes
        /// utilizando Mock
        /// </summary>
        [NotMapped]
        public virtual ValidationResult ValidationResult { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
