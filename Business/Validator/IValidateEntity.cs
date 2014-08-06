namespace Business.Validator
{
    public interface IValidateEntity<T> where T : class
    {
        bool Validate(T entity);
    }
}