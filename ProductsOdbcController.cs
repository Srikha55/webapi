using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Odbc;
using System.Diagnostics.Eventing.Reader;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsOdbcController : ControllerBase
    {
        private readonly string connectionString;
        public ProductsOdbcController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings : OdbcSqlServerDb"] ?? "";
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO products " +
                        "(name, brand, category, price, description ) VALUES" +
                        "(? ,? ,? , ?, ?)";

                    using (var command = new OdbcCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@brand", productDto.Brand);
                        command.Parameters.AddWithValue("@category", productDto.Category);
                        command.Parameters.AddWithValue("@price", productDto.Price);
                        command.Parameters.AddWithValue("@description", productDto.Description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Product", "Sorry, but we have an exception");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM products";

                    using (var command = new OdbcCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Product product = new Product();

                                product.Id = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Brand = reader.GetString(2);
                                product.Category = reader.GetString(3);
                                product.Price = reader.GetDecimal(4);
                                product.Description = reader.GetString(5);
                                product.CreatedAt = reader.GetDateTime(6);

                            }
                            else
                            {
                                return NotFound();
                                ; }
                        }
                    }
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("Product", "Sorry, but we have an exception");
                return BadRequest(ModelState);
            }


            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            Product product = new Product();

            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM products WHERE id = ?";

                    using (var command = new OdbcCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product.Id = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Brand = reader.GetString(2);
                                product.Category = reader.GetString(3);
                                product.Price = reader.GetDecimal(4);
                                product.Description = reader.GetString(5);
                                product.CreatedAt = reader.GetDateTime(6);

                            }
                            else
                            {
                                return NotFound();

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "Sorry, but we have an exception");
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE products SET name = ?, brand=?, category = ?, " + "price = ?, description = ? WHERE id = ?";


                    using (var command = new OdbcCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@brand", productDto.Brand);
                        command.Parameters.AddWithValue("@category", productDto.Category);
                        command.Parameters.AddWithValue("@price", productDto.Price);
                        command.Parameters.AddWithValue("@description", productDto.Description);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "Sorry, but we have an exception");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) {

            {
                try
                {
                    using (var connection = new OdbcConnection(connectionString))
                    {
                        connection.Open();

                        string sql = "SELECT * FROM products WHERE id = ?";
                        using (var command = new OdbcCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Product", "Sorry, but we have an exception");
                    return BadRequest(ModelState);
                }
                return Ok();

            }
        }
    } 
}
