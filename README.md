## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)  (https://opensource.org/licenses/MIT)  
# Features

# ![WebApp](https://github.com/berjcode/MongoDbGenericRepositoryPattern-1.0.0/blob/main/mongodb.png)

"Hello, I would like to talk about the features of MongoDbGenericRepositoryPattern, a NuGet package I wrote."

* You can work with asynchronous or non-asynchronous methods.
* You can use it in your sample projects. It will speed you up.
* There are crud operations.
* When you configure appsettings and program.cs, it will run smoothly.
* I am using it in a real microservice project. (own project)
* More methods will be added.


Errors are corrected as a result of feedback.

# MongoDbGenericRepositoryPattern- 1.0.0
 A nuget package I wrote to use the generic repository pattern more efficiently.
# Version
.net 7.0
# Install
```
  dotnet add package MongoDbGenericRepositoryPattern --version 1.0.0
```
# Use 

## Appsettings.json
```
{
  "DatabaseSettings": {
    "ConnectionStrings": "mongodb://localhost:27017",
    "DatabaseName": "ProductDb"
  }
}

```
## Program.cs 
```
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

```
## Create Repository
```
* Interface
    public interface IProductRepository :IRepository<Products>
    {
        void AddProduct(Products product);
    }



  *  Concrete  
public class ProductRepository : MongoDbGenericRepository<Products>, IProductRepository
{
    private readonly MongoDbContext _dbContext;

    public ProductRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
    {
        _dbContext = new MongoDbContext(databaseSettings);
    }

    public void AddProduct(Products product)
    {
        var collection = _dbContext.GetCollection<Products>();
        collection.InsertOne(product);
    }
}


```
## Service

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
       _mapper = mapper;
    }

      public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

        return ResponseDto<List<CategoryDto>>.Success(categoriesDto, 200);
    }


```

```
## Controller 
  
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

....

```

## Packages

* Mongodb.Bson(2.20.0)
* Mongodb.Driver(2.20.0)
 ### Design Patterns:
    * Generic Repository   
       

                                                                                                                      
   ###    By Abdullah Balikci - berjcode

