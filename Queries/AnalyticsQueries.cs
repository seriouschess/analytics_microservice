using System.Collections.Generic;
using System.Linq;
using analytics.Models;

namespace analytics.Queries
{
    public class AnalyticsQueries
    {
        private AnalyticsContext dbContext;

        public AnalyticsQueries(AnalyticsContext _dbContext){
            dbContext = _dbContext;
        }

        public void addSession(GenericSession NewSession){ //void a good idea?
            dbContext.Add(NewSession);
            // OR dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        public void updateSession(GenericSession _CurrentSession){
            GenericSession CurrentSession = dbContext.SessionGs.Where( x => x.session_id == _CurrentSession.session_id).FirstOrDefault();
            CurrentSession.time_on_homepage = _CurrentSession.time_on_homepage;
            dbContext.SaveChanges();
        }

        public List<GenericSession> getAllSessions(){
            List<GenericSession> output = dbContext.SessionGs.ToList();
            return output;
        }

        public void methodicalDelete(){
            List<GenericSession> output = dbContext.SessionGs.ToList();
            foreach (GenericSession item in output){
                dbContext.SessionGs.Remove(item);
                dbContext.SaveChanges();
            }
        }


        //authentication queries
        public GenericSession getSessionById(int session_id){
            GenericSession FoundSession =  dbContext.SessionGs.Where( x => x.session_id == session_id).FirstOrDefault();
            return FoundSession;
        }
    }
}