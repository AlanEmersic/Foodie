namespace Foodie.Core.Exceptions;

public sealed class EntityNotFoundException<TEntity> : CustomException
{
    public int Id { get; set; }

    public EntityNotFoundException(int id) : base($"{typeof(TEntity).Name} not found with ID: {id}.")
    {
        Id = id;
    }
}
