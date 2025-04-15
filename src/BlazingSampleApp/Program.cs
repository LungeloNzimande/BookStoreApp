using BlazingSampleApp.Data;
using BlazingSampleApp.Services;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;

namespace BlazingSampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient<ISampleApiClient, SampleApiClient>(c => 
                c.BaseAddress = new Uri("https://fakestoreapi.com"));
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredLocalStorage();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
