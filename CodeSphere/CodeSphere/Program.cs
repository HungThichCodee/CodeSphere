using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorStrap;
using CloudinaryDotNet;
using CodeSphere.Areas.Administration.Repositories.AddChatSticker;
using CodeSphere.Areas.Administration.Repositories.AddChatStickers;
using CodeSphere.Areas.Administration.Repositories.AddChatStickerType;
using CodeSphere.Areas.Administration.Repositories.AddChatTheme;
using CodeSphere.Areas.Administration.Repositories.AddEmoji;
using CodeSphere.Areas.Administration.Repositories.AddEmojis;
using CodeSphere.Areas.Administration.Repositories.AddEmojiWithSkin;
using CodeSphere.Areas.Administration.Repositories.AddHolidayTheme;
using CodeSphere.Areas.Administration.Repositories.AllChatStickers;
using CodeSphere.Areas.Administration.Repositories.AllEmojis;
using CodeSphere.Areas.Administration.Repositories.AllHolidayThemes;
using CodeSphere.Areas.Administration.Repositories.BlogAddons;
using CodeSphere.Areas.Administration.Repositories.Dashboard;
using CodeSphere.Areas.Administration.Repositories.DbUsage;
using CodeSphere.Areas.Administration.Repositories.DeleteChatSticker;
using CodeSphere.Areas.Administration.Repositories.DeleteChatStickerType;
using CodeSphere.Areas.Administration.Repositories.DeleteChatTheme;
using CodeSphere.Areas.Administration.Repositories.DeleteEmoji;
using CodeSphere.Areas.Administration.Repositories.DeleteEmojisByType;
using CodeSphere.Areas.Administration.Repositories.EditChatSticker;
using CodeSphere.Areas.Administration.Repositories.EditChatStickerType;
using CodeSphere.Areas.Administration.Repositories.EditChatTheme;
using CodeSphere.Areas.Administration.Repositories.EditEmoji;
using CodeSphere.Areas.Administration.Repositories.EditEmojiPosition;
using CodeSphere.Areas.Administration.Repositories.PendingComments;
using CodeSphere.Areas.Administration.Repositories.PendingPosts;
using CodeSphere.Areas.Administration.Repositories.SiteReports.BlogReports;
using CodeSphere.Areas.Administration.Repositories.UserPenalties;
using CodeSphere.Areas.Administration.Repositories.UsersInformation;
using CodeSphere.Areas.Editor.Repositories.CategoryRepositories;
using CodeSphere.Areas.Editor.Repositories.CommentRepositories;
using CodeSphere.Areas.Editor.Repositories.PostRepositories;
using CodeSphere.Areas.PrivateChat.Repositories.ChatMessagesDbUsage;
using CodeSphere.Areas.PrivateChat.Repositories.CollectStickers;
using CodeSphere.Areas.PrivateChat.Repositories.PrivateChat;
using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Areas.UserNotifications.Repositories.NotificationDbUsage;
using CodeSphere.AutoMapperProfiles.Blog;
using CodeSphere.AutoMapperProfiles.User;
using CodeSphere.AutoMapperProfiles.ViewComponents;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Hubs;
using CodeSphere.MlModels.CommentModels;
using CodeSphere.MlModels.PostModels;
using CodeSphere.Models.User;
using CodeSphere.Repositories;
using CodeSphere.Repositories.AiRepositories;
using CodeSphere.Repositories.AllCategories;
using CodeSphere.Repositories.BlogRepositories;
using CodeSphere.Repositories.CategoryRepositories;
using CodeSphere.Repositories.CloudRepositories;
using CodeSphere.Repositories.CommentRepositories;
using CodeSphere.Repositories.ContactRepositories;
using CodeSphere.Repositories.HomeRepositories;
using CodeSphere.Repositories.PostRepositories;
using CodeSphere.Repositories.ProfileRepositories;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllAdministrators;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllUsersTab;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.BannedUsers;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.RecommendedUsers;
using CodeSphere.Repositories.ProfileRepositories.Pagination.Profile;
using CodeSphere.Repositories.RecommendedFriendsRepositories;
using CodeSphere.Repositories.TagRepositories;
using CodeSphere.Repositories.UserActivitesDbUsage.AllActivities;
using CodeSphere.Repositories.UserActivitesDbUsage.FollowActivities;
using CodeSphere.Repositories.UserPostRepositories;
using CodeSphere.SecurityModels;
using CodeSphere.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ML;
using OfficeOpenXml;
using SdvCode.Areas.Administration.Services.EditEmojiPosition;
using Twilio;

//deploy
using Microsoft.AspNetCore.HttpOverrides;

namespace CodeSphere
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //deploy
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto |
                    ForwardedHeaders.XForwardedHost;

                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            //deploy

            // Access the configuration instance from the builder
            var configuration = builder.Configuration;

            // Add services to the container.

            // Initialize ApplicationUser and DdContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = GlobalConstants.PasswordRequiredLength;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
                options.LoginPath = "/Identity/Account/Login";
                options.SlidingExpiration = true;
            });

            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            // Configuration for update cookies when user is added in Role!!!
            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(0);
            });

            // Social Network Authentication
            builder.Services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });

            var cloudinaryAccount = new CloudinaryDotNet.Account(configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);
            var cloudinary = new Cloudinary(cloudinaryAccount);
            builder.Services.AddSingleton(cloudinary);

            // Twilio Authentication
            var accountSid = configuration["Twilio:AccountSID"];
            var authToken = configuration["Twilio:AuthToken"];
            TwilioClient.Init(accountSid, authToken);
            builder.Services.Configure<TwilioVerifySettings>(configuration.GetSection("Twilio"));

            builder.Services.AddTransient<ApplicationDbContext>();

            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<IProfileRepository, ProfileRepository>();
            builder.Services.AddTransient<IDashboardRepository, DashboardRepository>();
            builder.Services.AddTransient<IHomeRepository, HomeRepository>();
            builder.Services.AddTransient<IDbUsageRepository, DbUsageRepository>();
            builder.Services.AddTransient<IUsersPenaltiesRepository, UsersPenaltiesRepository>();
            builder.Services.AddTransient<IProfileActivitiesRepository, ProfileActivitiesRepository>();
            builder.Services.AddTransient<IProfileFollowersRepository, ProfileFollowersRepository>();
            builder.Services.AddTransient<IProfileFollowingRepository, ProfileFollowingRepository>();
            builder.Services.AddTransient<IBlogRepository, BlogRepository>();
            builder.Services.AddTransient<IBlogAddonsRepository, BlogAddonsRepository>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();
            builder.Services.AddTransient<IBlogComponentRepository, BlogComponentRepository>();
            builder.Services.AddTransient<ITagRepository, TagRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IUserPostsRepository, UserPostsRepository>();
            builder.Services.AddTransient<IEditCategoryRepository, EditCategoryRepository>();
            builder.Services.AddTransient<IProfileFavoritesRepository, ProfileFavouritePostsRepository>();
            builder.Services.AddTransient<IAddCategoryRepository, AddCategoryRepository>();
            builder.Services.AddTransient<IEditorPostRepository, EditorPostRepository>();
            builder.Services.AddTransient<IProfilePendingPostsRepository, ProfilePendingPostsRepository>();
            builder.Services.AddTransient<IProfileBannedPostsRepository, ProfileBannedPostsRepository>();
            builder.Services.AddTransient<IPrivateChatRepository, PrivateChatRepository>();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<IAllUsersRepository, AllUsersRepository>();
            builder.Services.AddTransient<IBannedUsersRepository, BannedUsersRepository>();
            builder.Services.AddTransient<IAiService, AiService>();
            builder.Services.AddTransient<IAiRepository, AiRepository>();
            builder.Services.AddTransient<IRecommendedUsersRepository, RecommendedUsersRepository>();
            builder.Services.AddTransient<IPendingCommentsRepository, PendingCommentsRepository>();
            builder.Services.AddTransient<IPendingPostsRepository, PendingPostsRepository>();
            builder.Services.AddTransient<IEditorCommentRepository, EditorCommentRepository>();
            builder.Services.AddTransient<IBlogPostReport, BlogPostReport>();
            builder.Services.AddTransient<IUsersInformationRepository, UsersInformationRepository>();
            builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

            builder.Services.AddTransient<IAllAdministratorsRepository, AllAdministratorsRepository>();

            builder.Services.AddTransient<IAddEmojiRepository, AddEmojiRepository>();
            builder.Services.AddTransient<IEditEmojiRepository, EditEmojiRepository>();
            builder.Services.AddTransient<IDeleteEmojiRepository, DeleteEmojiRepository>();
            builder.Services.AddTransient<IEditEmojiPositionRepository, EditEmojiPositionRepository>();
            builder.Services.AddTransient<IAllEmojisRepository, AllEmojisRepository>();
            builder.Services.AddTransient<IAddChatThemeRepository, AddChatThemeRepository>();
            builder.Services.AddTransient<IDeleteChatThemeRepository, DeleteChatThemeRepository>();
            builder.Services.AddTransient<IEditChatThemeRepository, EditChatThemeRepository>();
            builder.Services.AddTransient<IAddEmojisRepository, AddEmojisRepository>();
            builder.Services.AddTransient<IAddEmojiWithSkinRepository, AddEmojiWithSkinRepository>();
            builder.Services.AddTransient<IDeleteEmojisByTypeRepository, DeleteEmojisByTypeRepository>();
            builder.Services.AddTransient<IAddChatStickerTypeRepository, AddChatStickerTypeRepository>();
            builder.Services.AddTransient<IAddChatStickerRepository, AddChatStickerRepository>();
            builder.Services.AddTransient<IEditChatStickerTypeRepository, EditChatStickerTypeRepository>();
            builder.Services.AddTransient<IEditChatStickerRepository, EditChatStickerRepository>();
            builder.Services.AddTransient<IDeleteChatStickerRepository, DeleteChatStickerRepository>();
            builder.Services.AddTransient<IDeleteChatStickerTypeRepository, DeleteChatStickerTypeRepository>();
            builder.Services.AddTransient<IAddChatStickersRepository, AddChatStickersRepository>();
            builder.Services.AddTransient<IAllChatStickersRepository, AllChatStickersRepository>();

            builder.Services.AddTransient<IAllCategoriesRepository, AllCategoriesRepository>();
            builder.Services.AddTransient<ICollectStickersRepository, CollectStickersRepository>();

            builder.Services.AddTransient<IAddHolidayThemeRepository, AddHolidayThemeRepository>();
            builder.Services.AddTransient<IAllHolidayThemesRepository, AllHolidayThemesRepository>();

            builder.Services.AddTransient<AdminAccountService>();

            // Register ML Models
            builder.Services.AddPredictionEnginePool<BlogPostModelInput, BlogPostModelOutput>()
                .FromFile("MlModels/PostModels/BlogPostMLModel.zip");
            builder.Services.AddPredictionEnginePool<BlogCommentModelInput, BlogCommentModelOutput>()
                .FromFile("MlModels/CommentModels/BlogCommentMLModel.zip");

            // Groq AI Configuration
            builder.Services.Configure<GroqSettings>(configuration.GetSection("Groq"));
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IAiService, AiService>();
            builder.Services.AddTransient<IAiRepository, AiRepository>();

            // Configure ReCaptch Settings
            builder.Services.Configure<ReCaptchSettings>(configuration.GetSection("GoogleReCAPTCHA"));

            // Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                }));

            // Add the processing server as IHostedService
            builder.Services.AddHangfireServer();

            // Add Server Side Blazor
            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            // Server Side Blazor doesn't register HttpClient by default
            if (!builder.Services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                builder.Services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri),
                    };
                });
            }

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PostProfile(provider.GetService<IHttpContextAccessor>()));
                cfg.AddProfile(new UserProfile(
                    provider.GetService<ApplicationDbContext>(),
                    provider.GetService<IHttpContextAccessor>()));
                cfg.AddProfile(new RoleProfile());
                cfg.AddProfile(new CategoryProfile());
                cfg.AddProfile(new CommentProfile());
                cfg.AddProfile(new PostImageProfile());
                cfg.AddProfile(new PostTagProfile());
                cfg.AddProfile(new CountryCodeProfile());
                cfg.AddProfile(new CountryProfile());
                cfg.AddProfile(new StateProfile());
                cfg.AddProfile(new CityProfile());
                cfg.AddProfile(new ZipCodeProfile());
                cfg.AddProfile(new UserActionProfile());
                cfg.AddProfile(new BannedPostProfile());
                cfg.AddProfile(new FavouritePostProfile());
                cfg.AddProfile(new PendingPostProfile());
                cfg.AddProfile(new FollowUnfollowProfile(
                    provider.GetService<ApplicationDbContext>(),
                    provider.GetService<IHttpContextAccessor>()));
            }).CreateMapper());

            // Add Blazor Session and Local Storages
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddBlazorStrap();

            builder.Services.AddHttpClient();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            
            // Cấu hình SignalR với message size lớn hơn
            builder.Services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 102400; // 100KB (mặc định 32KB)
                options.EnableDetailedErrors = true; // Hiển thị error chi tiết khi development
            });

            // --- BẮT ĐẦU ĐOẠN CẦN THÊM session ---

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            // --- KẾT THÚC ĐOẠN CẦN THÊM session ---

            var app = builder.Build();

            //deploy
            app.UseForwardedHeaders();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //deploy


            //using (var scope = app.Services.CreateScope())
            //{
            //    var adminService = scope.ServiceProvider.GetRequiredService<AdminAccountService>();
            //    await adminService.EnsureAdminAccountExistsAsync();
            //}

            //using (var scope = app.Services.CreateScope())
            //{
            //    var EditorService = scope.ServiceProvider.GetRequiredService<AdminAccountService>();
            //    await EditorService.EnsureEditorAccountsExistAsync();
            //}

            //using (var scope = app.Services.CreateScope())
            //{
            //    var subscriberService = scope.ServiceProvider.GetRequiredService<AdminAccountService>();
            //    await subscriberService.EnsureSubscriberAccountsExistAsync();
            //}

            using (var scope = app.Services.CreateScope())
            {
                var contributorService = scope.ServiceProvider.GetRequiredService<AdminAccountService>();
                await contributorService.EnsureContributorAccountsExistAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            if(!app.Environment.IsProduction())
{
                app.UseHangfireDashboard("/Administration/UsersPenalties/HangFire", new DashboardOptions
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
            }

            // Seed Recurring Jobs (chạy 1 lần khi start app)
            using (var scope = app.Services.CreateScope())
            {
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                SeedHangfireJobs(recurringJobManager);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<PrivateChatHub>("/privateChatHub");
                endpoints.MapHub<NotificationHub>("/notificationHub");
                endpoints.MapHub<UserStatusHub>("/userStatusHub");

                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });

            app.Run();
        }

        private static void SeedHangfireJobs(IRecurringJobManager recurringJobManager)
        {
            // Edit recommended friend list for each user
            recurringJobManager
                .AddOrUpdate<RecommendedFriends>(
                "RecommendedFriends",
                x => x.AddRecomendedFriends(),
                Cron.Weekly);

            // Delete all follow-unfollow activities
            recurringJobManager
                .AddOrUpdate<UserFollowActivitiesDbUsage>(
                "UserActivitiesDbSavage",
                x => x.DeleteFollowActivites(),
                Cron.Monthly);

            // Delete all user activities
            recurringJobManager
                .AddOrUpdate<AllActivities>("AllActivities", x => x.DeleteAllActivites(), Cron.Yearly);

            // Delete all chat messages
            recurringJobManager
                .AddOrUpdate<DeleteMessages>("DeleteMessages", x => x.DeleteAllChatMessages(), Cron.Yearly);

            // Delete all user notification
            recurringJobManager
                .AddOrUpdate<NotificationDbUsage>("DeleteNotifications", x => x.DeleteNotifications(), Cron.Yearly);
        }

        // 2. Dashboard Role Authorization
        public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRole);
            }
        }
    }
}