namespace myShop.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductController(ProductFacadeServices Services) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductPagination>> GetProducts([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        IEnumerable<Product> productList = await Services.Queries.GetProductsAsync();
        IEnumerable<ProductDto> productListDto = new ProductDto().ToListDto(productList);
        return Ok(new ProductPagination(pageIndex, pageIndex, productListDto.Count(), productListDto));
    }

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

    [HttpGet]
    [Route("{name:alpha}")]
    [ActionName(nameof(GetProductsByName))]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductDto>> GetProductsByName(string name)
    {
        IEnumerable<Product> productList = await Services.Queries.GetProductsByNameAsync(name);
        IEnumerable<ProductDto> productListDto = new ProductDto().ToListDto(productList);
        return Ok(productListDto);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> AddProduct(ProductDto productDto)
    {
        Product product = productDto.ToEntity();
        await Services.Repository.AddAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

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
        product.UpdateProductBrand(productDto.BrandId!);

        await Services.Repository.UpdateAsync(product);
        return NoContent();
    }

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