using CoursesApp.Data;

namespace CoursesApp
{
    public class CourseSeederAPP
    {
        //Proszę o dokończenie!!!!!!!!!!!!!!!

        private readonly ApplicationDbContext _dbContext;
        public CourseSeederAPP(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void seed()
        {
            if(_dbContext.Database.CanConnect())
            {

            }
        }
    }
}
