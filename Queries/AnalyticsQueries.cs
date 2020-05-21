using System;
using System.Collections.Generic;
using System.Linq;
using analytics.Classes;
using analytics.Models;

namespace analytics.Queries
{
    public class AnalyticsQueries
    {
        private AnalyticsContext dbContext;
        private DataFormatter formatter;

        public AnalyticsQueries(AnalyticsContext _dbContext){
            dbContext = _dbContext;
            formatter = new DataFormatter();
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
            GenericSession FoundSession =  dbContext.SessionGs.Where(x => x.session_id == session_id).FirstOrDefault();
            return FoundSession;
        }

        public List<GenericSession> getSessionsByDomain(string domain){ //can also take a full url
            domain = formatter.stripDomain(domain);
            return dbContext.SessionGs.Where( x => x.url.Contains(domain) ).ToList();
        }

        public List<GenericSession> getSessionsByUrl(string url){
            return dbContext.SessionGs.Where( x => x.url.Contains(url) ).ToList();
        }

        public List<GenericSession> getSessionsByDate(DateTime date){
            List<GenericSession> FoundSessions =  dbContext.SessionGs.Where(x => x.created_at.Year == date.Year )
                                                                .Where(x => x.created_at.Month == date.Month )
                                                                .Where(x => x.created_at.Day == date.Day).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsBeforeDateTime(DateTime date){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where(x => x.created_at < date).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsAfterDateTime(DateTime date){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where(x => x.created_at > date).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsInDateTimeRange(DateTime min, DateTime max){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where(x => x.created_at > min)
                                                                    .Where(x => x.created_at < max).ToList();
            return FoundSessions;
        }
    }
}