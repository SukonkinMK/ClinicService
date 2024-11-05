namespace ClinicService.Services
{
    public interface IRepository<T, Tid>
    {
        int Create(T entity);
        int Update(T entity);
        int Delete(Tid id);
        T GetById(Tid id);
        List<T> GetAll();
    }
}
