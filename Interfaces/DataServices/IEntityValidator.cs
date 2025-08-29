namespace HuesarioApp.Interfaces.DataServices;

public interface IEntityValidator<in T> where T : class
{
    public bool IsValid(T entity);
}