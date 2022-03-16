namespace CarRenting.Common
{
    internal interface IRepository

    {
        void Add<T>(T entity) where T : class;

        IQueryable<T> All<T>() where T : class;

        int SaveChanges();
    }
}