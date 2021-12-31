using Hangfire;
using Hangfire.SqlServer;
using HangFireWorker.JobsManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotMvcButCoreWebUi.Data;
using NotMvcButCoreWebUi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotMvcButCoreWebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ProjectDev")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IEmailSender,EmailSender>();
            var hangfireConnectionString = Configuration["ConnectionStrings:HangfireDev"];

           
            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                config.UseSqlServerStorage(hangfireConnectionString,option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));

            });


            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            #region HangFire

       
         

            app.UseHangfireDashboard("/HangFire",new DashboardOptions
            {
                DashboardTitle = "Zamanlanmis G�revler ve Niceleri",  // Dashboard sayfas�na ait Ba�l�k alan�n� de�i�tiririz.
                AppPath = "/Views/Home/About",// Dashboard �zerinden "back to site" button

                //Authorization = new[] { new HangfireDashboardAuthorizationFilter() } core olsaydi iyiydi
                //G�venlik i�in authorization i�lemleri  
            });
            //app.UseHangfireServer();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                #region job t�rleri

                /*
                 * Fire & Forget: Bir kez ve an�nda �al��acak olan job �e�ididir.
                 * Delayed: Belirtilen s�renin sonunda bir defaya mahsus �al��acak olan job �e�ididir.
                 * Recurring: Recursive olarak devaml� �al��acak olan job �e�ididir.
                 * Continuations: Daha �nceden tan�mlanm�� olan job��n (scheduled) ba�ar�l� �ekilde �al��mas� durumunda �al��acak olan job �e�ididir.
                 */

                #endregion

                #region Bilgiler

                /*  
                    Hangfire Server, planlanan i�leri s�ralar�na g�re s�ralamak i�in zamanlamay� d�zenli olarak denetler ve 
                    �al��anlar�n bunlar� y�r�tmesine olanak tan�r. 
                    Varsay�lan olarak, kontrol aral��� 15 saniyeye e�ittir, ancak BackgroundJobServer yap�c�s�na iletti�iniz se�eneklerde 
                    SchedulePollingInterval �zelli�ini ayarlayarak de�i�tirebilirsiniz   
                */

                #endregion
                ServerName = String.Format("{0}.{1}",Environment.MachineName,Guid.NewGuid().ToString()),
                SchedulePollingInterval = TimeSpan.FromSeconds(15),

                //Arkaplanda �al��acak Job say�s�n� de�i�tirebiliriz.
                WorkerCount = Environment.ProcessorCount * 5
            });

            #endregion

            #region �rnek kullan�mlar

            /*
             * BackgroundJob.Schedule<DailySendMaff>(jobs => jobs.DailySendMaffPolicies(),DateTimeOffset.UtcNow.AddDays(1));
             * BackgroundJob.Enqueue<DailySendMaff>(jobs => jobs.DailySendMaffPolicies());
             * BackgroundJob.Schedule<DailySendMaff>(x => x.DailySendMaffPolicies(),DateTimeOffset.UtcNow.AddDays(1));
             * RecurringJob.AddOrUpdate<DailySendMaff>("RecurringSendGetRequest",jobs => jobs.DailySendMaffPolicies(),Cron.Hourly());
             */

            #endregion
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 1 });
            #region Joblar Ba�lan�yor.

            // ContinuationJobs.setContinuationJobs();
            RecurringJobsManager ss =new RecurringJobsManager();
            ss.SetRecurringJobs();

            #endregion

            app.UseHangfireDashboard();

        }
    }
}
