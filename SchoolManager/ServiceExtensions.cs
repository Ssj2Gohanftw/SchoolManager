using SchoolManager.Data.Repositories;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Services;
using SchoolManager.Services.Interfaces;

namespace SchoolManager
{
    public static class ServiceExtensions
    {
        //Extension function that wraps all the services
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentServices, StudentServices>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherServices, TeacherServices>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectServices, SubjectServices>();

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassServices, ClassServices>();

            services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
            services.AddScoped<IStudentSubjectServices, StudentSubjectServices>();

            services.AddScoped<ISubjectTeacherRepository, SubjectTeacherRepository>();
            services.AddScoped<ISubjectTeacherServices, SubjectTeacherServices>();

            services.AddScoped<IApiService, ApiService>();
            return services;
        }
    }
}
