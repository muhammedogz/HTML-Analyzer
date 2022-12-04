var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:5173/HTML-Analyzer/",
                                            "https://muhammedogz.github.io/HTML-Analyzer/",
                                            // localhost
                                            "http://localhost:5173",
                                            "http://localhost:5173/",
                                            "http://localhost:5173/HTML-Analyzer",
                                            // github
                                            "https://muhammedogz.github.io"
                                            );
                    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
