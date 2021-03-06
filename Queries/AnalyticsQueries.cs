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
        //private DataFormatter formatter;

        public AnalyticsQueries(AnalyticsContext _dbContext){
            dbContext = _dbContext;
            //formatter = new DataFormatter(); //--strip domain functionality not currently used
        }

        public void addSession(GenericSession NewSession){
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
            List<GenericSession> output = dbContext.SessionGs.OrderBy(x => x.created_at).ToList();
            return output;
        }

        public void methodicalDelete(){ //for testing. Should not be in production.
            List<GenericSession> output = dbContext.SessionGs.ToList();
            foreach (GenericSession item in output){
                dbContext.SessionGs.Remove(item);
                dbContext.SaveChanges();
            }
        }

        //Summary Queries
        public int getSessionCountByDomain(string domain){
            //domain = formatter.stripDomain(domain);
            var count = dbContext.SessionGs.Count(x => x.url.Contains(domain));
            return count;
        }

        //Session Queries
        public GenericSession getSessionById(int session_id){
            GenericSession FoundSession =  dbContext.SessionGs.Where(x => x.session_id == session_id).FirstOrDefault();
            return FoundSession;
        }

        public List<GenericSession> getSessionsByDomain(string domain){ //can also take a full url
            //domain = formatter.stripDomain(domain);
            return dbContext.SessionGs.Where( x => x.url.Contains(domain) ).ToList();
        }

        public List<GenericSession> getSessionsByUrl(string url){
            return dbContext.SessionGs.Where( x => x.url.Contains(url) ).ToList();
        }

        //datetime queries

        public List<GenericSession> getSessionsForDomainByMonth(string domain, DateTime date){ //can also take a full url
            //domain = formatter.stripDomain(domain);
            return dbContext.SessionGs.Where( x => x.url.Contains(domain) )
                                        .Where(x => x.created_at.Year == date.Year )
                                        .Where(x => x.created_at.Month == date.Month )
                                        .OrderBy(x => x.created_at).ToList();
        }

        public List<GenericSession> getSessionsForDomainByDate(string domain, DateTime date){ //can also take a full url
            //domain = formatter.stripDomain(domain);
            return dbContext.SessionGs.Where( x => x.url.Contains(domain) )
                                        .Where(x => x.created_at.Year == date.Year )
                                        .Where(x => x.created_at.Month == date.Month )
                                        .Where(x => x.created_at.Day == date.Day)
                                        .OrderBy(x => x.created_at).ToList();
        }

        public List<GenericSession> getSessionsByDate(DateTime date){
            List<GenericSession> FoundSessions =  dbContext.SessionGs.Where(x => x.created_at.Year == date.Year )
                                                                .Where(x => x.created_at.Month == date.Month )
                                                                .Where(x => x.created_at.Day == date.Day)
                                                                .OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsByMonth(DateTime date){
            List<GenericSession> FoundSessions =  dbContext.SessionGs.Where(x => x.created_at.Year == date.Year )
                                                                .Where(x => x.created_at.Month == date.Month )
                                                                .OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsByYear(DateTime date){
            List<GenericSession> FoundSessions =  dbContext.SessionGs.Where(x => x.created_at.Year == date.Year ).OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsOnOrBeforeDateTime(DateTime date){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where(x => x.created_at <= date).OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsOnOrAfterDateTime(DateTime date){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where(x => x.created_at >= date).OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }

        public List<GenericSession> getSessionsByDomainInDateTimeRange(string domain, DateTime min, DateTime max){
            List<GenericSession> FoundSessions = dbContext.SessionGs.Where( x => x.url.Contains(domain) )
                                                                    .Where(x => x.created_at >= min)
                                                                    .Where(x => x.created_at <= max).OrderBy(x => x.created_at).ToList();
            return FoundSessions;
        }
    }
}