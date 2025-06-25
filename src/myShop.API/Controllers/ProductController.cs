namespace myShop.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Produces("application/json")]
public class ProductController(IProductFacadeServices Services) : ControllerBase
{
    /// <summary>
    /// Retreives a Product List
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     GET /api/product 
    /// </remarks>
    /// <response code="200">Returns the full List</response>
    /// 
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductPagination>> GetProducts([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        IEnumerable<Product> productList = await Services.Queries.GetProductsAsync();
        IEnumerable<ProductDto> productListDto = new ProductDto().ToListDto(productList);
        return Ok(new ProductPagination(pageIndex, pageIndex, productListDto.Count(), productListDto));
    }

    /// <summary>
    /// Retreives a Product Filtered by its Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     GET /api/product/1 
    /// </remarks>
    /// <response code="200">Returns the filtered Product</response>
    /// <response code="404">Returns Not Found</response>
    [HttpGet]
    [Route("{id:int}")]
    [ActionName(nameof(GetProductById))]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        Product product = await Services.Queries.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        ProductDto productDto = new ProductDto().ToDto(product);
        return Ok(productDto);
    }

    /// <summary>
    /// Retreives a Product Filtered by its Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     GET /api/product/t-shirt 
    /// </remarks>
    /// <response code="200">Returns the filtered List of Products</response>
    /// <response code="404">Returns not Found</response>
    [HttpGet]
    [Route("{name:alpha}")]
    [ActionName(nameof(GetProductsByName))]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductDto>> GetProductsByName(string name)
    {
        IEnumerable<Product> productList = await Services.Queries.GetProductsByNameAsync(name);
        if (productList == null)
        { 
            return NotFound();
        }
        IEnumerable<ProductDto> productListDto = new ProductDto().ToListDto(productList);
        return Ok(productListDto);
    }

    /// <summary>
    /// Creates a new Product
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     POST /api/product
    ///     {
    ///         "name" : "T-Shirt",
    ///         "description" : "Oversized",
    ///         "price" : 1,
    ///         "productImageUrl" : "/images/tshirt.jpg",
    ///         "brandId": 1   
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created Product</response>
    /// <response code="400">If the Product is null</response>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> AddProduct(ProductDto productDto)
    {
        Product product = productDto.ToEntity();
        await Services.Repository.AddAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Updates an existing Product
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     PUT /api/product
    ///     {
    ///         "id": 11
    ///         "name" : "Cap",
    ///         "description" : "Black",
    ///         "price" : 20,
    ///         "productImageUrl" : "/images/cap.jpg",
    ///         "brandId": 3  
    ///     }
    /// </remarks>
    /// <response code="204">No Content if Updated</response>
    /// <response code="400">If the Product was not Found</response>

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateProduct(ProductDto productDto)
    {
        Product product = await Services.Queries.GetProductByIdAsync(productDto.Id);
        if (product == null)
        {
            return NotFound();
        }

        product.UpdateProductDetails(productDto.Name!, productDto.Description!, productDto.Price, productDto.ProductImageUrl);
        product.UpdateProductBrandId(productDto.BrandId!);

        await Services.Repository.UpdateAsync(product);
        return NoContent();
    }

    /// <summary>
    /// Updates an existing Product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample Request
    /// 
    ///     DELETE /api/product/id 
    /// </remarks>
    /// <response code="204">No Content if DEleted</response>
    /// <response code="400">If the Product was not Found</response>
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product product = await Services.Queries.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await Services.Repository.DeleteAsync(product);
        return NoContent();
    }
}