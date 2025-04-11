using Microsoft.EntityFrameworkCore;
using Novin.Libary.Backend.API.DB;
using Novin.Libary.Backend.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configure services for minimal API documentation (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EF Core DbContext with SQL Server using the connection string from configuration
builder.Services.AddDbContext<FirstDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB"));
});

// Configure CORS to allow any origin, method, and header (not secure for production!)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

// Enable authentication and authorization services
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Configure Identity system with relaxed password rules for simplicity
builder.Services.AddIdentityApiEndpoints<LibraryUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<FirstDB>();

var app = builder.Build();

// Enable Swagger UI only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapIdentityApi<LibraryUser>();

#region Books
// Endpoint to add a new book if title is valid
app.MapPost("/books/add", async (FirstDB db, Book book) =>
{
    if (book != null && !string.IsNullOrWhiteSpace(book.Title) && book.Title.Length > 2)
    {
        await db.Books.AddAsync(book);
        await db.SaveChangesAsync();
        return Results.Ok(book);
    }
    return Results.BadRequest(new
    {
        Message = "Invalid book data.",
        Errors = new[] { "Title must be at least 3 characters long." }
    });
});

// List all books
app.MapGet("/books/list", async (FirstDB db) =>
{
    var books = await db.Books.ToListAsync();
    return Results.Ok(books);
});

// Update book if valid
app.MapPost("/books/edit", async (FirstDB db, Book book) =>
{
    if (book != null && !string.IsNullOrWhiteSpace(book.Title) && book.Title.Length > 2)
    {
        db.Books.Update(book);
        await db.SaveChangesAsync();
        return Results.Ok(book);
    }
    return Results.BadRequest(new
    {
        Message = "Invalid book data.",
        Errors = new[] { "Title must be at least 3 characters long." }
    });
});

// Remove a book by ID
app.MapPost("/books/remove/{id}", async (FirstDB db, int id) =>
{
    var book = await db.Books.FindAsync(id);
    if (book != null)
    {
        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound(new { Message = "Book not found." });
});

// Delete all books from the database
app.MapPost("/books/removeall", async (FirstDB db) =>
{
    db.Books.RemoveRange(db.Books);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

#region Members
// Add a member with basic validation
app.MapPost("/members/add", async (FirstDB db, Member member) =>
{
    if (member != null && !string.IsNullOrWhiteSpace(member.Fullname) && member.Fullname.Length > 2)
    {
        await db.Members.AddAsync(member);
        await db.SaveChangesAsync();
        return Results.Ok(member);
    }
    return Results.BadRequest(new
    {
        Message = "Invalid member data.",
        Errors = new[] { "Fullname must be at least 3 characters long." }
    });
});

// Get all members
app.MapGet("/members/list", async (FirstDB db) =>
{
    var members = await db.Members.ToListAsync();
    return Results.Ok(members);
});

// Edit a member if valid
app.MapPost("/members/edit", async (FirstDB db, Member member) =>
{
    if (member != null && !string.IsNullOrWhiteSpace(member.Fullname) && member.Fullname.Length > 2)
    {
        db.Members.Update(member);
        await db.SaveChangesAsync();
        return Results.Ok(member);
    }
    return Results.BadRequest(new
    {
        Message = "Invalid member data.",
        Errors = new[] { "Fullname must be at least 3 characters long." }
    });
});

// Remove a member by ID
app.MapPost("/members/remove/{id}", async (FirstDB db, int id) =>
{
    var member = await db.Members.FindAsync(id);
    if (member != null)
    {
        db.Members.Remove(member);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound(new { Message = "Member not found." });
});

// Delete all members
app.MapPost("/members/removeall", async (FirstDB db) =>
{
    db.Members.RemoveRange(db.Members);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

#region Borrows
// Add a borrow record (no validation here, assumes front-end ensures integrity)
app.MapPost("/borrows/add", async (FirstDB db, Borrow borrow) =>
{
    await db.Borrows.AddAsync(borrow);
    await db.SaveChangesAsync();
    return Results.Ok(borrow);
});

// List all borrows with related Book and Member data
app.MapGet("/borrow/list", async (FirstDB db) =>
{
    var borrows = await db.Borrows.Include(b => b.Book).Include(b => b.Member).ToListAsync();
    return Results.Ok(borrows);
});

// Update borrow record
app.MapPost("/borrows/edit", async (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Update(borrow);
    await db.SaveChangesAsync();
    return Results.Ok(borrow);
});

// Delete a borrow by ID
app.MapPost("/borrows/remove/{id}", async (FirstDB db, int id) =>
{
    var borrow = await db.Borrows.FindAsync(id);
    if (borrow != null)
    {
        db.Borrows.Remove(borrow);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound(new { Message = "Borrow not found." });
});

// Delete all borrow records
app.MapPost("/borrows/removeall", async (FirstDB db) =>
{
    db.Borrows.RemoveRange(db.Borrows);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

app.Run(); // Start the application
