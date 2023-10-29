


//my generic repo and service exam


//IProductService productService = new ProductService(new efProductRepository(new MyDbContext()));
//IProductService productService = new ProductService(new efProductRepository(new SqlContext()));
//IProductService productService = new ProductService(new AdoProductRepository(new MyDbContext()));
IProductService productService = new ProductService(new AdoProductRepository(new SqlContext()));
productService.ControlAndAdd(new Product());
productService.CustomControl();





public class DbContext
{

}
public class SqlContext : IContext
{


    public void Add() => Console.WriteLine("SQLContext Kullanılıyor");

}

public class MyDbContext : DbContext, IContext
{


    public void Add() => Console.WriteLine("MyDbContext kullanılıyor");

}

public interface IContext
{
    void Add();
}

public interface IGenericRepository<T> where T : class
{
    void Add(T entity);
}
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    IContext context;

    public GenericRepository(IContext context)
    {
        this.context = context;
    }

    public void Add(T entity)
    {
        context.Add();
    }
}

public interface IGenericService<T> where T : class
{
    void ControlAndAdd(T entity);
}
public class GenericService<T> : IGenericService<T> where T : class
{
    public IGenericRepository<T> repository;
    public GenericService(IGenericRepository<T> repository)
    {

        this.repository = repository;
    }

    public void ControlAndAdd(T entity) => repository.Add(entity);

}

public class efProductRepository : GenericRepository<Product>, IProductRepository
{

    public efProductRepository(IContext context) : base(context)
    {
    }

    public void CustomMethod() => Console.WriteLine("Custom product metot");

}
public interface IProductRepository : IGenericRepository<Product>
{
    void CustomMethod();
}

public interface IProductService : IGenericService<Product>
{
    void CustomControl();
}

public class ProductService : GenericService<Product>, IProductService
{
    public IProductRepository repository;

    public ProductService(IProductRepository repository) : base(repository)
    {
        this.repository = repository;
    }

    public void CustomControl()
    {
        repository.CustomMethod();

    }

}
public class AdoProductRepository : GenericRepository<Product>, IProductRepository
{
    public AdoProductRepository(IContext context) : base(context)
    {
    }

    public void CustomMethod() => Console.WriteLine("Custom Ado Product repository");

}
public class Product
{

}

