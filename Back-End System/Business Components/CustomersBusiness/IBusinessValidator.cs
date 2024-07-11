namespace CustomersBusiness;

public interface IBusinessValidator<T>
{
    bool Validate(T tObject);
}
