using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if (!dbContext.Questions.Any())
{
	var questionOneAnswer = Guid.NewGuid();
	var questionOne = new Question()
	{
		Text = "What is the capital of New Zealand?",
		Options = new List<Option>
		{
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Auckland"
			},
			new Option()
			{
				Id = questionOneAnswer,
				Text = "Wellington"
			},
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Christchurch"
			},
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Queenstown"
			}
		},
		CorrectOption = questionOneAnswer
	};

	var questionTwoAnswer = Guid.NewGuid();
	var questionTwo = new Question()
	{
		Text = "What is the capital of India?",
		Options = new List<Option>
		{
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Mumbai"
			},
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Kolkata"
			},
			new Option()
			{
				Id = questionTwoAnswer,
				Text = "New Delhi"
			},
			new Option()
			{
				Id = Guid.NewGuid(),
				Text = "Chennai"
			}
		},
		CorrectOption = questionTwoAnswer
	};

	dbContext.Questions.AddRange([questionOne, questionTwo]);
	dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Quiz}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
