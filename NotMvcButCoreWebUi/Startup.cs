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
                DashboardTitle = "Zamanlanmis Görevler ve Niceleri",  // Dashboard sayfasýna ait Baþlýk alanýný deðiþtiririz.
                AppPath = "/Views/Home/About",// Dashboard üzerinden "back to site" button

                //Authorization = new[] { new HangfireDashboardAuthorizationFilter() } core olsaydi iyiydi
                //Güvenlik için authorization iþlemleri  
            });
            //app.UseHangfireServer();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                #region job türleri

                /*
                 * Fire & Forget: Bir kez ve anýnda çalýþacak olan job çeþididir.
                 * Delayed: Belirtilen sürenin sonunda bir defaya mahsus çalýþacak olan job çeþididir.
                 * Recurring: Recursive olarak devamlý çalýþacak olan job çeþididir.
                 * Continuations: Daha önceden tanýmlanmýþ olan job’ýn (scheduled) baþarýlý þekilde çalýþmasý durumunda çalýþacak olan job çeþididir.
                 */

                #endregion

                #region Bilgiler

                /*  
                    Hangfire Server, planlanan iþleri sýralarýna göre sýralamak için zamanlamayý düzenli olarak denetler ve 
                    çalýþanlarýn bunlarý yürütmesine olanak tanýr. 
                    Varsayýlan olarak, kontrol aralýðý 15 saniyeye eþittir, ancak BackgroundJobServer yapýcýsýna ilettiðiniz seçeneklerde 
                    SchedulePollingInterval özelliðini ayarlayarak deðiþtirebilirsiniz   
                */

                #endregion
                ServerName = String.Format("{0}.{1}",Environment.MachineName,Guid.NewGuid().ToString()),
                SchedulePollingInterval = TimeSpan.FromSeconds(15),

                //Arkaplanda çalýþacak Job sayýsýný deðiþtirebiliriz.
                WorkerCount = Environment.ProcessorCount * 5
            });

            #endregion

            #region örnek kullanýmlar

            /*
             * BackgroundJob.Schedule<DailySendMaff>(jobs => jobs.DailySendMaffPolicies(),DateTimeOffset.UtcNow.AddDays(1));
             * BackgroundJob.Enqueue<DailySendMaff>(jobs => jobs.DailySendMaffPolicies());
             * BackgroundJob.Schedule<DailySendMaff>(x => x.DailySendMaffPolicies(),DateTimeOffset.UtcNow.AddDays(1));
             * RecurringJob.AddOrUpdate<DailySendMaff>("RecurringSendGetRequest",jobs => jobs.DailySendMaffPolicies(),Cron.Hourly());
             */

            #endregion
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 1 });
            #region Joblar Baðlanýyor.

            // ContinuationJobs.setContinuationJobs();
            RecurringJobsManager ss =new RecurringJobsManager();
            ss.SetRecurringJobs();

            #endregion

            app.UseHangfireDashboard();

        }
    }
}
