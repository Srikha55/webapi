Web API README
Overview

This is a Web API built with C++ Langauge, designed to provide CRUD Operations. The API exposes various endpoints to interact with the application data and services.


API Endpoints
1. POST /api/example
   
Description: Creates new example data.
Request:

     [HttpPost]
         public IActionResult CreateProduct(ProductDto productDto)
                 {
               try
                       {
         using (var connection = new SqlConnection(connectionString))
         {
            connection.Open();
            string sql = "INSERT INTO products " +
                "(name, brand, category, price, description ) VALUES" +
                "(@name ,@brand ,@category , @price, @description)";

            using (var command = new SqlCommand(sql, connection))
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
    
ERROR HANDLING


                }
    }
    catch (Exception ex)
    {
        ModelState.AddModelError("Product", "Sorry, but we have an exception");
        return BadRequest(ModelState);
    }
    return Ok();
}
Response:

![image](https://github.com/user-attachments/assets/0ea6a3f0-5b32-4765-899f-e9ad29e7b925)




2. GET /api/example
Description: Retrieves example data.

Request:

 [HttpGet]
 public IActionResult GetProducts()
 {
     List<Product> products = new List<Product>();

     try
     {
         using (var connection = new SqlConnection(connectionString))
         {
             connection.Open();

             string sql = "SELECT * FROM products";

             using (var command = new SqlCommand(sql, connection))
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


ERROR HANDLING


    catch (Exception ex)
    {
        ModelState.AddModelError("Product", "Sorry, but we have an exception");
        return BadRequest(ModelState);
    }


    return Ok(products);
}



Response:

![image](https://github.com/user-attachments/assets/089329c2-bf01-4eeb-b123-30e2dbeb0072)



3. GET (ID) /api/example
Description: Retrieves example data.

Request:

  [HttpGet("{id}")]
  public IActionResult GetProduct(int id)
  {
      Product product = new Product();

      try
      {
          using (var connection = new SqlConnection(connectionString))
          {
              connection.Open();

              string sql = "SELECT * FROM products WHERE id = @id";

              using (var command = new SqlCommand(sql, connection))
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






Response:

![image](https://github.com/user-attachments/assets/ae001562-41ac-4916-ba81-43f9374e7ebf)


ERROR HANDLING


    catch (Exception ex)
    {
        ModelState.AddModelError("Product", "Sorry, but we have an exception");
        return BadRequest(ModelState);
    }


    return Ok(products);
}



4. PUT (ID) /api/example
Description: insert example data.

Request:

[HttpPut("{id}")]
public IActionResult UpdateProduct(int id, ProductDto productDto)
{
    try
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "UPDATE products SET name = @name, brand=@brand, category = @category, " + "price = @price, description = @description WHERE id = @id";


            using (var command = new SqlCommand(sql, connection))
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



ERROR HANDLING


    catch (Exception ex)
    {
        ModelState.AddModelError("Product", "Sorry, but we have an exception");
        return BadRequest(ModelState);
    }


    return Ok(products);
}


Response:

![image](https://github.com/user-attachments/assets/a48b8062-d42b-422d-867c-1ba1930ca18f)



 5. DELETE (ID) /api/example
Description: Delete example data.

Request:

  [HttpDelete("{id}")]
  public IActionResult DeleteProduct(int id) {

      {
          try
          {
              using (var connection = new SqlConnection(connectionString))
              {
                  connection.Open();

                  string sql = "SELECT * FROM products WHERE id = @id";
                  using (var command = new SqlCommand(sql, connection))
                  {
                      command.Parameters.AddWithValue("id", id);
                      command.ExecuteNonQuery();
                  }
              }
          }



ERROR HANDLING


    catch (Exception ex)
    {
        ModelState.AddModelError("Product", "Sorry, but we have an exception");
        return BadRequest(ModelState);
    }


    return Ok(products);
}


Response:

![image](https://github.com/user-attachments/assets/e7cf000f-ed28-485f-b6db-92cb940c2b89)






PRODUCTDto


namespace WebAPI.Models
{
    public class ProductDto
    {
       
        [Required]
        public String Name { get; set; } = " ";
        [Required]
        public String Brand { get; set; } = " ";
        [Required]
        public String Category { get; set; } = " ";
        [Required]
        public decimal Price { get; set; } 
        [Required]
        public String Description { get; set; } = " ";


    }
}




PRODUCT


    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";      
        public string Brand { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; } 
        public string Description { get; set; } = "";

        public DateTime CreatedAt { get; set; }



    }
}







   
