using Microsoft.EntityFrameworkCore;
using Novin.Libary.Backend.API.DB;
using Novin.Libary.Backend.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FirstDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<LibraryUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<FirstDB>();

var app = builder.Build();

// Configure the HTTP request pipeline
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
// Book Add
app.MapPost("/books/add", async (FirstDB db, Book book) =>
{
    if (book != null && !string.IsNullOrWhiteSpace(book.Title) && book.Title.Length > 2)
    {
        await db.Books.AddAsync(book);
        await db.SaveChangesAsync();
        return Results.Ok(book);
    }
    return Results.BadRequest("Invalid book data.");
});

// Book List
app.MapGet("/books/list", async (FirstDB db) =>
{
    var books = await db.Books.ToListAsync();
    return Results.Ok(books);
});

// Book Edit
app.MapPost("/books/edit", async (FirstDB db, Book book) =>
{
    if (book != null && !string.IsNullOrWhiteSpace(book.Title) && book.Title.Length > 2)
    {
        db.Books.Update(book);
        await db.SaveChangesAsync();
        return Results.Ok(book);
    }
    return Results.BadRequest("Invalid book data.");
});

// Book Remove
app.MapPost("/books/remove/{id}", async (FirstDB db, int id) =>
{
    var book = await db.Books.FindAsync(id);
    if (book != null)
    {
        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound("Book not found.");
});

// Remove All Books
app.MapPost("/books/removeall", async (FirstDB db) =>
{
    db.Books.RemoveRange(db.Books);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

#region Members
// Member Add
app.MapPost("/members/add", async (FirstDB db, Member member) =>
{
    if (member != null && !string.IsNullOrWhiteSpace(member.Fullname) && member.Fullname.Length > 2)
    {
        await db.Members.AddAsync(member);
        await db.SaveChangesAsync();
        return Results.Ok(member);
    }
    return Results.BadRequest("Invalid member data.");
});

// Member List
app.MapGet("/members/list", async (FirstDB db) =>
{
    var members = await db.Members.ToListAsync();
    return Results.Ok(members);
});

// Member Edit
app.MapPost("/members/edit", async (FirstDB db, Member member) =>
{
    if (member != null && !string.IsNullOrWhiteSpace(member.Fullname) && member.Fullname.Length > 2)
    {
        db.Members.Update(member);
        await db.SaveChangesAsync();
        return Results.Ok(member);
    }
    return Results.BadRequest("Invalid member data.");
});

// Member Remove
app.MapPost("/members/remove/{id}", async (FirstDB db, int id) =>
{
    var member = await db.Members.FindAsync(id);
    if (member != null)
    {
        db.Members.Remove(member);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound("Member not found.");
});

// Remove All Members
app.MapPost("/members/removeall", async (FirstDB db) =>
{
    db.Members.RemoveRange(db.Members);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

#region Borrows
// Borrow Add
app.MapPost("/borrows/add", async (FirstDB db, Borrow borrow) =>
{
    await db.Borrows.AddAsync(borrow);
    await db.SaveChangesAsync();
    return Results.Ok(borrow);
});

// Borrow List
app.MapGet("/borrow/list", async (FirstDB db) =>
{
    var borrows = await db.Borrows.Include(b => b.Book).Include(b => b.Member).ToListAsync();
    return Results.Ok(borrows);
});

// Borrow Edit
app.MapPost("/borrows/edit", async (FirstDB db, Borrow borrow) =>
{
    db.Borrows.Update(borrow);
    await db.SaveChangesAsync();
    return Results.Ok(borrow);
});

// Borrow Remove
app.MapPost("/borrows/remove/{id}", async (FirstDB db, int id) =>
{
    var borrow = await db.Borrows.FindAsync(id);
    if (borrow != null)
    {
        db.Borrows.Remove(borrow);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound("Borrow not found.");
});

// Remove All Borrows
app.MapPost("/borrows/removeall", async (FirstDB db) =>
{
    db.Borrows.RemoveRange(db.Borrows);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

app.Run();